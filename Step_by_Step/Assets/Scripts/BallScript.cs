using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallScript : MonoBehaviour
{
    private Vector3 respawnLocation = new Vector3();

    void Start()
    {
        respawnLocation = transform.position;
    }

    void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "River")
        {
            respawn();
        }
    }

    public void respawn()
    {
        transform.position = respawnLocation;
    }
}
