//The Grid class will also create sections but thats for later.
using UnityEngine;
using System.Collections;

public class GridLayout : MonoBehaviour
{
    public enum Direction
    {
        Up,
        Down,
        Left,
        Right
    }

    public static GridLayout Instance;

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
            if (!objArr[(int)position.x, (int)position.z + 1].GetDisabledPlatform() &&
                    !objArr[(int)position.x, (int)position.z + 1].IsOccupied &&
                        objArr[(int)position.x, (int)position.z + 1].SamePlatformType(plat))
                return true;

        return false;
    }

    public bool CheckPlatformSouth(Vector3 position, Platform.PlatformType plat)
    {
        if (position.z > 0)
            if (!objArr[(int)position.x, (int)position.z - 1].GetDisabledPlatform() &&
                    !objArr[(int)position.x, (int)position.z - 1].IsOccupied &&
                        objArr[(int)position.x, (int)position.z - 1].SamePlatformType(plat))
                return true;

        return false;
    }

    public bool CheckPlatformEast(Vector3 position, Platform.PlatformType plat)
    {
        if (position.x < (x - 1))
            if (!objArr[(int)position.x + 1, (int)position.z].GetDisabledPlatform() &&
                    !objArr[(int)position.x + 1, (int)position.z].IsOccupied &&
                        objArr[(int)position.x + 1, (int)position.z].SamePlatformType(plat))
                return true;

        return false;
    }

    public bool CheckPlatformWest(Vector3 position, Platform.PlatformType plat)
    {
        if (position.x > 0)
            if (!objArr[(int)position.x - 1, (int)position.z].GetDisabledPlatform() &&
                    !objArr[(int)position.x - 1, (int)position.z].IsOccupied &&
                        objArr[(int)position.x - 1, (int)position.z].SamePlatformType(plat))
                return true;

        return false;
    }

    //Needs 
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
            }
        }
    }

    public Platform[] GetEnemyGrid()
    {
        return m_EnemyGrid;
    }

    public Platform GetPlatform(Vector3 vec)
    {
        return objArr[(int)vec.x, (int)vec.z];
    }

    public bool CheckColumnPlatforms(int v)
    {
        for (int i = 0; i < ySize; i++)
        {
            if (objArr[v, i].IsOccupied)
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

    public void MoveOnGrid(Transform obj, Direction dir, Platform.PlatformType plat)
    {
        switch (dir)
        {
            case Direction.Up:
                if (CheckPlatformNorth(obj.position, plat))
                {
                    SetPlatformSpace(obj.position, Platform.PlatformSpace.Free);
                    obj.position += Vector3.forward;
                    SetPlatformSpace(obj.position, Platform.PlatformSpace.Occupied);
                }
                break;
            case Direction.Down:
                if (CheckPlatformSouth(obj.position, plat))
                {
                    SetPlatformSpace(obj.position, Platform.PlatformSpace.Free);
                    obj.position += Vector3.back;
                    SetPlatformSpace(obj.position, Platform.PlatformSpace.Occupied);
                }
                break;
            case Direction.Left:
                if (CheckPlatformWest(obj.position, plat))
                {
                    SetPlatformSpace(obj.position, Platform.PlatformSpace.Free);
                    obj.position += Vector3.left;
                    SetPlatformSpace(obj.position, Platform.PlatformSpace.Occupied);
                }
                break;
            case Direction.Right:
                if (CheckPlatformEast(obj.position, plat))
                {
                    SetPlatformSpace(obj.position, Platform.PlatformSpace.Free);
                    obj.position += Vector3.right;
                    SetPlatformSpace(obj.position, Platform.PlatformSpace.Occupied);
                }
                break;
        }
    }

    private void SetPlatformSpace(Vector3 position, Platform.PlatformSpace free)
    {
        objArr[(int)position.x, (int)position.z].SetPlatformSpace(free);
    }

    public void SetPositionOnGrid(Transform transform, Vector3 position)
    {
        if (!GetPlatform(position).IsOccupied)
            transform.position = GetPlatform(position).position;
    }
}