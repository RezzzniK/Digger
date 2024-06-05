using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;
public class EnemyMovement : MonoBehaviour
{
    [SerializeField] float enemySpeed=1f;
    Rigidbody2D enemyRigedbody;
    BoxCollider2D enemyColider;
    Transform enemyTransform;
  
    void Start()
    {
        enemyRigedbody=GetComponent<Rigidbody2D>();
        enemyColider=GetComponent<BoxCollider2D>();
    }


    void Update()
    {
        enemyRigedbody.velocity = new Vector2(enemySpeed,0);

    }
    /// <summary>
    /// Sent when a collider on another object stops touching this
    /// object's collider (2D physics only).
    /// </summary>
    /// <param name="other">The Collision2D data associated with this collision.</param>

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.tag == "Ground"){
            Debug.Log(other.gameObject.tag);
            transform.localScale= new Vector2((-1)*enemyRigedbody.velocity.x, 1f);
            enemySpeed*=-1;
        }
    }


    // void OnTExit2D(Collision2D other)
    // {
    //     if (other.gameObject.tag == "Ground"){
    //         Debug.Log("EXIT FROM");
    //         transform.localScale= new Vector2(Mathf.Sign(enemyRigedbody.velocity.x), 1f);
    //     }
    // }
}
