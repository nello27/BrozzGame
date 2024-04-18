using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformScript : MonoBehaviour
{
    public GameObject Player;
    public GameObject PlatformParent;

    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.FindWithTag(MyTags.PLAYER_TAG);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == MyTags.PLAYER_TAG) {

            // Supongamos que tienes dos GameObjects: parentObject y childObject
            Player.transform.SetParent(PlatformParent.transform);


        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == MyTags.PLAYER_TAG)
        {

            
            Player.transform.SetParent(null);


        }
    }

}
