using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private GridLayout m_Grid;
    private Vector3 m_Direction;

    void Awake()
    {
        GameManager.Instance.OnGameReset.AddListener(Intialize);
    }

    // Use this for initialization
    void Start()
    {
        Intialize();
    }

    void Intialize()
    {
        if (m_Grid == null)
            m_Grid = GameObject.FindGameObjectWithTag("GridLayout").GetComponent<GridLayout>();

        if (m_Grid.OutOfBounds(transform.position))
            m_Grid.SetPositionOnGrid(transform, Vector3.zero);

        transform.position = new Vector3(Mathf.RoundToInt(transform.position.x), transform.position.y, Mathf.RoundToInt(transform.position.z));
    }
    // Update is called once per frame
    void Update()
    {
        if (GameManager.Instance.IsPaused)
            return;

        Movement();
    }

    void Movement()
    {
        if (Input.GetKeyDown(KeyCode.W))
            m_Grid.MoveOnGrid(transform, GridLayout.Direction.Up, Platform.PlatformType.Player);

        if (Input.GetKeyDown(KeyCode.S))
            m_Grid.MoveOnGrid(transform, GridLayout.Direction.Down, Platform.PlatformType.Player);

        if (Input.GetKeyDown(KeyCode.D))
            m_Grid.MoveOnGrid(transform, GridLayout.Direction.Right, Platform.PlatformType.Player);

        if (Input.GetKeyDown(KeyCode.A))
            m_Grid.MoveOnGrid(transform, GridLayout.Direction.Left, Platform.PlatformType.Player);
    }
}
