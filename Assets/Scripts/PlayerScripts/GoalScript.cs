using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalScript : MonoBehaviour
{
    public PlayerMove playerMovement; // Referencia al script PlayerMovement

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player") {

            playerMovement.enabled = false;

        }
    }

}
