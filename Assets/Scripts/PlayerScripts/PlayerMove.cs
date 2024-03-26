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

    // Posición de verificación para comprobar si el jugador está en el suelo
    public Transform groundCheckPosition;

    // Capa del suelo
    public LayerMask groundLayer;

    // Booleanos para controlar si el jugador está en el suelo y si ha saltado
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
        // Este método se llama al inicio del juego
        // Aquí puedes realizar inicializaciones adicionales si es necesario
        //print("Called 2st");
    }

    // Update se llama una vez por fotograma
    void Update()
    {
        // Método para comprobar si el jugador está en el suelo
        CheckIfGrounded();
        // Método para permitir que el jugador salte
        PlayerJump();
    }

    // FixedUpdate se llama en intervalos regulares
    void FixedUpdate()
    {
        // Método para mover al jugador
        PlayerWalk();
    }

    // Método para mover al jugador
    void PlayerWalk()
    {
        // Obtener la entrada del eje horizontal (derecha o izquierda)
        float h = Input.GetAxisRaw("Horizontal");

        // Mover al jugador en la dirección correspondiente
        if (h > 0)
        {
            myBody.velocity = new Vector2(speed, myBody.velocity.y);
            // Cambiar la orientación del jugador hacia la derecha
            ChangeDirection(1);
        }
        else if (h < 0)
        {
            myBody.velocity = new Vector2(-speed, myBody.velocity.y);
            // Cambiar la orientación del jugador hacia la izquierda
            ChangeDirection(-1);
        }
        else
        {
            // Si no hay entrada, el jugador se detiene
            myBody.velocity = new Vector2(0f, myBody.velocity.y);
        }

        // Establecer la animación de velocidad en función de la velocidad del jugador
        anim.SetInteger("Speed", Mathf.Abs((int)myBody.velocity.x));
    }

    // Método para cambiar la orientación del jugador
    void ChangeDirection(int direction)
    {
        Vector3 tempScale = transform.localScale;
        tempScale.x = direction;
        transform.localScale = tempScale;
    }

    // Método para comprobar si el jugador está en el suelo
    void CheckIfGrounded()
    {
        // Realizar un raycast hacia abajo desde la posición de verificación de suelo
        isGrounded = Physics2D.Raycast(groundCheckPosition.position, Vector2.down, 0.1f, groundLayer);

        // Si el jugador está en el suelo y ha saltado, actualizar la animación
        if (isGrounded && jumped)
        {
            jumped = false;
            anim.SetBool("Jump", false);
        }
    }

    // Método para permitir que el jugador salte
    void PlayerJump()
    {
        // Si el jugador está en el suelo y presiona la tecla de espacio
        if (isGrounded && Input.GetKey(KeyCode.Space))
        {
            jumped = true;
            // Aplicar una velocidad hacia arriba para simular el salto
            myBody.velocity = new Vector2(myBody.velocity.x, jumpPower);
            // Actualizar la animación de salto
            anim.SetBool("Jump", true);
        }
    }
}
