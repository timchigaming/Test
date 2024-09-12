using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController2 : MonoBehaviour
{
    public float speed = 6.0f;
    public float jumpSpeed = 8.0f;
    public float gravity = 20.0f;
    private CharacterController controller;

    // Start is called before the first frame update
    void Start()
    {
        if(controller == null)
        {
            controller = GetComponent<CharacterController>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 velocity = controller.velocity;
        velocity.x = Input.GetAxis("Horizontal") * speed;
        velocity.z = Input.GetAxis("Vertical") * speed; 
        if(controller.isGrounded)
        {
            if(Input.GetButtonDown("Jump"))
            {
                velocity.y = jumpSpeed;
            }
        }
        velocity.y -=    gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }
}
