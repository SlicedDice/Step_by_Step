using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BodyScript : MonoBehaviour
{
    public GameController gameController;
    public CharacterController characterController;

    public AudioClip waterSound;
    public AudioClip impactSound;
    public AudioClip pickUpSound;

    private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
    }
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Ground" || collision.gameObject.tag == "Wet Ground" || collision.gameObject.tag == "Swamp Ground"|| collision.gameObject.tag == "River")
        {
            gameController.GameOver();
            audioSource.clip = impactSound;
            audioSource.Play();
        }
    }

    void OnTriggerEnter(Collider col)
    {
        gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();

        if (col.gameObject.tag == "Collectible")
        {
            Destroy(col.gameObject);

            audioSource.clip = pickUpSound;
            audioSource.Play();

            switch (col.gameObject.name)
            {
                case "BeanstalkCollectible":
                    characterController.foundBeanstalkCollectible = true;
                    gameController.foundBeanstalkCollectible = true;
                    gameController.rotateCamera(1);
                    break;
                case "ShipCollectible":
                    characterController.foundShipCollectible = true;
                    gameController.foundShipCollectible = true;
                    gameController.rotateCamera(2);
                    break;
                case "RuinCollectible":
                    characterController.foundRuinCollectible = true;
                    gameController.foundRuinCollectible = true;
                    gameController.rotateCamera(3);
                    break;
                default:
                    gameController.rotateCamera(0);
                    break;
            }
        }
        else if(col.gameObject.tag == "River")
        {
            audioSource.clip = waterSound;
            audioSource.Play();
            gameController.GameOver();
        }
        else if(col.gameObject.tag == "ResetZone")
        {
            Vector3 resLoc = col.gameObject.transform.position;
            Quaternion resRot = col.gameObject.transform.rotation;
            gameController.respawnLocation = resLoc;
            gameController.respawnRotation = resRot;
        }
    }
}
