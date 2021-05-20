using UnityEngine;

public class Platform : MonoBehaviour
{
    public enum PlatformType 
    { 
        Player, 
        Enemy, 
    };

    public enum PlatformSpace
    {
        Free = 0,
        Occupied
    }

    public Vector3 position;

    [SerializeField] private PlatformType platform;
    [SerializeField] private PlatformSpace m_Space;
    [SerializeField] private MeshRenderer m_MeshRenderer;
    [SerializeField] private bool disabled;

    public bool IsOccupied { get => (m_Space == PlatformSpace.Occupied); }

    // Use this for initialization
    void Start()
    {
        m_MeshRenderer = GetComponent<MeshRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (disabled)
           m_MeshRenderer.enabled = false;
        else
            m_MeshRenderer.enabled = true;
    }

    public void SetPosition(int x, int y)
    {
        position.x = x;
        position.y = y;
    }

    public Vector3 GetPosition()
    {
        return position;
    }

    public void PlatformEnabled(bool dib)
    {
        disabled = dib;
    }

    public bool GetDisabledPlatform()
    {
        return disabled;
    }

    public void SetPlatformSpace(PlatformSpace plat)
    {
        m_Space = plat;
    }

    public void Initialize(PlatformType plat)
    {
        platform = plat;

        switch (plat)
        {
            case PlatformType.Player:
                m_MeshRenderer.material.color = Color.blue;
                break;

            case PlatformType.Enemy:
                m_MeshRenderer.material.color = Color.red;
                break;
            default:
                m_MeshRenderer.material.color = Color.white;
                break;
        }
    }

    public PlatformType GetPlatformType()
    {
        return platform;
    }

    public bool SamePlatformType(PlatformType plat)
    {
        return (platform == plat);
    }

    public float GetXScale()
    {
        return transform.localScale.x;
    }

    public float GetZScale()
    {
        return transform.localScale.z;
    }

    public void OccupyPlatform()
    {
        m_Space = PlatformSpace.Occupied;
    }
}
