using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private GridLayout grid;
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
        if (grid == null)
            grid = GameObject.FindGameObjectWithTag("GridLayout").GetComponent<GridLayout>();

        if (grid.OutOfBounds(transform.position))
            transform.position = new Vector3(0, transform.position.y, 0);

        transform.position = new Vector3(Mathf.RoundToInt(transform.position.x), transform.position.y, Mathf.RoundToInt(transform.position.z));
    }
    // Update is called once per frame
    void Update()
    {
        Movement();
    }

    void Movement()
    {
        m_Direction = Vector3.zero;

        if (Input.GetKeyDown(KeyCode.W) && grid.CheckPlatformNorth(transform.position, Platform.PlatformType.Player))
            m_Direction += Vector3.forward;

        if (Input.GetKeyDown(KeyCode.S) && grid.CheckPlatformSouth(transform.position, Platform.PlatformType.Player))
            m_Direction += Vector3.back;

        if (Input.GetKeyDown(KeyCode.D) && grid.CheckPlatformEast(transform.position, Platform.PlatformType.Player))
            m_Direction += Vector3.right;

        if (Input.GetKeyDown(KeyCode.A) && grid.CheckPlatformWest(transform.position, Platform.PlatformType.Player))
            m_Direction += Vector3.left;

        transform.position += m_Direction;
    }
}
