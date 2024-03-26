using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrogScript : MonoBehaviour
{

    private Animator anim;

    private bool animation_Started;
    private bool animation_Finished;

    private int jumpedTimes;
    private bool jumpLeft = true;

    private string coroutine_Name = "FrogJump";

    public LayerMask playerLayer;
    public GameObject player;

    void Awake () { 
        
        anim = GetComponent<Animator>(); 
    }

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag (MyTags.PLAYER_TAG);
        StartCoroutine(coroutine_Name);
    }

    private void Update()
    {
        if (Physics2D.OverlapCircle(transform.position ,0.5f, playerLayer)) {
            
            player.GetComponent<PlayerDamage>().DealDamage();

        }
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (animation_Finished && animation_Started)
        {
            transform.parent.position = transform.position;
            transform.localPosition = Vector3.zero;
        }
    }

    IEnumerator FrogJump() {

        yield return new WaitForSeconds(Random.Range(1f, 4f));

        animation_Started = true;
        animation_Finished = false;

        jumpedTimes++;

        if (jumpLeft)
        {

            anim.Play("FrogJumpLeft");
        }
        else {
            anim.Play("FrogJumpRight");
        }


        StartCoroutine(coroutine_Name);

    }

    void AnimationFinished()
    {
        animation_Finished = true;

        if (jumpLeft)
        {

            anim.Play("FrogIdleLeft");

        }
        else {
            anim.Play("FrogIdleRight");
        }

        if (jumpedTimes == 3)
        {
            jumpedTimes = 0;

            Vector3 tempScale = transform.localScale;
            tempScale.x *= -1;
            transform.localScale = tempScale;

            jumpLeft = !jumpLeft;


        }
        

    }


}
