using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class StoneScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Invoke("Desactive", 4f);
    }

    void Desactive() { 
        
        gameObject.SetActive(false);
    
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == MyTags.PLAYER_TAG) { 
            
            collision.GetComponent<PlayerDamage>().DealDamage();
            gameObject.SetActive(false);
        
        }

        
    }

}
