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
        //　CharacterControllerのコライダで接地が確認出来ない場合
        if (!controller_.isGrounded)
        {

            if (Physics.Linecast(ray_transform.position, (ray_transform.position - transform.up * ray_range)))
            {
                isGround_ = true;
            }
            else
            {
                isGround_ = false;
            }

            //　接地確認用にレイを視覚化
            Debug.DrawLine(ray_transform.position, (ray_transform.position - transform.up * ray_range), Color.red);

        }

        if (controller_.isGrounded || isGround_)
        {
            //　地面に接地してる時は速度を初期化
            if (controller_.isGrounded)
            { //　レイを飛ばして接地確認の場合は重力だけは働かせておく、前後左右は初期化
                move_direction = Vector3.zero;
            } else {
                move_direction = new Vector3(0f, move_direction.y, 0f);

            }


            move_direction = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
            move_direction = transform.TransformDirection(move_direction);
            move_direction *= speed;
            if (Input.GetButton("Jump"))
            {
                move_direction.y = jump_speed;
            }

            move_direction.y = gravity * Time.deltaTime;
            controller_.Move(move_direction * Time.deltaTime);
        }
    }
  
}
