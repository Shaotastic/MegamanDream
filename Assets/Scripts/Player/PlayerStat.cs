using UnityEngine;
using System.Collections;
using UnityEngine.UI;

//Change to PlayerStats.cs
public class PlayerStat : MonoBehaviour
{
    BonusStats bonus;
    public int startingHealth = 100;
    public int damage;
    public int armour;

    public float flashSpeed;
    public int flashAmount = 1;
    public Text text;
    Player player;

    int currentHealth;

    void Awake()
    {
        //if (GameManager.reset)
        //{
        //    Intialize();
        //}
    }

    // Use this for initialization
    void Start()
    {
        Intialize();

    }

    void Intialize()
    {

        if (player == null)
            player = GetComponent<Player>();

        if (bonus == null)
            bonus = GameObject.FindGameObjectWithTag("Bonus").GetComponent<BonusStats>();

        if (GameManager.Instance.currentHealth != -1 && GameManager.Instance.level == 1)
            currentHealth = GameManager.Instance.currentHealth;
        else
            currentHealth = startingHealth + BonusStats.healthB;
    }

    // Update is called once per frame
    void Update()
    {
        if (currentHealth < 0)
            Destroy(this.gameObject);
    }

    void OnGUI()
    {
        text.text = "Health: " + currentHealth.ToString()
            + "\nDefense: " + BonusStats.armourB.ToString()
            + "\nAttack: " + BonusStats.damageB.ToString();
        GameManager.Instance.currentHealth = currentHealth;
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= Mathf.RoundToInt(damage * (100 / (100 + bonus.defenseBonus)));
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Bullet")
        {
            if (col.GetComponent<Bullet>().GetWeaponType() == Weapon.WeaponType.Enemy)
            {
                TakeDamage(col.gameObject.GetComponent<Bullet>().GetBulletDamage());
                StartCoroutine(Flash());
            }
        }
    }
    IEnumerator Flash()
    {
        GetComponent<SpriteRenderer>().material.SetColor("_EmissionColor", Color.white);
        yield return new WaitForSecondsRealtime(flashSpeed);
        GetComponent<SpriteRenderer>().material.SetColor("_EmissionColor", Color.black);
    }
}
