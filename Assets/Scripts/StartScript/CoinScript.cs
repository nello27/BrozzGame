using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinScript : MonoBehaviour
{
    private float ascendSpeed = 0.5f; // Velocidad de ascenso
    private Vector3 initialPosition; // Posición inicial del objeto
    private bool isAnimating = true; // Variable para controlar si se está reproduciendo la animación
    private float startTime; // Tiempo inicial de la animación

    void Start()
    {
        // Almacenar la posición inicial del objeto
        initialPosition = transform.position;
        startTime = Time.time;
    }

    void Update()
    {

        // Verificar si se está reproduciendo la animación
        if (isAnimating)
        {
            // Calcular el tiempo transcurrido desde el inicio de la animación
            float elapsedTime = Time.time - startTime;

            // Verificar si ha pasado más de 2 segundos desde el inicio de la animación
            if (elapsedTime >= 2f)
            {
                // Detener la animación estableciendo isAnimating a false
                isAnimating = false;
            }
            else
            {
                // Incrementar la posición Y del objeto utilizando la velocidad de ascenso y el tiempo transcurrido desde el último frame
                transform.position = new Vector3(transform.position.x, initialPosition.y + ascendSpeed * elapsedTime, transform.position.z);
            }
        }

    }
}
