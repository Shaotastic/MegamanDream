using UnityEngine;
using UnityEngine.Events;
using System.Collections;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

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
        GameStart();
    }

    public void GameStart()
    {
        OnGameStart.Invoke();

    }
    public void Reset()
    {
        int scene = UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex;
        UnityEngine.SceneManagement.SceneManager.LoadScene(scene, UnityEngine.SceneManagement.LoadSceneMode.Single);
        level++;
    }

    internal void EndLevel()
    {
        OnLevelEnd.Invoke();
    }

}
