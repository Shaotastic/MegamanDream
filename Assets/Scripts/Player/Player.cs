using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour
{
    public Weapon weapon;
    public GridLayout grid;
    public Vector3 gridPosition;
    public Weapon.Direction direction = Weapon.Direction.Right;
    public bool facingRight;

    //Vector3 pos;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
            weapon.Attack();

        Direction();
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


}
