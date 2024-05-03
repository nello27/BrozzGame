using UnityEngine;

public class EnemyOvni : MonoBehaviour
{
    public Transform pointA;
    public Transform pointB;
    public float speed = 2f;
    public float rotationSpeed = 1f;
    public LayerMask playerLayer;
    private Animator anim;
    private GameObject rebound;
    private AudioSource sound;
   

    private Transform target;
    private Vector3 originPosition;
    // Transform de los puntos de colisión del enemy
    public Transform top_Collision;
    private bool enableAnimation;

    void Start()
    {

        enableAnimation = false;
        originPosition = transform.position;
        target = pointA; // Comenzamos moviéndonos hacia pointA
        top_Collision = GetComponent<Transform>(); // Assign the current object's transform
        anim = GetComponent<Animator>();
        sound = GetComponent<AudioSource>();

    }

    void Update()
    {

        if (!enableAnimation)
        {
            // Movimiento hacia el objetivo
            transform.position = Vector3.MoveTowards(transform.position, target.position, speed * Time.deltaTime);

        // Rotación hacia el objetivo
        //Quaternion targetRotation = Quaternion.LookRotation(target.position - transform.position);
        // transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);


            // Si alcanzamos el punto, cambiamos de objetivo
            if (Vector3.Distance(transform.position, target.position) < 0.1f)
            {
                if (target == pointA)
                {
                    target = pointB;
                    // Rotación a 180 grados cuando llegamos al punto B
                    transform.rotation = Quaternion.Euler(0f, 180f, 0f);
                }
                else
                {
                    target = pointA;

                    transform.rotation = Quaternion.Euler(0f, 0f, 0f);
                }
            }
        }

        CheckCollision();
    }

    void CheckCollision()
    {

        // Colisión superior del enemy
        Collider2D topHit = Physics2D.OverlapCircle(top_Collision.position, 0.2f, playerLayer);


        if (topHit != null)
        {
            if (topHit.gameObject.tag == MyTags.PLAYER_TAG)
            {
                //inactivamos el componente collider 2d
                sound.Play();
                gameObject.GetComponent<Collider2D>().enabled = false;

                enableAnimation = true;
                // topHit.gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(topHit.gameObject.GetComponent<Rigidbody2D>().velocity.x, 7f);
                anim.Play("smashEnemy");
                //Destroy(gameObject);

            }

        }

    }

    void DisableOvni()
    {
        
        Destroy(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == MyTags.PLAYER_TAG ) {

            print("Colisiona con el Player");

        }
    }
}
