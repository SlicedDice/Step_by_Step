using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnterDetection : MonoBehaviour
{
    // this Script was written by Kai
    public GameObject bridge;

    //If the player passes through here start rotating the bridge
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (bridge.GetComponent<BridgeRotationScript>().GetIsRotating() == false)
            {
                bridge.GetComponent<BridgeRotationScript>().SetIsRotating(true);
            }
        }
    }
}

