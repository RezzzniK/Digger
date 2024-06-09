using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements.Experimental;

public class Bullet : MonoBehaviour
{
    Rigidbody2D myBulletRigid;
    [SerializeField] float bulletSpeed=20f;
    PlayerInput playerMovement;
    float xSpeed;
     void Start()
    {
        myBulletRigid=GetComponent<Rigidbody2D>();   
        playerMovement=FindAnyObjectByType<PlayerInput>();  
        xSpeed=playerMovement.transform.localScale.x*bulletSpeed;
    }


    void Update()
    {
        myBulletRigid.velocity=new Vector2(xSpeed,0f);   
    }

    void OnTriggerEnter2D(Collider2D other){
        if(other.tag=="Enemy")  {
            Debug.Log("Enemy Hit");
             Destroy(other.gameObject);
        } else if(other.tag=="Spikes")
        {
             Destroy(other.gameObject);
        }
         //Destroy(gameObject);
    }
    void OnCollisionEnter2D(Collision2D other) {
        if(other.gameObject.tag=="Ground"){
            Destroy(gameObject);
        }
    }
}
