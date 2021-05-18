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

    float movementTimer = 0;

    GridLayout grid;
    private bool attack;

    // Use this for initialization
    void Start()
    {
        grid = GameObject.FindGameObjectWithTag("GridLayout").GetComponent<GridLayout>();

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

        if (grid == null)
            Debug.Log("Could not find GridLayout object");

        else
        {
            if (grid.OutOfBounds(transform.position))
                transform.position = new Vector3(UnityEngine.Random.Range(4, 6), transform.position.y, UnityEngine.Random.Range(0, 3));
        }

        transform.position = new Vector3(Mathf.RoundToInt(transform.position.x), transform.position.y, Mathf.RoundToInt(transform.position.z));

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
        grid.GetEnemyPlatform(transform.position).SetPlatformType(Platform.PlatformType.Enemy);
        GameManager.Instance.enemyCount--;
    }

    void Update()
    {
        if(prevPos != transform.position)
        {
            grid.GetEnemyPlatform(transform.position).SetPlatformType(Platform.PlatformType.Enemy);
            prevPos = transform.position;
        }
        Timer(move);
        Move();
        StartCoroutine(CheckAttack());
    }



    IEnumerator CheckAttack()
    {
        if (target != null)
        {
            if (transform.position.z == target.transform.position.z && !attack)
            {
                weapon.Attack();
                attack = true;
                yield return new WaitForSeconds(attackSpeed);
                attack = false;
            }
        }
    }

    private void Move()
    {
        //Maybe make a movement class
        pos = new Vector3();
        if (target != null)
        {
            //get targets position and check if its in front of the enemy
            if (transform.position.z != target.transform.position.z)
            {
                move = true;
                if (target.transform.position.z < transform.position.z && grid.CheckPlatformSouth(transform.position, Platform.PlatformType.Enemy))
                    pos += Vector3.back;

                if (target.transform.position.z > transform.position.z && grid.CheckPlatformNorth(transform.position, Platform.PlatformType.Enemy))
                    pos += Vector3.forward;

                //StartCoroutine(Delay());
                grid.GetEnemyPlatform(transform.position).SetPlatformType(Platform.PlatformType.Occupied);
            }
        }

        if (movementTimer == 0)
        {
            grid.GetEnemyPlatform(transform.position).SetPlatformType(Platform.PlatformType.Enemy);
            transform.position += pos;
            grid.GetEnemyPlatform(transform.position).SetPlatformType(Platform.PlatformType.Occupied);
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

    public void SetPosition(Vector3 pos)
    {
        transform.position = new Vector3(pos.x, transform.position.y, pos.z);
    }

}
