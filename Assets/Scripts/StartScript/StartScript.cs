using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class StartScript : MonoBehaviour
{
    private Animator start;

    // Start is called before the first frame update
    void Start()
    {
        start = GetComponent<Animator>();


    }

    // Update is called once per frame
    void Update()
    {

    }

    void StartAnimationStop()
    {
        start.speed = 0f;
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            // Obtener el GameObject que tiene el componente PlayerShoot
            GameObject playerObject = GameObject.FindGameObjectWithTag("Player");
            // Obtener el componente PlayerShoot del GameObject del jugador
            PlayerShoot playerShoot = playerObject.GetComponent<PlayerShoot>();
            // Activar el booleano activeBullet en el componente PlayerShoot
            playerShoot.activeBullet = true;
        }
    }
}