using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    // Velocidad de reseteo de la cámara cuando está fuera de los límites
    public float resetSpeed = 0.5f;
    // Velocidad de movimiento de la cámara
    public float cameraSpeed = 0.3f;

    // Límites de la cámara
    public Bounds cameraBounds;

    // Objeto a seguir
    public Transform target;
    // Distancia entre la cámara y el objetivo en el eje Z
    private float offsetZ;
    // Última posición conocida del objetivo
    private Vector3 lastTargetPosition;
    // Velocidad actual de movimiento de la cámara
    private Vector3 currentVelocity;

    // Variable para habilitar/deshabilitar el seguimiento del jugador
    private bool followPlayer;

    void Awake()
    {
        // Establecer el tamaño del colisionador de la cámara según la resolución
        BoxCollider2D myCol = GetComponent<BoxCollider2D>();
        myCol.size = new Vector2(Camera.main.aspect * 2f * Camera.main.orthographicSize, 15f);
        // Actualizar los límites de la cámara
        cameraBounds = myCol.bounds;
    }

    // Start is called before the first frame update
    void Start()
    {
        // Encontrar y establecer el objeto jugador como objetivo de seguimiento
        target = GameObject.FindGameObjectWithTag(MyTags.PLAYER_TAG).transform;
        // Almacenar la posición inicial del jugador
        lastTargetPosition = target.position;
        // Calcular la distancia inicial en el eje Z entre la cámara y el objetivo
        offsetZ = (transform.position - target.position).z;
        // Activar el seguimiento del jugador
        followPlayer = true;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // Si se está siguiendo al jugador
        if (followPlayer)
        {
            // Calcular la posición futura del objetivo
            Vector3 aheadTargetPos = target.position + Vector3.forward * offsetZ;
            Vector3 LimitLeft = new Vector3(0,0, transform.position.z);

            // Si el objetivo se mueve hacia la derecha
            if (aheadTargetPos.x >= LimitLeft.x)
            {
                // Calcular la nueva posición de la cámara de forma suave
                Vector3 newCameraPosition = Vector3.SmoothDamp(transform.position, aheadTargetPos, ref currentVelocity, cameraSpeed);
                // Actualizar la posición de la cámara solo en el eje X
                transform.position = new Vector3(newCameraPosition.x, transform.position.y, newCameraPosition.z);
                // Actualizar la última posición conocida del objetivo
                lastTargetPosition = target.position;
            }

        }
    }
}
