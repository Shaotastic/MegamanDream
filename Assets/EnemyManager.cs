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

            Vector3 position = Vector3.zero;

            do
            {
                position = m_GridLayout.RandomEnemyPlatform().transform.position;
            } while (m_GridLayout.GetEnemyPlatform(position).IsOccupied);
            m_GridLayout.GetEnemyPlatform(position).OccupyPlatform();
            temp.transform.position = new Vector3(position.x, temp.transform.position.y, position.z);
            m_ActiveEnemies.Add(temp);
        }
    }
}
