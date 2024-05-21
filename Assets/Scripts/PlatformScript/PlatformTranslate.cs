using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEditor.FilePathAttribute;
using static UnityEngine.GraphicsBuffer;

public class PlatformTranslate : MonoBehaviour
{

    private Transform target;
    public Transform pointA;
    public Transform pointB;
    public float speed = 2f;

    // Start is called before the first frame update
    void Start()
    {
        target = pointA; // Comenzamos moviéndonos hacia pointA
    }

    // Update is called once per frame
    void Update()
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

                }
                else
                {
                    target = pointA;


                }
            }
        

    }
}
