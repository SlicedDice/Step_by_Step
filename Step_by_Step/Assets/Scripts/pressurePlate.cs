using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pressurePlate : MonoBehaviour
{
    public Animator connectedDoor;
    public AudioSource conDoorAud;
    private Vector3 pos = new Vector3();

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Ground" || collision.gameObject.tag == "Wet Ground" || collision.gameObject.tag == "Swamp Ground")
        {
            GetComponent<AudioSource>().Play();
            connectedDoor.SetTrigger("Open Doors");
            
            GetComponent<Collider>().enabled = false;
            conDoorAud.Play();
        }
    }
    void Start()
    {
        pos = transform.position;
    }
    void Update()
    {
        if (transform.position.y > pos.y) transform.position = new Vector3(transform.position.x, pos.y, transform.position.z);
    }
}
