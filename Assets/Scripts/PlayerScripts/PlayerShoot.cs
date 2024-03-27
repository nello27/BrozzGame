using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    public GameObject superSaiyan;
    public GameObject fireBullet;
    public bool activeBullet;


    private GameObject currentAura;

    void Start()
    {
        activeBullet = false;    
    }
    // Update is called once per frame
    void Update()
    {
        ShootBullet();
        if (activeBullet)
        {
            if (currentAura == null)
            {
                // No hay una instancia de superSaiyan activa, así que instanciamos una nueva
                currentAura = Instantiate(superSaiyan,transform.position + new Vector3(0f, 0.5f, 0f), Quaternion.identity);
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

    void ShootBullet() {

        if (Input.GetKeyDown(KeyCode.J) && activeBullet) {
            

            GameObject bullet = Instantiate(fireBullet, transform.position, Quaternion.identity);
            
            bullet.GetComponent<FireBullet>().Speed *= transform.localScale.x;
        
        }
    
    }
}
