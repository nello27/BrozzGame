using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalScript : MonoBehaviour
{
    private GameObject player;
    public GameObject portalBlue;
    public GameObject portalBlack;
    public GameObject portalYellow;
    public Rigidbody2D rb;
    public float pushForce = 10f; // Fuerza de empuje en el eje X
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag(MyTags.PLAYER_TAG);
        rb = player.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Verifica que el objeto que colisiona tiene el tag del jugador
        if (collision.gameObject.tag == MyTags.PLAYER_TAG)
        {

            // Obtén el nombre del objeto que tiene este script
            string thisObjectName = gameObject.name;

            if (thisObjectName == "portalBlue") {

                // Teletransporta el objeto a la posición del portal negro
                collision.gameObject.transform.position = portalBlack.transform.position;


                rb.AddForce(Vector2.up * pushForce, ForceMode2D.Impulse); // Aplica fuerza hacia arriba

            }

            if (thisObjectName == "portalBlack")
            {

                // Teletransporta el objeto a la posición del portal negro
                collision.gameObject.transform.position = portalYellow.transform.position;


                rb.AddForce(Vector2.up * pushForce, ForceMode2D.Impulse); // Aplica fuerza hacia arriba

            }



        }
    }



}
