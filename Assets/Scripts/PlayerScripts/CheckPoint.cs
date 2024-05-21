using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    PlayerDamage dead;
    CameraFollow camerapositionY;
    public GameObject Player;
    public GameObject Camera;
    public GameObject checkPoint;
    public bool inY;

    void Start()
    {
        
        Player = GameObject.FindWithTag(MyTags.PLAYER_TAG);
        Camera = GameObject.FindWithTag("MainCamera");
        dead = Player.GetComponent<PlayerDamage>();
        camerapositionY = Camera.GetComponent<CameraFollow>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == MyTags.PLAYER_TAG)
        {
            dead.respawnPosition = checkPoint.transform.position;

            if (inY) {

                camerapositionY.followPlayerinY = true;
            }
        }
    }

}
