using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBullet : MonoBehaviour
{
    // Velocidad a la que se moverá la bala
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
        // Al iniciar, la bala puede moverse y se desactivará después de 5 segundos
        canMove = true;
        StartCoroutine(DisableBullet(5f));
    }

    void Update()
    {
        // Método para mover la bala
        Move();
    }

    // Método para mover la bala
    void Move()
    {
        if (canMove)
        {
            // Mover la bala en la dirección del eje X
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

    // Método para desactivar la bala después de cierto tiempo
    IEnumerator DisableBullet(float timer)
    {
        yield return new WaitForSeconds(timer);
        gameObject.SetActive(false);
    }

    // Método que se llama cuando la bala colisiona con otro objeto
    void OnTriggerEnter2D(Collider2D collision)
    {
        // Si la bala colisiona con un objeto etiquetado como "Beetle", "Snail" o "Spider"
        if (collision.gameObject.tag == MyTags.BEETLE_TAG ||
            collision.gameObject.tag == MyTags.SNAIL_TAG ||
            collision.gameObject.tag == MyTags.SPIDER_TAG || collision.gameObject.tag == MyTags.BOSS_TAG)
        {
            // Reproducir la animación "Explode"
            anim.Play("Explode");

            // La bala ya no puede moverse
            canMove = false;

            // Desactivar la bala después de 0.9 segundos
            StartCoroutine(DisableBullet(0.9f));
        }
    }
}
