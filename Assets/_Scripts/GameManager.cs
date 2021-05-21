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

    public UnityEvent OnReadyForSkills;
    public UnityEvent OnOpenSkillWindow;
    public UnityEvent OnCloseSkills;
    public UnityEvent onUpdateSkillTimer;
    //ACTION VS EVENTS

    [SerializeField] float m_SkillTime = 8;
    [SerializeField] float m_SkillTimer = 0;

    [SerializeField] bool m_Pause;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
    }

    void Start()
    {
        GameStart();
    }

    private void Update()
    {
        Timer();
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

    internal void Timer()
    {
        if (m_SkillTimer <= m_SkillTime)
            m_SkillTimer += Time.deltaTime;
        else
        {
            OnReadyForSkills.Invoke();
        }
    }

    internal float GetSkillTime
    {
        get => (m_SkillTimer / m_SkillTime); 
    }
    public bool IsPaused { get => m_Pause; }

    internal void OpenSkillWindow()
    {
        OnOpenSkillWindow.Invoke();
        m_Pause = true;
    }

    internal void CloseSkill()
    {
        OnCloseSkills.Invoke();
        m_SkillTimer = 0;
        m_Pause = false;
    }
}
