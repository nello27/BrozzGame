using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiderScript : MonoBehaviour
{
    // Referencia al componente Animator de la ara�a
    private Animator anim;

    // Referencia al componente Rigidbody2D de la ara�a
    private Rigidbody2D myBody;

    // Direcci�n inicial de movimiento de la ara�a
    private Vector3 moveDirection = Vector3.down;

    // Nombre del coroutine que controla el cambio de direcci�n de la ara�a
    private string coroutine_Name = "ChangeMovement";

    void Awake()
    {
        // Obtener las referencias a los componentes Animator y Rigidbody2D
        anim = GetComponent<Animator>();
        myBody = GetComponent<Rigidbody2D>();
    }

    // M�todo que se llama al iniciar
    void Start()
    {
        // Comenzar el coroutine para cambiar el movimiento de la ara�a
        StartCoroutine(coroutine_Name);
    }

    // M�todo que se llama en cada fotograma
    void Update()
    {
        // M�todo para mover la ara�a
        MoveSpider();
    }

    // M�todo para mover la ara�a
    void MoveSpider()
    {
        // Mover la ara�a en la direcci�n establecida
        transform.Translate(moveDirection * Time.smoothDeltaTime);
    }

    // Coroutine para cambiar la direcci�n de movimiento de la ara�a
    IEnumerator ChangeMovement()
    {
        // Esperar un tiempo aleatorio antes de cambiar la direcci�n
        yield return new WaitForSeconds(Random.Range(2f, 5f));

        // Cambiar la direcci�n de la ara�a
        if (moveDirection == Vector3.down)
        {
            moveDirection = Vector3.up;
        }
        else
        {
            moveDirection = Vector3.down;
        }

        // Reiniciar el coroutine para cambiar la direcci�n
        StartCoroutine(coroutine_Name);
    }

    // Coroutine para manejar la muerte de la ara�a
    IEnumerator SpiderDead()
    {
        // Esperar un tiempo antes de desactivar la ara�a
        yield return new WaitForSeconds(3f);
        gameObject.SetActive(false);
    }

    // M�todo que se llama cuando la ara�a colisiona con otro objeto
     void OnTriggerEnter2D(Collider2D collision)
    {
        // Si la ara�a colisiona con una bala
        if (collision.tag == MyTags.BULLET_TAG)
        {
            // Reproducir la animaci�n de muerte de la ara�a
            anim.Play("SpiderDead");

            // Cambiar el tipo de cuerpo del Rigidbody2D para que la ara�a caiga
            myBody.bodyType = RigidbodyType2D.Dynamic;

            // Comenzar el coroutine para manejar la muerte de la ara�a
            StartCoroutine(SpiderDead());

            // Detener el coroutine para cambiar el movimiento de la ara�a
            StopCoroutine(coroutine_Name);
        }

        if (collision.tag == MyTags.PLAYER_TAG) {

            collision.GetComponent<PlayerDamage>().DealDamage();

        }
    }
}
