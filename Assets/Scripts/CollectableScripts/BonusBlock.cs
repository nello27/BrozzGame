using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BonusBlock : MonoBehaviour
{

    public Transform botton_Collision;

    private Animator anim;

    public GameObject start;
    public GameObject coin;

    private Animator animator;

    public bool OnStar;
    public bool Oncoin;

    public LayerMask playerLayer;

    private Vector3 moveDirection = Vector3.up;
    private Vector3 originPosition;
    private Vector3 animPosition;
    private bool startAnim;
    private bool canAnimate = true;
    private Vector3 posicionOriginal;


    void Awake() { 
        
        anim = GetComponent<Animator>();
        if (OnStar)
        {
            animator = start.GetComponent<Animator>();
        }
        if (Oncoin)
        {
            animator = coin.GetComponent<Animator>();
        }



    }

    // Start is called before the first frame update
    void Start()
    {
        originPosition = transform.position;
        animPosition = transform.position;
        animPosition.y += 0.15f;
        // Almacena la posición original, ignorando el eje Y
        posicionOriginal = transform.position;
       

    }

    // Update is called once per frame
    void Update()
    {
        CheckForCollision();
        AnimateUpDown();

        
        // Incrementar la posición Y del objeto utilizando la velocidad de ascenso y el tiempo transcurrido desde el último frame



    }

    void CheckForCollision() {

        if (canAnimate) { 
        RaycastHit2D hit = Physics2D.Raycast(botton_Collision.position, Vector2.down, 0.1f , playerLayer);

        if (hit) {
            if (hit.collider.gameObject.tag == MyTags.PLAYER_TAG) {
                //  increase score
                anim.Play("BlockIdle");
                
                    if (OnStar)
                    {
                        animator.Play("GoStartAnimation");
                    }
                    
                    startAnim = true;

                    
                }

        }
        }
    }

    void AnimateUpDown() {

        if (startAnim) {
            transform.Translate(moveDirection * Time.smoothDeltaTime);
            if (transform.position.y >= animPosition.y) {
                moveDirection = Vector3.down;
                canAnimate = false;
            } else if (transform.position.y <= originPosition.y) {
                if (Oncoin)
                {
                    Instantiate(coin, posicionOriginal, Quaternion.identity);
                    
                }
                
                 
                //animator.Play("OnCoin");
                startAnim = false;
            }
        }

    }

}
