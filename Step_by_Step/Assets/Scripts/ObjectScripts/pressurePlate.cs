using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pressurePlate : MonoBehaviour
{
    public GateScript connectedGate;
    private Vector3 pos = new Vector3();

    private bool activated = false;

    public ParticleSystem particleSystem;
    public Color activatedShineColor;
    public Color basicShineColor;

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Ground" || collision.gameObject.tag == "Wet Ground" || collision.gameObject.tag == "Swamp Ground")
        {

            if (!activated)
            {
                GetComponent<AudioSource>().Play();
                connectedGate.increaseActivated();
            }
            activated = true;

            particleSystem.startColor = activatedShineColor;
            GetComponent<Rigidbody>().velocity = new Vector3();
            //GetComponent<Collider>().enabled = false;

            
        }
    }
    void OnCollisionExit(Collision collision)
    {
        if(collision.gameObject.tag == "Ground" || collision.gameObject.tag == "Wet Ground" || collision.gameObject.tag == "Swamp Ground")
        {
            //GetComponent<Collider>().enabled = true;
            GetComponent<Rigidbody>().velocity = new Vector3();

            particleSystem.startColor = basicShineColor;
            connectedGate.activatedPlates--;
            activated = false;
        }
    }
    void Start()
    {
        pos = transform.position;
        particleSystem.startColor = basicShineColor;
    }
    void FixedUpdate()
    {
        if (transform.position.y > pos.y) transform.position = new Vector3(transform.position.x, pos.y, transform.position.z);

        if (activated) pushUpPlate();
    }

    void pushUpPlate()
    {
        //transform.position = new Vector3(transform.position.x, pos.y + 0.01f, transform.position.z);

        GetComponent<Rigidbody>().AddForce(transform.up * 0.01f);
    }
}
