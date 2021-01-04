using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject playerCharacter; //Reference to the player character


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = playerCharacter.transform.position + new Vector3(-2.5f, 1.5f, -2.5f);
    }
}
