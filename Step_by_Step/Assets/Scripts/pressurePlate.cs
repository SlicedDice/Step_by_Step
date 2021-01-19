using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pressurePlate : MonoBehaviour
{
    public Animator connectedDoor;
    private Vector3 pos = new Vector3();

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            connectedDoor.SetTrigger("Open Doors");
            GetComponent<Collider>().enabled = false;
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
