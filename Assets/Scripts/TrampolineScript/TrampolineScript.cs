using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrampolineScript : MonoBehaviour
{

    public float bounceForce = 20f; // La fuerza de rebote del trampolín

    void OnCollisionEnter2D(Collision2D collision)
    {
        Rigidbody2D rb = collision.collider.GetComponent<Rigidbody2D>();

        if (rb != null)
        {
            // Aplicar una fuerza hacia arriba
            Vector2 force = new Vector2(0, bounceForce);
            rb.AddForce(force, ForceMode2D.Impulse);
        }
    }
}
