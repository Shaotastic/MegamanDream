using UnityEngine;
using System.Collections;

abstract public class Weapon : MonoBehaviour
{
    bool facingRight;
    public enum Direction { Left, Right };

    public enum WeaponType { Player = 0, Enemy }

    Direction direction;

    WeaponType type;

    int weaponDamage;

    float attackSpeed;
    private int bonusDamage;

    abstract public void Attack();

    virtual public void SetWeaponName(string temp)
    {
        name = temp;
    }

    virtual public void SetWeaponDirection(Direction dir)
    {

        direction = dir;
    }

    virtual public Direction GetWeaponDirection()
    {
        return direction;
    }

    virtual public void FacingRight(bool res)
    {
        facingRight = res;
    }

    virtual public bool IsFacingRight()
    {
        return facingRight;
    }

    virtual public void SetWeaponType(WeaponType weapon)
    {
        type = weapon;
    }

    public WeaponType GetWeaponType()
    {
        return type;
    }

    public void SetWeaponDamage(int damgage)
    {
        weaponDamage = damgage;
    }

    public int GetWeaponDamage()
    {
        return weaponDamage * ((100 + bonusDamage) / 100);
    }

    public void SetWeaponSpeed(float speed)
    {
        attackSpeed = speed;
    }

    public float GetWeaponSpeed()
    {
        return attackSpeed;
    }

    public void SetBonusDamage(int x)
    {
        bonusDamage = x;
    }

    public int GetBonusDamage()
    {
        return bonusDamage;
    }

    //Maybe durability on weapons you find in the game world 
}
