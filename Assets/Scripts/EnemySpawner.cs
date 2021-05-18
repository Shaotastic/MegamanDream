using UnityEngine;
using System.Collections;

public class EnemySpawner : MonoBehaviour
{
    public int numberOfEnemies;

    public GameObject[] enemies;

    GridLayout grid;

    public GameObject[] totalEnemies;

    public static int EnemyCount = 0;

    void Awake()
    {
        ///if (GameManager.reset)
        ///{
        ///    Intialize();
        ///}
    }
    // Use this for initialization
    void Start()
    {
        Intialize();
    }

  void Intialize()
    {
        if (GameManager.Instance.enemyCount == -1)
            GameManager.Instance.enemyCount = 0;
        grid = GameObject.FindGameObjectWithTag("GridLayout").GetComponent<GridLayout>();
        totalEnemies = new GameObject[numberOfEnemies];

        if (numberOfEnemies >= 1)
        {
            if (enemies != null)
            {
                if (enemies.Length == 0)
                    Debug.LogError("There are no enemies to spawn");
                else
                {
                    for (int i = 0; i < numberOfEnemies; ++i)
                    {
                        int x = Random.Range(4, 6);
                        int y = Random.Range(1, 3);
                        GameObject temp = Instantiate(enemies[Random.Range(0, enemies.Length)]);

                        Vector3 xy = grid.GetEnemyGrid()[Random.Range(0, grid.GetEnemyGrid().Length)].transform.position;

                        if (grid.GetEnemyPlatform(xy).GetPlatformType() == Platform.PlatformType.Occupied)
                        {
                            Vector3 oh = xy;
                            while (oh == xy)
                                xy = grid.GetEnemyGrid()[Random.Range(0, grid.GetEnemyGrid().Length)].transform.position;
                        }

                        temp.transform.position = new Vector3(xy.x, temp.transform.position.y, 2);
                        //grid.GetEnemyPlatform(xy).SetPlatformType(Platform.PlatformType.Occupied);
                        totalEnemies[i] = temp;
                        GameManager.Instance.enemyCount++;
                    }
                }
            }
        }
        else
            Debug.LogError("Number of enemies error");
    }
}
