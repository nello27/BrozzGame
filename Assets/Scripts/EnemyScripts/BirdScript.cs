using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdScript : MonoBehaviour
{
    // Referencias a componentes
    private Rigidbody2D myBody;
    private Animator anim;

    // Direcci�n de movimiento del p�jaro
    private Vector3 moveDirection = Vector3.zero;

    // Posiciones de origen y movimiento del p�jaro
    public Vector3 originPosition;
    public Vector3 movePosition;

    // Prefabricado del huevo del p�jaro
    public GameObject birdEgg;

    // Capa del jugador
    public LayerMask playerLayer;

    // Booleano para controlar si el p�jaro ha atacado
    public bool attaked;

    // Velocidad del p�jaro
    private float velocity = 2.5f;

    // Booleano para controlar si el p�jaro puede moverse
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

        // Permitir que el p�jaro se mueva
        canMove = true;
    }

    // Update se llama una vez por fotograma
    void Update()
    {
        // Llamar a los m�todos para mover el p�jaro y dejar caer el huevo
        MoveTheBird();
        DropTheEgg();
    }

    // M�todo para mover el p�jaro
    void MoveTheBird()
    {
        if (canMove)
        {
            // Mover el objeto en la direcci�n especificada
            transform.Translate(moveDirection * velocity * Time.smoothDeltaTime);

            // Comprobar si el objeto ha alcanzado su posici�n de origen o su posici�n de movimiento
            if (transform.position.x >= originPosition.x)
            {
                // Cambiar la direcci�n para que el objeto se mueva en la direcci�n opuesta
                moveDirection = Vector3.left;
                // Cambiar la orientaci�n del sprite para que mire en la direcci�n opuesta
                ChangeDirection(0.5f);
            }
            else if (transform.position.x <= movePosition.x)
            {
                // Cambiar la direcci�n para que el objeto se mueva en la direcci�n opuesta
                moveDirection = Vector3.right;
                // Cambiar la orientaci�n del sprite para que mire en la direcci�n opuesta
                ChangeDirection(-0.5f);
            }
        }
    }

    // M�todo para cambiar la orientaci�n del sprite del p�jaro
    void ChangeDirection(float direction)
    {
        Vector3 tempScale = transform.localScale;
        tempScale.x = direction;
        transform.localScale = tempScale;
    }

    // M�todo para dejar caer el huevo del p�jaro
    void DropTheEgg()
    {
        // Si el p�jaro no ha atacado
        if (!attaked)
        {
            // Si el p�jaro detecta al jugador debajo
            if (Physics2D.Raycast(transform.position, Vector2.down, Mathf.Infinity, playerLayer))
            {
                // Instanciar el huevo debajo del p�jaro
                Instantiate(birdEgg, new Vector3(transform.position.x, transform.position.y - 1f, transform.position.z), Quaternion.identity);
                // Marcar que el p�jaro ha atacado
                attaked = true;
                // Reproducir la animaci�n de vuelo del p�jaro
                anim.Play("BirdFly");
                StartCoroutine(BirdStone());
            }
        }
        
    }

    // Coroutine para manejar la muerte del p�jaro
    IEnumerator BirdDead()
    {
        // Esperar un tiempo antes de desactivar el p�jaro
        yield return new WaitForSeconds(3f);
        gameObject.SetActive(false);
    }

    // Coroutine para Retorno caida piedra
    IEnumerator BirdStone()
    {
        // Esperar un tiempo antes de desactivar el p�jaro
        yield return new WaitForSeconds(2f);
        anim.Play("BirdStone");
        attaked = false;
    }

    // M�todo que se llama cuando el p�jaro colisiona con otro objeto
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Si el p�jaro es golpeado por una bala
        if (collision.tag == MyTags.BULLET_TAG)
        {
            // Reproducir la animaci�n de muerte del p�jaro
            anim.Play("BirdDead");
            // Activar el trigger del collider del p�jaro
            GetComponent<BoxCollider2D>().isTrigger = true;
            // Cambiar el tipo de cuerpo del Rigidbody2D para que el p�jaro caiga
            myBody.bodyType = RigidbodyType2D.Dynamic;
            // El p�jaro ya no puede moverse
            canMove = false;
            // Comenzar el coroutine para manejar la muerte del p�jaro
            StartCoroutine(BirdDead());
        }
    }
}
