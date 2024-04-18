using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Unity.VisualScripting;

public class PlayerDamage : MonoBehaviour
{

    public Text lifeText;
    public int lifeScoreCount;
    public GameObject canva;

    private bool canDamage;
    public Vector3 respawnPosition;

    private Color colorActual;
    private float nuevoAlfa;
    private SpriteRenderer spriteRenderer;
    private float velocidadIntermitencia = 0.1f; // Velocidad de la intermitencia
    private bool aumentando = true; // Indica si el valor alfa est� aumentando o disminuyendo


    void Awake()
    {
        lifeText = GameObject.Find("LifeText").GetComponent<Text>();
        lifeScoreCount = 3;
        lifeText.text = "x" + lifeScoreCount;
        
        canDamage = true;

    }

    void Start() {


        Time.timeScale = 1f;
        // Guardamos la posici�n inicial del jugador como posici�n de respawn
        respawnPosition = transform.position;

        // Obtener referencia al componente SpriteRenderer
        spriteRenderer = GetComponent<SpriteRenderer>();

        // Comenzar la intermitencia
        //InvokeRepeating("CambiarAlfa", 0, velocidadIntermitencia);

    }

    public void Update()
    {
        lifeText.text = "x" + lifeScoreCount; ;
        
    }


    // M�todo para respawnear en el �ltimo checkpoint alcanzado
    public void RespawnAtLastCheckpoint(Vector3 checkpointPosition)
    {
        transform.position = checkpointPosition;
        Invoke("DetenerIntermitencia", 2.0f);
        InvokeRepeating("CambiarAlfa", 0, velocidadIntermitencia);

    }


    public void DealDamage() {


        if (canDamage)
        {
            lifeScoreCount--;


            if (lifeScoreCount >= 0)
            {
                lifeText.text = "x" + lifeScoreCount;
                // Respawnear en el �ltimo checkpoint alcanzado
                RespawnAtLastCheckpoint(respawnPosition);
            }

            if (lifeScoreCount == 0)
            {

                //RESTART THE GAME
                Time.timeScale = 0f;
                StartCoroutine(RestartGame());

            }

            canDamage = false;

            //StartCoroutine(WaitforDamage());
        }
        canDamage = true;

       
    }

    IEnumerator WaitforDamage()
    {

        yield return new WaitForSeconds(2f);
        canDamage = true;

    }

    IEnumerator RestartGame() {


        yield return new WaitForSecondsRealtime(2f);
        SceneManager.LoadScene("GamePlay");
        // Destruir el objeto al que est� adjunto este script
        Destroy(gameObject);
        Destroy(canva);
    }

    // M�todo para detectar la muerte del jugador (este m�todo debe ser adaptado seg�n el sistema de muerte de tu juego)
    public void Die()
    {
        // Respawnear en el �ltimo checkpoint alcanzado
        RespawnAtLastCheckpoint(respawnPosition);
    }

    void DetenerIntermitencia()
    {
        colorActual = spriteRenderer.color;
        nuevoAlfa = 1f;
        // Detener la intermitencia
        // Actualizar el color con el nuevo valor alfa
        spriteRenderer.color = new Color(colorActual.r, colorActual.g, colorActual.b, nuevoAlfa);
        CancelInvoke("CambiarAlfa");

    }
    void CambiarAlfa()
    {
        if (spriteRenderer != null)
        {
            colorActual = spriteRenderer.color;

            // Determinar si el valor alfa est� aumentando o disminuyendo
            if (aumentando)
            {
                nuevoAlfa = 1.0f;
            }
            else
            {
                nuevoAlfa = 0.5f;
            }

            // Actualizar el color con el nuevo valor alfa
            spriteRenderer.color = new Color(colorActual.r, colorActual.g, colorActual.b, nuevoAlfa);

            // Cambiar la direcci�n de la intermitencia
            aumentando = !aumentando;
        }
    }
}
