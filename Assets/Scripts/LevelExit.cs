using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelExit : MonoBehaviour
{
   [SerializeField] float levelLoadDelay=2f;
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player"){

        
             StartCoroutine(LoadNextLevel() );   
        }
        // if (other.gameObject.tag == "Exit"){
        //     Debug.Log("OOOP");
        //}
    }
    IEnumerator LoadNextLevel(){
        yield return new WaitForSecondsRealtime(levelLoadDelay);
        var currScene=SceneManager.GetActiveScene().buildIndex;
        Debug.Log("WWWWWWWWWWWWWW");
        if (currScene==SceneManager.sceneCountInBuildSettings){currScene=0;}
        SceneManager.LoadScene(currScene+1);  
     }
}
