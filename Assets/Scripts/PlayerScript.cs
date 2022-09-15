using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    //VARIABLES FOR MOVEMENT
    public float xSpeed;
    public float ySpeed;
    public float accelerateSpeed;
    public float x;
    public float y;

    //VARIABLES FOR JUMP
    public float radius = 0.1f;
    public float jumpTimer;
    public float jumpPower;
    public float gravityScale;
    public Transform feet;
    public Transform jumpPlatform;
    public LayerMask layerMask;

    //VARIABLES FOR COMPONENTS
    public Rigidbody2D myRigidbody;

    //VARIABLES FOR BOOLEANS
    public bool facingRight;
    public bool canJump;
    public bool timerOn;

    // Start is called before the first frame update
    void Start()
    {
        myRigidbody = GameObject.Find("Player").GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButton("Jump") && jumpTimer == 0)
        {
            Jump();
        }

        JumpCheck();

    }

    void FixedUpdate()
    {
        Movement();
    }

    public void Movement()
    {
        x = Input.GetAxisRaw("Horizontal");
        y = Input.GetAxisRaw("Vertical");

        myRigidbody.velocity = new Vector2(x * xSpeed, myRigidbody.velocity.y);
        myRigidbody.velocity = new Vector2(myRigidbody.velocity.x, y * ySpeed);

        if(x != 0 || y != 0)
        {
            xSpeed += Time.deltaTime * accelerateSpeed;   
            if( xSpeed >= 6f)
            {
                xSpeed = 6f;
            }
        }

        else if ( x == 0 && y == 0)
        {
            xSpeed = 1f;
        }

        if(x > 0.01f && facingRight == false)
        {
            facingRight = !facingRight;
            transform.localScale = new Vector2(-transform.localScale.x, transform.localScale.y);
        }

        if (x < -0.01f && facingRight == true)
        {
            facingRight = !facingRight;
            transform.localScale = new Vector2(-transform.localScale.x, transform.localScale.y);

        }

    }

    public void Jump()
    {
        myRigidbody.gravityScale = gravityScale;
        myRigidbody.AddForce(Vector2.up * jumpPower);

        timerOn = true;
    }

    public void JumpCheck()
    {
        canJump = Physics2D.OverlapCircle(feet.position, radius, layerMask);

        if (timerOn == true)
        {
            jumpTimer += Time.deltaTime;

            if (jumpTimer >= 1.5f)
            {
                timerOn = false;
                jumpTimer = 0;
                myRigidbody.gravityScale = 0f;
            }
        }
    }

}
