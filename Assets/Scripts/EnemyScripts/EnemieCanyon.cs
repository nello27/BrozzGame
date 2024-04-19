using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemieCanyon : MonoBehaviour
{
    public GameObject player;
    public LayerMask playerLayer;
    public LayerMask GroundLayer;
    public Animator Enemy;
    // Start is called before the first frame update
    void Start()
    {
        Enemy = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag(MyTags.PLAYER_TAG);
    }

    // Update is called once per frame
    void Update()
    {
        if (Physics2D.OverlapCircle(transform.position, 0.5f, playerLayer))
        {

            player.GetComponent<PlayerDamage>().DealDamage();

        }

        if (Physics2D.OverlapCircle(transform.position, 0.5f, GroundLayer))
        {

            Destroy(gameObject);

        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == MyTags.BULLET_TAG) {

            Enemy.enabled = false; // En lugar de Enemy.enabled = false;
            Rigidbody2D rb; //  Rigidbody2D para trabajar en un entorno 2D
            rb = GetComponent<Rigidbody2D>(); // Ontgenemod el componente Rigidbody2D

            //el operador &= se utiliza para eliminar la restricción FreezePositionY / El operador ~ se usa para invertir los bits de RigidbodyConstraints2D.FreezePositionY
            rb.constraints &= ~RigidbodyConstraints2D.FreezePositionY;



        }
    }
}
