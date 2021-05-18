using UnityEngine;
using System.Collections;

public class Gun : Weapon
{
    public Bullet bulletObj;

   public int gunDamage = 10;

    public float bulletSpeed;

    public int bulletRange = 1;


    BonusStats bonus;
    // Use this for initialization
    void Start()
    {
        if (bonus == null)
            bonus = GameObject.FindGameObjectWithTag("Bonus").GetComponent<BonusStats>();

        SetWeaponDamage(gunDamage);
        SetBonusDamage(BonusStats.damageB);
        SetWeaponSpeed(bulletSpeed);

        if (GetComponent<Enemy>())
            SetWeaponType(WeaponType.Enemy);
        else
            SetWeaponType(WeaponType.Player);
    }

    // Update is called once per frame
    void Update()
    {
    }

    override public void Attack()
    {
        if (bulletObj)
        {
            GameObject temp = (GameObject)Instantiate(bulletObj.gameObject, transform.position, transform.rotation);
            temp.GetComponent<Bullet>().SetBulletDamage(GetWeaponDamage());
            temp.GetComponent<Bullet>().SetBulletRange(bulletRange);
            temp.GetComponent<Bullet>().SetBulletSpeed(GetWeaponSpeed());
            temp.GetComponent<Bullet>().SetDirection(IsFacingRight());
            temp.GetComponent<Bullet>().SetWeaponType(GetWeaponType());
        }
    }
}
