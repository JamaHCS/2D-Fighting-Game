using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //VARIABLES FOR INPUT
    public float horizontalMove;
    public float verticalMove;

    //VARIABLES FOR MOVEMENT
    [SerializeField] private float xSpeed = 10f;
    [SerializeField] private float ySpeed = 6f;

    public Rigidbody2D myRigidbody;

    [SerializeField] bool canMove = true;
    [SerializeField] bool facingRight = true;

    [Range(0, 1.0f)]
    [SerializeField] float movementSmooth = 0.5f;
    public Vector3 velocity = Vector3.zero;

    public void Awake()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
    }

    public void Update()
    {
        horizontalMove = Input.GetAxisRaw("Horizontal");
        verticalMove = Input.GetAxisRaw("Vertical");
    }

    public void FixedUpdate()
    {
        Move(horizontalMove, verticalMove, false);
    }

    public void Move(float xMove, float yMove, bool jump)
    {
        if(canMove)
        {
            Vector3 targetVelocity = new Vector2(xMove * xSpeed, yMove * ySpeed);

            myRigidbody.velocity = Vector3.SmoothDamp(myRigidbody.velocity, targetVelocity, ref velocity, movementSmooth);

            // FLIP AND ROTATE CHARACTER TO FACE TO RIGHT DIRECTION WHEN MOVING
            if(xMove > 0  && !facingRight)
            {
                Flip();
            }

            else if(xMove < 0 &&facingRight)
            {
                Flip();
            }
        }
    }

    public void Flip()
    {
        facingRight = !facingRight;
        transform.Rotate(0, 180, 0);
    }
}
