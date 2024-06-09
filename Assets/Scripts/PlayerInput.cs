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
    BoxCollider2D feetColider;
    bool isAlive;
    // LayerMask layers;
    [SerializeField] float movementSpeed = 1f;
    [SerializeField] float jumpSpeed = 5f;
    [SerializeField] Vector2 deathKick;
    [SerializeField] GameObject bullet;
    [SerializeField] Transform gun;
    float startGravityScale;
    
    void Start()
    {
        playerRigedBody = GetComponent<Rigidbody2D>();
        clipStates = GetComponent<Animator>();
        colider = GetComponent<CapsuleCollider2D>();
        feetColider = GetComponent<BoxCollider2D>();
        startGravityScale = playerRigedBody.gravityScale;
        isAlive = true;
        //bullet=FindAnyObjectByType<GameObject>();
    }


    void Update()
    {
        if (!isAlive) { return; }

        Run();
        FlipSprite();
        OnLadder();
        Die();

    }
    void OnMove(InputValue value)
    {
        if (!isAlive) { return; }

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
        if (!isAlive) { return; }

        // Debug.Log(LayerMask.GetMask("Ground"));
        if (feetColider.IsTouchingLayers(LayerMask.GetMask("Ground")))
        {
            //Debug.Log("FEET TOUCHIN THE GROUND");

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
    void OnFire(InputValue inputValue){
        if (!isAlive) { return; }
        // if (inputValue.isPressed){
        //     Debug.Log("SFDFFF");
        // }
        Instantiate(bullet,gun.position,transform.rotation);
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
        if (feetColider.IsTouchingLayers(LayerMask.GetMask("Ladder")))
        {
            playerRigedBody.gravityScale = 0f;
            Vector2 playerYVelocity = new Vector2(playerRigedBody.velocity.x, movingInput.y * movementSpeed);
            playerRigedBody.velocity = playerYVelocity;
            bool playerVerticalSpeed = Mathf.Abs(playerRigedBody.velocity.y) > Mathf.Epsilon;
            clipStates.SetBool("isClimbing", playerVerticalSpeed);

        }
        else
        {
            playerRigedBody.gravityScale = startGravityScale;
        }
    }

    void Die()
    {
        if (playerRigedBody.IsTouchingLayers(LayerMask.GetMask("Enemy","Spikes")) )//|| playerRigedBody.IsTouchingLayers(LayerMask.GetMask("Spikes")))
        {
            isAlive = false;
            playerRigedBody.velocity += deathKick;
            clipStates.SetTrigger("Dying");
            Debug.Log("Game Over");
        }
    }



}
