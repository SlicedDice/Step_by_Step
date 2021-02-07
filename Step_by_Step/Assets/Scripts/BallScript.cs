using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallScript : MonoBehaviour
{
    private Vector3 respawnLocation = new Vector3();
    public AudioSource source0;
    public AudioSource source1;
    public AudioClip rollsound;
    public AudioClip splash;

    void Start()
    {
        respawnLocation = transform.position;
        source0.clip = rollsound;
        source1.clip = splash;
        source0.volume = gameObject.GetComponent<Rigidbody>().velocity.magnitude;
        source0.volume = Mathf.Clamp(source0.volume, 0f, 1f);
        source0.Play();
    }

    private void Update()
    {
        source0.volume = gameObject.GetComponent<Rigidbody>().velocity.magnitude;
        source0.volume = Mathf.Clamp(source0.volume, 0f, 0.8f);
    }

    void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "River")
        {
            source1.Play();
            respawn();
        }
    }

    public void respawn()
    {
        transform.position = respawnLocation;
    }
}