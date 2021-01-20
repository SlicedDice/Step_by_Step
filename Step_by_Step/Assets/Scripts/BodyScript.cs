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
        if (col.gameObject.tag == "Collectible")
        {
            Destroy(col.gameObject);

            audioSource.clip = pickUpSound;
            audioSource.Play();
            characterController.foundCollectible = true;
        }
        else if(col.gameObject.tag == "River")
        {
            audioSource.clip = waterSound;
            audioSource.Play();
            gameController.GameOver();
        }
        else if(col.gameObject.tag == "ResetZone")
        {
            characterController.setResetLocation(gameObject.transform.position);
        }
    }
}
