using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GoalScript : MonoBehaviour
{
    public GameObject Player;
    public Transform point;
    
    public string sceneName;

    private void Start()
    {
        Player = GameObject.FindWithTag(MyTags.PLAYER_TAG);
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == MyTags.PLAYER_TAG) {

            Player.transform.position = point.position;
            //Player.GetComponent<PlayerMove>().enabled = false;
            SceneManager.LoadScene(sceneName);

            


        }
    }

}
