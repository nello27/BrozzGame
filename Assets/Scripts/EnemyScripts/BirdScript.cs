using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdScript : MonoBehaviour
{
    // Referencias a componentes
    private Rigidbody2D myBody;
    private Animator anim;

    // Dirección de movimiento del pájaro
    private Vector3 moveDirection = Vector3.zero;

    // Posiciones de origen y movimiento del pájaro
    public Vector3 originPosition;
    public Vector3 movePosition;

    // Prefabricado del huevo del pájaro
    public GameObject birdEgg;

    // Capa del jugador
    public LayerMask playerLayer;

    // Booleano para controlar si el pájaro ha atacado
    public bool attaked;

    // Velocidad del pájaro
    private float velocity = 2.5f;

    // Booleano para controlar si el pájaro puede moverse
    private bool canMove;

    void Awake()
    {
        // Obtener componentes al inicio
        myBody = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    // Start se llama antes del primer fotograma
    void Start()
    {
        // Establecer posiciones de origen y movimiento
        // originPosition = new Vector3(0, 2.9f, 0);
        //originPosition.x += 6f;

        originPosition.x = transform.position.x;

        // movePosition = new Vector3(0, 2.9f, 0);
        // movePosition.x -= 6f;

        movePosition.x = transform.position.x - 10f;

        // Permitir que el pájaro se mueva
        canMove = true;
    }

    // Update se llama una vez por fotograma
    void Update()
    {
        // Llamar a los métodos para mover el pájaro y dejar caer el huevo
        MoveTheBird();
        DropTheEgg();
    }

    // Método para mover el pájaro
    void MoveTheBird()
    {
        if (canMove)
        {
            // Mover el objeto en la dirección especificada
            transform.Translate(moveDirection * velocity * Time.smoothDeltaTime);

            // Comprobar si el objeto ha alcanzado su posición de origen o su posición de movimiento
            if (transform.position.x >= originPosition.x)
            {
                // Cambiar la dirección para que el objeto se mueva en la dirección opuesta
                moveDirection = Vector3.left;
                // Cambiar la orientación del sprite para que mire en la dirección opuesta
                ChangeDirection(0.5f);
            }
            else if (transform.position.x <= movePosition.x)
            {
                // Cambiar la dirección para que el objeto se mueva en la dirección opuesta
                moveDirection = Vector3.right;
                // Cambiar la orientación del sprite para que mire en la dirección opuesta
                ChangeDirection(-0.5f);
            }
        }
    }

    // Método para cambiar la orientación del sprite del pájaro
    void ChangeDirection(float direction)
    {
        Vector3 tempScale = transform.localScale;
        tempScale.x = direction;
        transform.localScale = tempScale;
    }

    // Método para dejar caer el huevo del pájaro
    void DropTheEgg()
    {
        // Si el pájaro no ha atacado
        if (!attaked)
        {
            // Si el pájaro detecta al jugador debajo
            if (Physics2D.Raycast(transform.position, Vector2.down, Mathf.Infinity, playerLayer))
            {
                // Instanciar el huevo debajo del pájaro
                Instantiate(birdEgg, new Vector3(transform.position.x, transform.position.y - 1f, transform.position.z), Quaternion.identity);
                // Marcar que el pájaro ha atacado
                attaked = true;
                // Reproducir la animación de vuelo del pájaro
                anim.Play("BirdFly");
                StartCoroutine(BirdStone());
            }
        }
        
    }

    // Coroutine para manejar la muerte del pájaro
    IEnumerator BirdDead()
    {
        // Esperar un tiempo antes de desactivar el pájaro
        yield return new WaitForSeconds(3f);
        gameObject.SetActive(false);
    }

    // Coroutine para Retorno caida piedra
    IEnumerator BirdStone()
    {
        // Esperar un tiempo antes de desactivar el pájaro
        yield return new WaitForSeconds(2f);
        anim.Play("BirdStone");
        attaked = false;
    }

    // Método que se llama cuando el pájaro colisiona con otro objeto
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Si el pájaro es golpeado por una bala
        if (collision.tag == MyTags.BULLET_TAG)
        {
            // Reproducir la animación de muerte del pájaro
            anim.Play("BirdDead");
            // Activar el trigger del collider del pájaro
            GetComponent<BoxCollider2D>().isTrigger = true;
            // Cambiar el tipo de cuerpo del Rigidbody2D para que el pájaro caiga
            myBody.bodyType = RigidbodyType2D.Dynamic;
            // El pájaro ya no puede moverse
            canMove = false;
            // Comenzar el coroutine para manejar la muerte del pájaro
            StartCoroutine(BirdDead());
        }
    }
}
