using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BodyScript : MonoBehaviour
{
    public GameController gameController;
    public CharacterController characterController;

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            gameController.GameOver();
        }
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Collectible")
        {
            Destroy(col.gameObject);
            characterController.foundCollectible = true;
        }
    }
}
