using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ScoreScript : MonoBehaviour
{
    public Text coinTextScore;
    private AudioSource audioManager;
    private int scoreCount;


    private void Awake()
    {
        audioManager = GetComponent<AudioSource>();
    }

    // Start is called before the first frame update
    void Start()
    {
        coinTextScore = GameObject.Find("CoinText").GetComponent<Text>();
       
    }


    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Coin") {
            
            collision.gameObject.SetActive(false);
            scoreCount++;

            coinTextScore.text = "x" + scoreCount;

            audioManager.Play();

        }    
    }


}
