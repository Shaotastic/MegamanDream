using UnityEngine;
using UnityEngine.Events;
using System.Collections;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;


    public UnityEngine.UI.Text LevelText;

    public int enemyCount = 0;

    public int playerPoints;

    public int currentHealth = -1;

    public bool reset = false;

    public float damageBonus, armourBonus;

    public int level = 1;

    public UnityEvent OnGameStart;
    public UnityEvent OnLevelEnd;
    public UnityEvent OnGameReset;



    private void Awake()
    {
        if (Instance == null)
            Instance = this;
    }

    void Start()
    {
        LevelText.text = "Level: " + level.ToString();
    }

    public void Reset()
    {
        int scene = UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex;
        UnityEngine.SceneManagement.SceneManager.LoadScene(scene, UnityEngine.SceneManagement.LoadSceneMode.Single);
        reset = true;
        level++;
    }

}
