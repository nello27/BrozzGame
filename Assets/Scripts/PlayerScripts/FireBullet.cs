using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBullet : MonoBehaviour
{
    // Velocidad a la que se mover� la bala
    private float speed = 10f;

    // Referencia al componente Animator del objeto
    private Animator anim;

    // Variable para controlar si la bala puede moverse o no
    private bool canMove;

    void Awake()
    {
        // Obtener la referencia al componente Animator
        anim = GetComponent<Animator>();
    }

    void Start()
    {
        // Al iniciar, la bala puede moverse y se desactivar� despu�s de 5 segundos
        canMove = true;
        StartCoroutine(DisableBullet(5f));
    }

    void Update()
    {
        // M�todo para mover la bala
        Move();
    }

    // M�todo para mover la bala
    void Move()
    {
        if (canMove)
        {
            // Mover la bala en la direcci�n del eje X
            Vector3 temp = transform.position;
            temp.x += speed * Time.deltaTime;
            transform.position = temp;
        }
    }

    // Propiedad para obtener y establecer la velocidad de la bala
    public float Speed
    {
        get { return speed; }
        set { speed = value; }
    }

    // M�todo para desactivar la bala despu�s de cierto tiempo
    IEnumerator DisableBullet(float timer)
    {
        yield return new WaitForSeconds(timer);
        gameObject.SetActive(false);
    }

    // M�todo que se llama cuando la bala colisiona con otro objeto
    void OnTriggerEnter2D(Collider2D collision)
    {
        // Si la bala colisiona con un objeto etiquetado como "Beetle", "Snail" o "Spider"
        if (collision.gameObject.tag == MyTags.BEETLE_TAG ||
            collision.gameObject.tag == MyTags.SNAIL_TAG ||
            collision.gameObject.tag == MyTags.SPIDER_TAG || collision.gameObject.tag == MyTags.BOSS_TAG)
        {
            // Reproducir la animaci�n "Explode"
            anim.Play("Explode");

            // La bala ya no puede moverse
            canMove = false;

            // Desactivar la bala despu�s de 0.9 segundos
            StartCoroutine(DisableBullet(0.9f));
        }
    }
}
