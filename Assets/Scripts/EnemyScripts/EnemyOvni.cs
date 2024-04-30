using UnityEngine;

public class EnemyOvni : MonoBehaviour
{
    public Transform pointA;
    public Transform pointB;
    public float speed = 2f;
    public float rotationSpeed = 1f;

    private Transform target;
    private Vector3 originPosition;

    void Start()
    {
        originPosition = transform.position;
        target = pointA; // Comenzamos moviéndonos hacia pointA
    }

    void Update()
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
}
