using System.Collections;
using System.Collections.Generic;
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

    // Start is called before the first frame update
    void Start()
    {
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

        if (!enableAnimation)
        {
            // Movimiento hacia el objetivo
            transform.position = Vector3.MoveTowards(transform.position, target.position, speed * Time.deltaTime);


            // Si alcanzamos el punto, cambiamos de objetivo
            if (Vector3.Distance(transform.position, target.position) < 0.1f)
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
}
