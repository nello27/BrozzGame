using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinScript : MonoBehaviour
{
    private float ascendSpeed = 0.5f; // Velocidad de ascenso
    private Vector3 initialPosition; // Posici�n inicial del objeto
    private bool isAnimating = true; // Variable para controlar si se est� reproduciendo la animaci�n
    private float startTime; // Tiempo inicial de la animaci�n

    void Start()
    {
        // Almacenar la posici�n inicial del objeto
        initialPosition = transform.position;
        startTime = Time.time;
    }

    void Update()
    {

        // Verificar si se est� reproduciendo la animaci�n
        if (isAnimating)
        {
            // Calcular el tiempo transcurrido desde el inicio de la animaci�n
            float elapsedTime = Time.time - startTime;

            // Verificar si ha pasado m�s de 2 segundos desde el inicio de la animaci�n
            if (elapsedTime >= 2f)
            {
                // Detener la animaci�n estableciendo isAnimating a false
                isAnimating = false;
            }
            else
            {
                // Incrementar la posici�n Y del objeto utilizando la velocidad de ascenso y el tiempo transcurrido desde el �ltimo frame
                transform.position = new Vector3(transform.position.x, initialPosition.y + ascendSpeed * elapsedTime, transform.position.z);
            }
        }

    }
}
