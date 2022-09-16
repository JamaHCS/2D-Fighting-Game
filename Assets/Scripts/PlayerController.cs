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

    //VARIABLES FOR JUMP
    public float radius = 0.1f;
    public float jumpPower;
    public Transform feet;
    public Transform jumpPlatform;
    public LayerMask layerMask;

    //public float gravityScale;
    //public float jumpTimer;

    public Rigidbody2D myRigidbody;
    public Animator myAnimator;

    [SerializeField] bool canMove = true;
    [SerializeField] bool canJump;
    [SerializeField] bool facingRight = true;

    [Range(0, 1.0f)]
    [SerializeField] float movementSmooth = 0.5f;
    public Vector3 velocity = Vector3.zero;

    public void Awake()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
        myAnimator = GetComponentInChildren<Animator>();
    }

    public void Update()
    {
        
        horizontalMove = Input.GetAxisRaw("Horizontal");
        verticalMove = Input.GetAxisRaw("Vertical");

        Jump();

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

            else if(xMove < 0 && facingRight)
            {
                Flip();
            }
        }
    }

    public void Jump()
    {
        canJump = Physics2D.OverlapCircle(feet.position, radius, layerMask);

        if(!canJump)
        {
            canMove = false;
        }

        else
        {
            canMove = true;
        }

        if(Input.GetButtonDown("Jump") && canJump == true)
        {
            print("hyppään");
            myAnimator.SetTrigger("isJumping");
        }

        else
        {
            print("olen maassa");
            myAnimator.SetBool("Jumping", false);
        }


    }

    public void Flip()
    {
        facingRight = !facingRight;
        transform.Rotate(0, 180, 0);
    }
}
