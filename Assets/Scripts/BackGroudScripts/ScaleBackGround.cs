using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaleBackGround : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        // Obtener el componente SpriteRenderer del objeto actual
        SpriteRenderer sr = GetComponent<SpriteRenderer>();

        // Restablecer la escala del objeto a (1, 1, 1)
        transform.localScale = new Vector3(1, 1, 1);

        // Obtener el ancho y alto del sprite
        float width = sr.sprite.bounds.size.x;
        float height = sr.sprite.bounds.size.y;

        // Calcular la altura del mundo en unidades del juego
        float worldHeight = Camera.main.orthographicSize * 2f;

        // Calcular el ancho del mundo en unidades del juego basado en la relación de aspecto de la pantalla
        float worldWidth = worldHeight / Screen.height * Screen.width;

        // Crear un vector temporal para almacenar la escala
        Vector3 tempScale = transform.localScale;

        // Calcular la nueva escala en el eje X y agregar un pequeño margen (0.1f)
        tempScale.x = worldWidth / width + 0.1f;

        // Calcular la nueva escala en el eje Y y agregar un pequeño margen (0.1f)
        tempScale.y = worldHeight / height + 0.2f;

        // Aplicar la nueva escala al objeto
        transform.localScale = tempScale;
    
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
