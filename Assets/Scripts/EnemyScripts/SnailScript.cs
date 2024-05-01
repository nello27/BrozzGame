using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;

public class SnailScript : MonoBehaviour
{
    // Velocidad de movimiento del caracol
    public float moveSpeed = 1f;

    // Referencias al Rigidbody2D y al Animator del caracol
    private Rigidbody2D mybody;
    private Animator anim;

    // Máscara de capa para detectar al jugador
    public LayerMask playerLayer;

    // Booleano para determinar la dirección de movimiento del caracol
    private bool moveLeft;

    // Transform de los puntos de colisión del caracol
    public Transform down_collision, left_Collision, right_Collision, top_Collision;

    // Posiciones iniciales de los puntos de colisión del caracol
    public Vector3 left_Collision_Pos, rigth_Collision_Pos;

    // Booleanos para controlar si el caracol puede moverse y si está aturdido
    private bool canMove;
    private bool stunned;


    void Awake() 
    {
        // Obtener referencias a Rigidbody2D y Animator
        mybody = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();

        // Guardar las posiciones iniciales de los puntos de colisión
        rigth_Collision_Pos = right_Collision.position;
        left_Collision_Pos = left_Collision.position;
    }

    // Método Start es llamado una vez al inicio
    void Start()
    {
        // Al inicio del juego, permitir que el caracol se mueva
        canMove = true;

    }

    // Update es llamado una vez por frame
    void Update()
    {


        // Si el caracol puede moverse
        if (canMove)
        {
            // Movimiento del caracol dependiendo de la dirección
            if (moveLeft)
            {
                mybody.velocity = new Vector2(-moveSpeed, mybody.velocity.y); // Cambiado a negativo para mover hacia la izquierda
            }
            else
            {
                mybody.velocity = new Vector2(moveSpeed, mybody.velocity.y); // Movimiento hacia la derecha
            }
        }

        // Verificar colisiones del caracol
        CheckCollision();
    }

    // Método para verificar colisiones del caracol
    void CheckCollision()
    {
        // Lanzar un Raycast hacia abajo desde la posición del objeto down_collision
        RaycastHit2D hit = Physics2D.Raycast(down_collision.position, Vector2.down, 0.1f);

        // Rayo para detectar colisión a la izquierda del caracol
        RaycastHit2D leftHit = Physics2D.Raycast(left_Collision.position, Vector2.left, 0.1f, playerLayer);

        // Rayo para detectar colisión a la derecha del caracol
        RaycastHit2D RightHit = Physics2D.Raycast(right_Collision.position, Vector2.right, 0.1f, playerLayer);

        // Colisión superior del caracol
        Collider2D topHit = Physics2D.OverlapCircle(top_Collision.position, 0.2f, playerLayer);

        if (topHit != null)
        {
            if (topHit.gameObject.tag == MyTags.PLAYER_TAG) {

                if (!stunned) { 
                    
                    topHit.gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(topHit.gameObject.GetComponent<Rigidbody2D>().velocity.x, 7f);

                    canMove = false;
                    mybody.velocity = new Vector2(0,0);

                    anim.Play("Stunned");
                    stunned = true;

                    //BEETLE CODE HERE 
                    if (tag == MyTags.BEETLE_TAG)
                    {
                        anim.Play("Stunned");
                        StartCoroutine(Dead(0.5f));
                    }

                
                }
            
            }

        }

        if (leftHit) {

            if (leftHit.collider.gameObject.tag == MyTags.PLAYER_TAG) {

                if (!stunned)
                {
                    //APPLY DAMAGE TO PLAYER
                    leftHit.collider.gameObject.GetComponent<PlayerDamage>().DealDamage();
                }
                else {

                    if (tag != MyTags.BEETLE_TAG) {

                        mybody.velocity = new Vector2(15f, mybody.velocity.y);
                    }
                                     

                }
            
            }

        }

        if (RightHit)
        {

            if (RightHit.collider.gameObject.tag == MyTags.PLAYER_TAG)
            {

                if (!stunned)
                {
                    //APPLY DAMAGE TO PLAYER
                    RightHit.collider.gameObject.GetComponent<PlayerDamage>().DealDamage();
                }
                else
                {
                    if (tag != MyTags.BEETLE_TAG)
                    {
                        mybody.velocity = new Vector2(-15f, mybody.velocity.y);
                    }
                }

            }
        }

        Debug.DrawRay(down_collision.position, Vector2.down * 0.1f, Color.red);

        // Verificar si el Raycast golpea algo (es decir, si hay suelo)
        if (hit.collider == null)
        {
            // Si hay suelo, realiza las acciones correspondientes
            Debug.Log("Hay suelo");
            ChangeDirection();
      
        }

    }

    // Método para cambiar la dirección del caracol
    void ChangeDirection()
    {
        // Cambiar la dirección de movimiento
        moveLeft = !moveLeft;

        // Cambiar la escala del caracol para reflejar la dirección
        Vector3 tempScale = transform.localScale;

        if (moveLeft)
        {
            tempScale.x = Mathf.Abs(tempScale.x);
            right_Collision.position = rigth_Collision_Pos;
            left_Collision.position = left_Collision_Pos;
        }
        else
        {
            tempScale.x = -Mathf.Abs(tempScale.x);

            right_Collision.position = left_Collision_Pos;
            left_Collision.position = rigth_Collision_Pos;
        }

        transform.localScale = tempScale;
    }


    IEnumerator Dead(float timer) { 
    
        yield return new WaitForSeconds(timer);
        gameObject.SetActive(false);
    
    }



    // Método llamado cuando el caracol colisiona con otro objeto
    private void OnTriggerEnter2D(Collider2D collision)
    {
       
        // Si la colisión es con el jugador, reproducir animación de aturdimiento
        if (collision.gameObject.tag == MyTags.BULLET_TAG)
        {
            anim.Play("Stunned");
            canMove = false;
            mybody.velocity = new Vector2(0,0);

            StartCoroutine(Dead(0.4f));
        }

        if (tag == MyTags.SNAIL_TAG) {

            if (!stunned)
            {
                anim.Play("Stunned");
                stunned = true;
                canMove = false;
                mybody.velocity = new Vector2(0, 0);
            }
            else { 
            
                gameObject.SetActive(true);
            }
        
        }
    }





}
