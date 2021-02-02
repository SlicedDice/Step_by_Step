using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EdgeDetection : MonoBehaviour
{
    public GameObject theOtherEdge;
    public GameObject lilypad;

    //If enemies reach the edge of the screen, inform the alien army manager
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Wet Ground")
        {
            lilypad.GetComponent<LinearMotion>().setEdgeTrue();

            theOtherEdge.SetActive(true);
            gameObject.SetActive(false);
        }
    }
}
