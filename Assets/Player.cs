using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed = 12f;
    public float gravity = -9.81f * 2;
    public float jumpHeight = 3f;

    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;
    Rigidbody2D rigi;

    Vector3 velocity;

    bool isGrounded;
    private void Awake()
    {
        rigi = GetComponent<Rigidbody2D>();
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        IsGrounded();
        Movement();
    }

    void Movement()
    {

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        float x = Input.GetAxis("Horizontal");

        //right is the red Axis, foward is the blue axis
        velocity.x = x * speed;

        //check if the player is on the ground so he can jump
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            //the equation for jumping
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        velocity.y += gravity * Time.deltaTime;

        rigi.velocity = velocity;
    }

    void IsGrounded()
    {
        RaycastHit2D hit = Physics2D.Raycast(groundCheck.position, Vector2.down, groundDistance, groundMask);
        if (hit.collider != null)
        {
            isGrounded = true;
        }
        else if (hit.collider == null)
        {
            isGrounded = false;
        }

    }
}
