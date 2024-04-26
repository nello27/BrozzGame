using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class PlayerShoot : MonoBehaviour
{
    public GameObject superSaiyan;
    public GameObject fireBullet;
    public bool activeBullet;

    private GameObject currentAura;
    private GameObject supersaiyanObject;

    public AudioSource audioSource; // Asegúrate de asignar esto desde el editor de Unity
    public AudioClip shootSound; // Asegúrate de asignar esto desde el editor de Unity
    public int seconds = 5;
    public Text textSuperSaiyan;

    void Start()
    {
        activeBullet = false;
    }

    // Update is called once per frame
    void Update()
    {
        // Buscar un objeto con el tag "supersaiyan"
        supersaiyanObject = GameObject.FindWithTag("supersaiyan");

        ShootBullet();

        if (activeBullet)
        {
            if (currentAura == null)
            {
                // No hay una instancia de superSaiyan activa, así que instanciamos una nueva
                currentAura = Instantiate(superSaiyan, transform.position + new Vector3(0f, 0.5f, 0f), Quaternion.identity);
                superSaiyan.SetActive(true);
                // Llamar a DisableSupersaiyan después de 3 segundos
                StartCoroutine(DisableSupersaiyan());
            }
            else
            {
                // Mover la instancia actual de superSaiyan a la posición del jugador
                currentAura.transform.position = transform.position + new Vector3(0f, 0.5f, 0f);
            }
        }
        else
        {
            // Desactivar la instancia actual de superSaiyan si no está activo
            if (currentAura != null)
            {
                Destroy(currentAura);
                currentAura = null;
            }
        }
    }

    void ShootBullet()
    {
        if (Input.GetKeyDown(KeyCode.J) && activeBullet)
        {
            GameObject bullet = Instantiate(fireBullet, transform.position, Quaternion.identity);
            bullet.GetComponent<FireBullet>().Speed *= transform.localScale.x;

            // Reproducir el sonido
            if (audioSource != null && shootSound != null)
            {
                audioSource.clip = shootSound;
                audioSource.Play();
            }
        }
    }

    IEnumerator DisableSupersaiyan()
    {
        float tiempoTranscurrido = 5.9f;

        while (tiempoTranscurrido > 0)
        {
            yield return null; // Esperar al siguiente frame
            tiempoTranscurrido -= Time.deltaTime;

            // Convertir el tiempo transcurrido a entero y asignarlo al componente de texto
            textSuperSaiyan.text = Mathf.RoundToInt(tiempoTranscurrido).ToString();
        }

        if (supersaiyanObject != null)
        {
            supersaiyanObject.SetActive(false);
            activeBullet = false;
            Debug.Log("Objeto con el tag 'supersaiyan' desactivado.");
        }
        else
        {
            Debug.Log("No se puede desactivar el objeto con el tag 'supersaiyan' porque no se encontró.");
        }
    }
}
