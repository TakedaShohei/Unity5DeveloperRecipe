using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    //Edit player's characterController
    [SerializeField] CharacterController controller_ = null;

    //set player physics
    public float speed = 0.5f;
    public float jump_speed = 0.5f;
    public float gravity = 0.5f;
    public float down_speed = 0;
    private Vector3 move_direction = Vector3.zero;

    //for confirming isGround
    [SerializeField] private Transform ray_transform=null;
    [SerializeField] private float ray_range = 0.85f;
    private bool isGround_= false;


    // Start is called before the first frame update
    void Start()
    {
        //  player moves
        controller_ = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        move_direction = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        move_direction = transform.TransformDirection(move_direction);
        move_direction *= speed;
        if (Input.GetButton("Jump"))
        {
            move_direction.y = jump_speed;
            down_speed = 0;
        }

        if (transform.position.y > 0)
        {
            down_speed += gravity * Time.deltaTime;
            move_direction.y -= down_speed;
        }
        
        controller_.Move(move_direction * Time.deltaTime);
        PlayerRotate();
    }

    public void PlayerRotate()
    {
       
        transform.Rotate(0, Input.GetAxis("Horizontal"), 0);

        GameObject camera_parent = Camera.main.transform.parent.gameObject;
        
        if (Input.GetKey("q"))
        {
            int up_ = 1;
           camera_parent.transform.Rotate(up_, 0, 0);
        }
        if (Input.GetKey("z"))
        {
            int down_ = -1;
            camera_parent.transform.Rotate(down_, 0, 0);

        }
    }
  
}
