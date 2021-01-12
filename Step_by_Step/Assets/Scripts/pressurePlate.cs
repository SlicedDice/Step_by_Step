using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pressurePlate : MonoBehaviour
{
    public Animator connectedDoor;

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            connectedDoor.SetTrigger("Open Doors");
            GetComponent<Collider>().enabled = false;
        }
    }
}
