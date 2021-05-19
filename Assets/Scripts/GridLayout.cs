//The Grid class will also create sections but thats for later.
using UnityEngine;
using System.Collections;

public class GridLayout : MonoBehaviour
{
    public Platform obj;          //The object to use to make a grid with 

    public int x, y;                //

    public float spread = 1;        //How far apart you want each piece 

    public bool divideGrid;

    Platform[,] objArr;
    public Platform[] m_EnemyGrid;

    int xSize, ySize;

    // Use this for initialization
    void Awake()
    {
        if (x <= 0 || y <= 0)
            x = y = 1;

        xSize = CheckValue(x);
        ySize = y;

        m_EnemyGrid = new Platform[(xSize / 2) * ySize];

        if (obj != null)
        {
            GenerateGridArray(obj); //Generates a grid using the 2d array
        }
    }

    public bool CheckPlatformNorth(Vector3 position, Platform.PlatformType plat)
    {
        if (position.z < (y - 1))
            if (!objArr[(int)position.x, (int)position.z + 1].GetDisabledPlatform())
                if (objArr[(int)position.x, (int)position.z + 1].GetPlatformType() == Platform.PlatformType.Free ||
                    objArr[(int)position.x, (int)position.z + 1].GetPlatformType() == plat)
                    return true;

        return false;
    }

    public bool CheckPlatformSouth(Vector3 position, Platform.PlatformType plat)
    {
        if (position.z > 0)
            if (!objArr[(int)position.x, (int)position.z - 1].GetDisabledPlatform())
                if (objArr[(int)position.x, (int)position.z - 1].GetPlatformType() == Platform.PlatformType.Free ||
                    objArr[(int)position.x, (int)position.z - 1].GetPlatformType() == plat)
                    return true;

        return false;
    }

    public bool CheckPlatformEast(Vector3 position, Platform.PlatformType plat)
    {
        if (position.x < (x - 1))
            if (!objArr[(int)position.x + 1, (int)position.z].GetDisabledPlatform())
                if (objArr[(int)position.x + 1, (int)position.z].GetPlatformType() == Platform.PlatformType.Free ||
                    objArr[(int)position.x + 1, (int)position.z].GetPlatformType() == plat)
                    return true;

        return false;
    }

    public bool CheckPlatformWest(Vector3 position, Platform.PlatformType plat)
    {
        if (position.x > 0)
            if (!objArr[(int)position.x - 1, (int)position.z].GetDisabledPlatform())
                if (objArr[(int)position.x - 1, (int)position.z].GetPlatformType() == Platform.PlatformType.Free ||
                    objArr[(int)position.x - 1, (int)position.z].GetPlatformType() == plat)
                    return true;

        return false;
    }

    public bool OutOfBounds(Vector3 position)
    {
        if (position.x >= (x - 1) || position.x < 0 || position.z >= (y - 1) || position.z < 0)
            return true;
        return false;
    }

    void GenerateGridArray(Platform tempObj)
    {
        objArr = new Platform[xSize, ySize];

        for (int i = 0; i < xSize; i++)
        {
            for (int j = 0; j < ySize; j++)
            {
                Platform temp = Instantiate(tempObj, transform);
                temp.SetPosition(i, j);
                temp.name = "Platform " + i + "x" + j;
                temp.transform.position = new Vector3(i * temp.GetXScale(), 0, j * temp.GetZScale());
                temp.PlatformEnabled(false);

                objArr[i, j] = temp;
            }
        }

        DivideGrid(divideGrid);
    }

    void DivideGrid(bool val)
    {
        int index = 0;
        for (int i = 0; i < xSize; i++)
        {
            for (int j = 0; j < ySize; j++)
            {
                if (val)
                {
                    if (i < xSize / 2)
                        objArr[i, j].Initialize(Platform.PlatformType.Player);

                    else
                    {
                        objArr[i, j].Initialize(Platform.PlatformType.Enemy);
                        m_EnemyGrid[index] = objArr[i, j];
                        index++;
                    }
                }
                else
                    objArr[i, j].Initialize(Platform.PlatformType.Free);
            }
        }
    }

    public Platform[] GetEnemyGrid()
    {
        return m_EnemyGrid;
    }

    public Platform GetEnemyPlatform(Vector3 vec)
    {
        return objArr[(int)vec.x, (int)vec.z];
    }

    public bool CheckColumnPlatforms(int v)
    {
        for (int i = 0; i < ySize; i++)
        {
            if (objArr[v, i].GetPlatformType() == Platform.PlatformType.Occupied)
                return false;
        }
        return true;
    }

    int CheckValue(int x)
    {
        int check = x % 2;

        if (check == 1)
            return x - 1;

        return x;
    }

    public Platform RandomEnemyPlatform()
    {
        return m_EnemyGrid[Random.Range(0, m_EnemyGrid.Length)];
    }

}