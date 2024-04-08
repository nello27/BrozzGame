using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadGround : MonoBehaviour
{
    PlayerDamage dead;
    public GameObject Player;

    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.FindWithTag(MyTags.PLAYER_TAG);
        dead = Player.GetComponent<PlayerDamage>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == MyTags.PLAYER_TAG)
        {
            dead.DealDamage();
        }
    }
}
