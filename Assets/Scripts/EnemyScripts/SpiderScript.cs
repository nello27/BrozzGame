using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiderScript : MonoBehaviour
{
    // Referencia al componente Animator de la araña
    private Animator anim;

    // Referencia al componente Rigidbody2D de la araña
    private Rigidbody2D myBody;

    // Dirección inicial de movimiento de la araña
    private Vector3 moveDirection = Vector3.down;

    // Nombre del coroutine que controla el cambio de dirección de la araña
    private string coroutine_Name = "ChangeMovement";

    void Awake()
    {
        // Obtener las referencias a los componentes Animator y Rigidbody2D
        anim = GetComponent<Animator>();
        myBody = GetComponent<Rigidbody2D>();
    }

    // Método que se llama al iniciar
    void Start()
    {
        // Comenzar el coroutine para cambiar el movimiento de la araña
        StartCoroutine(coroutine_Name);
    }

    // Método que se llama en cada fotograma
    void Update()
    {
        // Método para mover la araña
        MoveSpider();
    }

    // Método para mover la araña
    void MoveSpider()
    {
        // Mover la araña en la dirección establecida
        transform.Translate(moveDirection * Time.smoothDeltaTime);
    }

    // Coroutine para cambiar la dirección de movimiento de la araña
    IEnumerator ChangeMovement()
    {
        // Esperar un tiempo aleatorio antes de cambiar la dirección
        yield return new WaitForSeconds(Random.Range(2f, 5f));

        // Cambiar la dirección de la araña
        if (moveDirection == Vector3.down)
        {
            moveDirection = Vector3.up;
        }
        else
        {
            moveDirection = Vector3.down;
        }

        // Reiniciar el coroutine para cambiar la dirección
        StartCoroutine(coroutine_Name);
    }

    // Coroutine para manejar la muerte de la araña
    IEnumerator SpiderDead()
    {
        // Esperar un tiempo antes de desactivar la araña
        yield return new WaitForSeconds(3f);
        gameObject.SetActive(false);
    }

    // Método que se llama cuando la araña colisiona con otro objeto
     void OnTriggerEnter2D(Collider2D collision)
    {
        // Si la araña colisiona con una bala
        if (collision.tag == MyTags.BULLET_TAG)
        {
            // Reproducir la animación de muerte de la araña
            anim.Play("SpiderDead");

            // Cambiar el tipo de cuerpo del Rigidbody2D para que la araña caiga
            myBody.bodyType = RigidbodyType2D.Dynamic;

            // Comenzar el coroutine para manejar la muerte de la araña
            StartCoroutine(SpiderDead());

            // Detener el coroutine para cambiar el movimiento de la araña
            StopCoroutine(coroutine_Name);
        }

        if (collision.tag == MyTags.PLAYER_TAG) {

            collision.GetComponent<PlayerDamage>().DealDamage();

        }
    }
}
