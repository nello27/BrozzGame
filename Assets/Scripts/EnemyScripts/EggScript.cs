using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EggScript : MonoBehaviour
{

    private void OnCollisionEnter2D(Collision2D collision)
    {
        
        if (collision.gameObject.tag == MyTags.PLAYER_TAG) { 
            
            //damage the player
            collision.gameObject.GetComponent<PlayerDamage>().DealDamage();
        }

        gameObject.SetActive(false);

    }

}
