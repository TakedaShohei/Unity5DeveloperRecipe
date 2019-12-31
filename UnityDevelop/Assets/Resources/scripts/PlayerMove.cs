using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public float speed = 0.5f;
    public float jump_speed = 0.5f;
    public float gravity = 0.5f;
    private Vector3 move_direction = Vector3.zero;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //  player moves
        CharacterController controller_ = GetComponent<CharacterController>();

        if (controller_.isGrounded)
        {
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
