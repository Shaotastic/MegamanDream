using UnityEngine;
using System.Collections;
using System;
using DG.Tweening;
public class Player : MonoBehaviour
{
    public Weapon weapon;
    public GridLayout grid;
    public Vector3 gridPosition;
    public Weapon.Direction direction = Weapon.Direction.Right;
    public bool facingRight;
    public Transform m_SKillWindow;

    public float m_PowerCooldown = 8;

    private float m_Timer;

    //Vector3 pos;
    bool m_Ready;
    bool m_Window;

    // Use this for initialization
    void Start()
    {
        GameManager.Instance.OnReadyForSkills.AddListener(ReadyForSkills);
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.K) && m_Ready && !m_Window)
        {
            OpenWindow();
        }

        if (Input.GetKeyDown(KeyCode.K) && m_Window)
        {
            CloseWindow();
        }


        if (GameManager.Instance.IsPaused)
            return;

        if (Input.GetKeyDown(KeyCode.Space))
            weapon.Attack();



        Direction();
    }

    private void OpenWindow()
    {
        m_SKillWindow.DOLocalMoveX(-160, 0.5f).OnComplete(() => { m_Window = true; });
        GameManager.Instance.OpenSkillWindow();
    }

    private void CloseWindow()
    {
        m_SKillWindow.DOLocalMoveX(-750, 0.5f).OnComplete(() => { m_Window = false; });
        GameManager.Instance.CloseSkill();
        m_Ready = false;
    }


    //Get the position of the grid by raycasting below the player to get there position, not really usefull :P 
    void Direction()
    {
        if (!facingRight)
            weapon.FacingRight(false);
        else
            weapon.FacingRight(true);
    }

    //Function to set the direction of the weapon 
    void ReadyForSkills()
    {
        m_Ready = true;
    }


}
