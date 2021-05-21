using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public static EnemyManager Instance;

    [SerializeField] private int m_NumberOfEnemies;

    [SerializeField] private List<Enemy> m_EnemyList;

    [SerializeField] private List<Enemy> m_ActiveEnemies;

    [SerializeField] GridLayout m_GridLayout;

    [SerializeField] List<Vector3> m_Points = new List<Vector3>();

    [SerializeField] int m_CurrentEnemyIndex = 0;

    System.Action action;
    private void Awake()
    {
        if (Instance == null)
            Instance = this;
    }

    private void Start()
    {
        Initialize();
    }

    private void Initialize()
    {
        m_GridLayout = GameObject.FindGameObjectWithTag("GridLayout").GetComponent<GridLayout>();

        m_ActiveEnemies = new List<Enemy>();

        for (int i = 0; i < m_NumberOfEnemies; i++)
        {
            int randomIndex = Random.Range(0, m_EnemyList.Count);

            Enemy temp = Instantiate(m_EnemyList[randomIndex], transform);

            Vector3 position;

            do
            {
                position = m_GridLayout.RandomEnemyPlatform().transform.position;
            } while (m_GridLayout.GetPlatform(position).IsOccupied);
            m_Points.Add(position);
            m_GridLayout.GetPlatform(position).OccupyPlatform();
            temp.transform.position = position;
            m_ActiveEnemies.Add(temp);
        }
        Thing();
    }

    private void Update()
    {

    }


    internal void Thing()
    {
        m_ActiveEnemies[m_CurrentEnemyIndex].EnableMovement();
    }

    internal void NextEnemy()
    {
        //m_ActiveEnemies[m_CurrentEnemyIndex].DisableMovement();
        

        if (m_CurrentEnemyIndex >= m_ActiveEnemies.Count - 1)
            m_CurrentEnemyIndex = 0;
        else
            m_CurrentEnemyIndex++;

        Thing();
    }

    //IN LIST ORDER
    //Enable the enemy to move,
    //After they attack, notify the manager that they attacked and end there turn
    //Move onto the next

}
