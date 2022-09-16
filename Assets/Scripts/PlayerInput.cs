using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{

    //VARIABLES FOR INPUT
    public float horizontalMove;
    public float verticalMove;

    [SerializeField] private PlayerController playerController;

    // Start is called before the first frame update
    void Start()
    {
        playerController = GetComponent<PlayerController>();
    }


    public void Update()
    {
        horizontalMove = Input.GetAxisRaw("Horizontal");
        verticalMove = Input.GetAxisRaw("Vertical");
    }

    public void FixedUpdate()
    {
        playerController.Move(horizontalMove, verticalMove, false);
    }
}
