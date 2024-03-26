using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    // Velocidad de movimiento del jugador
    public float speed = 5f;

    // Referencias a componentes
    private Rigidbody2D myBody;
    private Animator anim;

    // Posici�n de verificaci�n para comprobar si el jugador est� en el suelo
    public Transform groundCheckPosition;

    // Capa del suelo
    public LayerMask groundLayer;

    // Booleanos para controlar si el jugador est� en el suelo y si ha saltado
    private bool isGrounded;
    private bool jumped;

    // Potencia de salto del jugador
    private float jumpPower = 10f;

    private void Awake()
    {
        // Obtener las referencias a los componentes al inicio
        myBody = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    void Start()
    {
        // Este m�todo se llama al inicio del juego
        // Aqu� puedes realizar inicializaciones adicionales si es necesario
        //print("Called 2st");
    }

    // Update se llama una vez por fotograma
    void Update()
    {
        // M�todo para comprobar si el jugador est� en el suelo
        CheckIfGrounded();
        // M�todo para permitir que el jugador salte
        PlayerJump();
    }

    // FixedUpdate se llama en intervalos regulares
    void FixedUpdate()
    {
        // M�todo para mover al jugador
        PlayerWalk();
    }

    // M�todo para mover al jugador
    void PlayerWalk()
    {
        // Obtener la entrada del eje horizontal (derecha o izquierda)
        float h = Input.GetAxisRaw("Horizontal");

        // Mover al jugador en la direcci�n correspondiente
        if (h > 0)
        {
            myBody.velocity = new Vector2(speed, myBody.velocity.y);
            // Cambiar la orientaci�n del jugador hacia la derecha
            ChangeDirection(1);
        }
        else if (h < 0)
        {
            myBody.velocity = new Vector2(-speed, myBody.velocity.y);
            // Cambiar la orientaci�n del jugador hacia la izquierda
            ChangeDirection(-1);
        }
        else
        {
            // Si no hay entrada, el jugador se detiene
            myBody.velocity = new Vector2(0f, myBody.velocity.y);
        }

        // Establecer la animaci�n de velocidad en funci�n de la velocidad del jugador
        anim.SetInteger("Speed", Mathf.Abs((int)myBody.velocity.x));
    }

    // M�todo para cambiar la orientaci�n del jugador
    void ChangeDirection(int direction)
    {
        Vector3 tempScale = transform.localScale;
        tempScale.x = direction;
        transform.localScale = tempScale;
    }

    // M�todo para comprobar si el jugador est� en el suelo
    void CheckIfGrounded()
    {
        // Realizar un raycast hacia abajo desde la posici�n de verificaci�n de suelo
        isGrounded = Physics2D.Raycast(groundCheckPosition.position, Vector2.down, 0.1f, groundLayer);

        // Si el jugador est� en el suelo y ha saltado, actualizar la animaci�n
        if (isGrounded && jumped)
        {
            jumped = false;
            anim.SetBool("Jump", false);
        }
    }

    // M�todo para permitir que el jugador salte
    void PlayerJump()
    {
        // Si el jugador est� en el suelo y presiona la tecla de espacio
        if (isGrounded && Input.GetKey(KeyCode.Space))
        {
            jumped = true;
            // Aplicar una velocidad hacia arriba para simular el salto
            myBody.velocity = new Vector2(myBody.velocity.x, jumpPower);
            // Actualizar la animaci�n de salto
            anim.SetBool("Jump", true);
        }
    }
}
