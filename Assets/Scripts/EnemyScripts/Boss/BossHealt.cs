using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossHealt : MonoBehaviour
{

    private Animator anim;
    public int health = 2;

    private bool canDamage;


    void Awake() { 
        
        anim = GetComponent<Animator>();
        canDamage = true;
    }

    IEnumerator WaitForDamage() {


        yield return new WaitForSeconds(2f);
        canDamage = true;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (canDamage)
        {
            if (collision.tag == MyTags.BULLET_TAG)
            {
                health--;
                canDamage = false;

                if (health == 0)
                {
                    GetComponent<BossScript>().DesactiveBossScript();
                    anim.Play("BossDead");
                }

                StartCoroutine(WaitForDamage());
            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
