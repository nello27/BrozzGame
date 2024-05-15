using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class HornetScript : MonoBehaviour
{
    public Transform pointA;
    public Transform pointB;
    public float speed = 2f;
    public float rotationSpeed = 1f;
    public LayerMask playerLayer;
    private Animator anim;
    public GameObject burst;
    private AudioSource sound;
    private Animator burstanim;

    private Transform target;
    private Vector3 originPosition;
    // Transform de los puntos de colisión del enemy
    public Transform top_Collision;
    private bool enableAnimation;
    public bool rotation;

    private bool canMove;

    // Referencias a componentes
    private Rigidbody2D myBody;

    public GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag(MyTags.PLAYER_TAG);
        // Obtener componentes al inicio
        myBody = GetComponent<Rigidbody2D>();
        canMove = true;
        enableAnimation = false;
        originPosition = transform.position;
        target = pointA; // Comenzamos moviéndonos hacia pointA
        top_Collision = GetComponent<Transform>(); // Assign the current object's transform
        anim = GetComponent<Animator>();
        burstanim = GetComponent<Animator>();
        sound = GetComponent<AudioSource>();
    }

    void Update()
    {


        if (Physics2D.OverlapCircle(transform.position, 1f, playerLayer))
        {

            player.GetComponent<PlayerDamage>().DealDamage();

        }

        if (!enableAnimation)
        {
            // Movimiento hacia el objetivo
            transform.position = Vector3.MoveTowards(transform.position, target.position, speed * Time.deltaTime);


            // Si alcanzamos el punto, cambiamos de objetivo
            if (Vector3.Distance(transform.position, target.position) < 0.1f && canMove)
            {
                if (target == pointA)
                {
                    target = pointB;
                    // Rotación a 180 grados cuando llegamos al punto B

                    if (rotation)
                    { 
                        transform.rotation = Quaternion.Euler(0f, 180f, 0f); 
                    }
                }
                else
                {
                    target = pointA;

                    if (rotation)
                    {
                        transform.rotation = Quaternion.Euler(0f, 0f, 0f);
                    }
                }
            }
        }



    }


    // Método que se llama cuando el pájaro colisiona con otro objeto
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Si el pájaro es golpeado por una bala
        if (collision.tag == MyTags.BULLET_TAG)
        {
            // Reproducir la animación de muerte del pájaro
            //anim.Play("HornetDead");
            // Activar el trigger del collider del pájaro
            GetComponent<BoxCollider2D>().isTrigger = true;
            // Cambiar el tipo de cuerpo del Rigidbody2D para que el pájaro caiga
            myBody.bodyType = RigidbodyType2D.Dynamic;
            myBody.gravityScale = 1;
            // El pájaro ya no puede moverse
            canMove = false;
            // Comenzar el coroutine para manejar la muerte del pájaro
            StartCoroutine(HornetDead());
        }
    }


    IEnumerator HornetDead() {

        // Esperar un tiempo antes de desactivar el ABEJORRO
        yield return new WaitForSeconds(3f);
        gameObject.SetActive(false);
    }
}
