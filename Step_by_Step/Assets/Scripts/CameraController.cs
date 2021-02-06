using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject playerCharacter; //Reference to the player character
    public int camLoc;

    // Start is called before the first frame update
    void Start()
    {
        camLoc = 0;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = playerCharacter.transform.position + camLocation(camLoc);
    }

    Vector3 camLocation(int loc)
    {
        switch (loc)
        {
            case 0:
                return new Vector3(-5f, 5f, -5f);
            case 1:
                return new Vector3(6.5f, 5f, -0.5f);
            case 2:
                return new Vector3(0.5f, 5f, 7.5f);
            case 3:
                return new Vector3(-5f, 5f, 6.5f);
            default:
                return new Vector3(0f, 0f, 0f);
        }
    }
}
