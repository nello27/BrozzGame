using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    // Velocidad de reseteo de la c�mara cuando est� fuera de los l�mites
    public float resetSpeed = 0.5f;
    // Velocidad de movimiento de la c�mara
    public float cameraSpeed = 0.3f;

    // L�mites de la c�mara
    public Bounds cameraBounds;

    // Objeto a seguir
    public Transform target;
    // Distancia entre la c�mara y el objetivo en el eje Z
    private float offsetZ;
    // �ltima posici�n conocida del objetivo
    private Vector3 lastTargetPosition;
    // Velocidad actual de movimiento de la c�mara
    private Vector3 currentVelocity;

    // Variable para habilitar/deshabilitar el seguimiento del jugador
    private bool followPlayer;

    void Awake()
    {
        // Establecer el tama�o del colisionador de la c�mara seg�n la resoluci�n
        BoxCollider2D myCol = GetComponent<BoxCollider2D>();
        myCol.size = new Vector2(Camera.main.aspect * 2f * Camera.main.orthographicSize, 15f);
        // Actualizar los l�mites de la c�mara
        cameraBounds = myCol.bounds;
    }

    // Start is called before the first frame update
    void Start()
    {
        // Encontrar y establecer el objeto jugador como objetivo de seguimiento
        target = GameObject.FindGameObjectWithTag(MyTags.PLAYER_TAG).transform;
        // Almacenar la posici�n inicial del jugador
        lastTargetPosition = target.position;
        // Calcular la distancia inicial en el eje Z entre la c�mara y el objetivo
        offsetZ = (transform.position - target.position).z;
        // Activar el seguimiento del jugador
        followPlayer = true;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // Si se est� siguiendo al jugador
        if (followPlayer)
        {
            // Calcular la posici�n futura del objetivo
            Vector3 aheadTargetPos = target.position + Vector3.forward * offsetZ;
            Vector3 LimitLeft = new Vector3(0,0, transform.position.z);

            // Si el objetivo se mueve hacia la derecha
            if (aheadTargetPos.x >= LimitLeft.x)
            {
                // Calcular la nueva posici�n de la c�mara de forma suave
                Vector3 newCameraPosition = Vector3.SmoothDamp(transform.position, aheadTargetPos, ref currentVelocity, cameraSpeed);
                // Actualizar la posici�n de la c�mara solo en el eje X
                transform.position = new Vector3(newCameraPosition.x, transform.position.y, newCameraPosition.z);
                // Actualizar la �ltima posici�n conocida del objetivo
                lastTargetPosition = target.position;
            }

        }
    }
}
