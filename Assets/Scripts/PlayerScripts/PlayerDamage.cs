using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerDamage : MonoBehaviour
{

    public Text lifeText;
    private int lifeScoreCount;

    private bool canDamage;
    public Vector3 respawnPosition;

    void Awake()
    {
        lifeText = GameObject.Find("LifeText").GetComponent<Text>();
        lifeScoreCount = 3;
        lifeText.text = "x" + lifeScoreCount;
        
        canDamage = true;

    }

    void Start() {

        Time.timeScale = 1f;
        // Guardamos la posición inicial del jugador como posición de respawn
        respawnPosition = transform.position;

    }


    // Método para respawnear en el último checkpoint alcanzado
    public void RespawnAtLastCheckpoint(Vector3 checkpointPosition)
    {
        transform.position = checkpointPosition;
    }


    public void DealDamage() {


        if (canDamage)
        {
            lifeScoreCount--;


            if (lifeScoreCount >= 0)
            {
                lifeText.text = "x" + lifeScoreCount;
                // Respawnear en el último checkpoint alcanzado
                RespawnAtLastCheckpoint(respawnPosition);
            }

            if (lifeScoreCount == 0)
            {

                //RESTART THE GAME
                Time.timeScale = 0f;
                StartCoroutine(RestartGame());

            }

            canDamage = false;

            StartCoroutine(WaitforDamage());
        }

    }

    IEnumerator WaitforDamage()
    {

        yield return new WaitForSeconds(2f);
        canDamage = true;

    }

    IEnumerator RestartGame() {

        yield return new WaitForSecondsRealtime(2f);
        SceneManager.LoadScene("GamePlay");
    }

    // Método para detectar la muerte del jugador (este método debe ser adaptado según el sistema de muerte de tu juego)
    public void Die()
    {
        // Respawnear en el último checkpoint alcanzado
        RespawnAtLastCheckpoint(respawnPosition);
    }
}
