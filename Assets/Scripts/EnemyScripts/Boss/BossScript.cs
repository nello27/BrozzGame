using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class BossScript : MonoBehaviour
{
    public GameObject stone;
    public Transform attackInstantiate;

    private Animator anim;

    private string coroutine_Name = "StartAttack";


    void Awake()
    {
        anim = GetComponent<Animator>();   
    }

    // Start is called before the first frame update
    void Start()
    {

        StartCoroutine(coroutine_Name);
    }

    void Attack() {

        GameObject obj = Instantiate(stone,attackInstantiate.position, Quaternion.identity);
        obj.GetComponent<Rigidbody2D>().AddForce(new Vector2(Random.Range(-300,-700),0f));

    }


    void BackToIdle() {

        anim.Play("BossIdle");
    
    }

    public void DesactiveBossScript()
    {

        StopAllCoroutines();
        enabled = false;

    }

    IEnumerator StartAttack() { 
        
        yield return new WaitForSeconds(Random.Range(2f,5f));

        anim.Play("BossAttack");
        StartCoroutine(coroutine_Name);
    
    }


}
