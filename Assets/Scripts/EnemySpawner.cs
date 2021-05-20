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
                        GameObject temp = Instantiate(enemies[Random.Range(0, enemies.Length)]);

                        Vector3 xy = grid.GetEnemyGrid()[Random.Range(0, grid.GetEnemyGrid().Length)].transform.position;

                        if (grid.GetPlatform(xy).IsOccupied)
                        {
                            Vector3 oh = xy;
                            while (oh == xy)
                                xy = grid.GetEnemyGrid()[Random.Range(0, grid.GetEnemyGrid().Length)].transform.position;
                        }

                        temp.transform.position = new Vector3(xy.x, temp.transform.position.y, 2);
                        //grid.GetEnemyPlatform(xy).SetPlatformType(Platform.PlatformType.Occupied);
                        totalEnemies[i] = temp;
                    }
                }
            }
        }
        else
            Debug.LogError("Number of enemies error");
    }
}
