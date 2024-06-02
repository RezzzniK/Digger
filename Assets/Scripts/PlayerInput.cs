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
    // bool ontheGround;
    CapsuleCollider2D colider;
    // LayerMask layers;
    [SerializeField] float movementSpeed = 1f;
    [SerializeField] float jumpSpeed = 5f;
    // [SerializeField] float gravity=1f;
    void Start()
    {
        playerRigedBody = GetComponent<Rigidbody2D>();
        clipStates = GetComponent<Animator>();
        colider = GetComponent<CapsuleCollider2D>();

    }


    void Update()
    {
        Run();
        FlipSprite();
        OnLadder();

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
        // Debug.Log(LayerMask.GetMask("Ground"));
        if (colider.IsTouchingLayers(LayerMask.GetMask("Ground")))
        {

            if (value.isPressed)
            {
                playerRigedBody.velocity += new Vector2(0f, jumpSpeed);
            }
        }
        // else if(colider.IsTouchingLayers(LayerMask.GetMask("Ladder"))){
        //      if (value.isPressed )     {
        //         Debug.Log("LADDER");
        //         playerRigedBody.velocity += new Vector2(0f, jumpSpeed*15);
        //     }
        // }
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
    void OnLadder()
    {
        if (colider.IsTouchingLayers(LayerMask.GetMask("Ladder")))
        {
            playerRigedBody.gravityScale = 0;
            Vector2 playerYVelocity = new Vector2(playerRigedBody.velocity.x, movingInput.y * movementSpeed);
            playerRigedBody.velocity = playerYVelocity;
            bool playerVerticalSpeed = Mathf.Abs(playerRigedBody.velocity.y) > Mathf.Epsilon;
            clipStates.SetBool("isClimbing", playerVerticalSpeed);

        }
        else
        {
            playerRigedBody.gravityScale = 5;
        }
    }


}
