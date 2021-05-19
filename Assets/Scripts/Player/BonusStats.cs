using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class BonusStats : MonoBehaviour
{

    public int damageBonus;
    public int healthBonus;
    public float defenseBonus;
    public GameObject bonusScreen;


    public static float armourB;
    public static int damageB, healthB;
    // Use this for initialization
    void Start()
    {
        bonusScreen.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
    }

    public int GetBonusHealth()
    {
        return healthBonus;
    }

    public void IncreaseHealth()
    {
        healthB += 10;
    }

    public void IncreaseDamage()
    {
        damageB += 10;
    }

    public void IncreaseDefense()
    {
        armourB += 10;
    }

}
