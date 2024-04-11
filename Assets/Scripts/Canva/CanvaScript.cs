using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvaScript : MonoBehaviour
{
    public static CanvaScript instance { get; private set;}
    void Awake()
    {
        // Singleton pattern to ensure only one instance of AudioManager exists
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
