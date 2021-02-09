using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EdgeDetection : MonoBehaviour
{
    //this script was written by Kai
    public GameObject theOtherEdge;
    public GameObject lilypad;

    //If the lilypad hits the collider make it go the other way
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Lilypads")
        {
            lilypad.GetComponent<LinearMotion>().setEdgeTrue();

            theOtherEdge.SetActive(true);
            gameObject.SetActive(false);
        }
    }
}
