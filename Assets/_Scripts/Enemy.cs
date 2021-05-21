using UnityEngine;
using System.Collections;
using System;

public class Enemy : MonoBehaviour
{
    enum States { Dead = 0, Attacking, Moving }
    public GameObject target;
    public Vector3 startPosition;
    public Weapon weapon;
    public float attackSpeed;
    public float flashSpeed = 0.1f;
    public int health = 50;

    public float movementDelay;

    Vector3 pos, prevPos;
    bool move;

    [SerializeField] float movementTimer = 0;

    GridLayout m_Grid;
    private bool attack;

    [SerializeField] private bool m_CanMove;

    // Use this for initialization
    void Start()
    {
        m_Grid = GameObject.FindGameObjectWithTag("GridLayout").GetComponent<GridLayout>();

        target = GameObject.FindGameObjectWithTag("Player");

        if (target == null)
            Debug.Log("There is not target to locate.");

        if (weapon == null)
        {
            Debug.Log("Weapon is null");
        }
        else
        {
            weapon.SetWeaponType(Weapon.WeaponType.Enemy);
        }

        //transform.position = new Vector3(Mathf.RoundToInt(transform.position.x), transform.position.y, Mathf.RoundToInt(transform.position.z));

        // grid.GetEnemyPlatform(transform.position).SetPlatformType(Platform.PlatformType.Occupied);
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Bullet")
        {
            if (col.GetComponent<Bullet>().GetWeaponType() == Weapon.WeaponType.Player)
            {
                TakeDamage(col.gameObject.GetComponent<Bullet>().GetBulletDamage());
                Destroy(col.gameObject);
                StartCoroutine(Flash());
            }
        }
    }


    private void TakeDamage(int damage)
    {
        health -= damage;
        if (health <= 0)
            Dead();
    }

    private void Dead()
    {
        Destroy(this.gameObject);
        m_Grid.GetPlatform(transform.position).SetPlatformSpace(Platform.PlatformSpace.Free);
    }


    void CheckAttack()
    {
        if (target != null)
        {
            if (transform.position.z == target.transform.position.z && !attack)
            {
                StartCoroutine(Attack());
            }
        }
    }

    IEnumerator Attack()
    {
        weapon.Attack();
        attack = true;
        m_CanMove = false;
        yield return new WaitForSeconds(attackSpeed);
        EnemyManager.Instance.NextEnemy();
        attack = false;
    }

    private void Move()
    {
        //Maybe make a movement class
        if (target != null)
        {
            //get targets position and check if its in front of the enemy
            if (transform.position.z != target.transform.position.z)
            {
                move = true;
                if (target.transform.position.z < transform.position.z)
                    m_Grid.MoveOnGrid(transform, GridLayout.Direction.Down, Platform.PlatformType.Enemy);

                if (target.transform.position.z > transform.position.z)
                    m_Grid.MoveOnGrid(transform, GridLayout.Direction.Up, Platform.PlatformType.Enemy);
            }
        }
    }

    void Timer(bool start)
    {
        if (start)
        {
            if (movementTimer <= movementDelay)
                movementTimer += Time.deltaTime;
            else
            {
                movementTimer = 0f;
                start = false;
            }
        }
    }

    IEnumerator Flash()
    {
        GetComponent<SpriteRenderer>().material.SetColor("_EmissionColor", Color.white);
        yield return new WaitForSecondsRealtime(flashSpeed);
        GetComponent<SpriteRenderer>().material.SetColor("_EmissionColor", Color.black);
    }

    private void Update()
    {

        if (!m_CanMove || GameManager.Instance.IsPaused)
            return;

        if (prevPos != transform.position)
        {
            prevPos = transform.position;
        }
        //Timer(move);
        //Move();
        CheckAttack();
        StartCoroutine(MoveDelay());
    }
    IEnumerator MoveDelay()
    {
        if (target != null && !move)
        {
            //get targets position and check if its in front of the enemy
            if (transform.position.z != target.transform.position.z)
            {
                move = true;
                if (target.transform.position.z < transform.position.z)
                    if (!m_Grid.MoveOnGrid(transform, GridLayout.Direction.Down, Platform.PlatformType.Enemy))
                    {
                        StartCoroutine(Attack());
                    }
                if (target.transform.position.z > transform.position.z)
                    if (!m_Grid.MoveOnGrid(transform, GridLayout.Direction.Up, Platform.PlatformType.Enemy))
                    {
                        StartCoroutine(Attack());
                    }
            }
        }
        yield return new WaitForSeconds(1);
        move = false;
    }
    public void EnableMovement()
    {
        m_CanMove = true;
    }

    public void DisableMovement()
    {
        m_CanMove = false;
    }

}
