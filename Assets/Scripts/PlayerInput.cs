using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PlayerInput : MonoBehaviour
{
    Vector2 movingInput;
    Rigidbody2D playerRigedBody;
    //[SerializeField] Animator clipStates;
    Animator clipStates;
    [SerializeField] float movementSpeed = 1f;
    [SerializeField] float jumpSpeed = 5f;
    // [SerializeField] float gravity=1f;
    void Start()
    {
        playerRigedBody = GetComponent<Rigidbody2D>();
        clipStates = GetComponent<Animator>();

    }


    void Update()
    {
        Run();
        FlipSprite();
    }
    void OnMove(InputValue value)
    {
        movingInput = value.Get<Vector2>();
        Debug.Log(movingInput);
        //  if(movingInput.x>=0){
        //     playerScale.localScale*=1;
        // }else{
        //     playerScale.localScale=new ;
        // }

    }
    void OnJump(InputValue value)
    {
        if (value.isPressed)
        {
            playerRigedBody.velocity += new Vector2(0f, jumpSpeed);
        }
    }
    void Run()
    {

        Vector2 playerVelocity = new Vector2(movingInput.x * movementSpeed, playerRigedBody.velocity.y);
        playerRigedBody.velocity = playerVelocity;
    }
    void FlipSprite()
    {
        bool playerHorizontalSpeed = Mathf.Abs(playerRigedBody.velocity.x) > Mathf.Epsilon;
        clipStates.SetBool("isRunning", playerHorizontalSpeed);
        if (playerHorizontalSpeed)
        {

            transform.localScale = new Vector2(Mathf.Sign(playerRigedBody.velocity.x), 1f);

        }

    }

}
