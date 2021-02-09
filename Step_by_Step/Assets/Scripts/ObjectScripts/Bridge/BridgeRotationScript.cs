using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BridgeRotationScript : MonoBehaviour
{
    // this Script was written by Kai
    public float rotationSpeed;
    public float rotationSize;
    float thisRotation;
    public float hasRotated = 0;
    public bool isRotating;

    // Update is called once per frame
    //rotates Object around z-axis in clockwise direction
    void Update()
    {
        if (hasRotated < rotationSize)
        {
            if (isRotating == true)
            {
                thisRotation = rotationSpeed * Time.deltaTime;
                transform.Rotate(0f, thisRotation, 0f, Space.Self);
                hasRotated = hasRotated + thisRotation;
            }
        }
        if (hasRotated >= rotationSize)
        {
            isRotating = false;
        }
    }

    public void SetIsRotating(bool arg)
    {
        isRotating = arg;
    }
    public bool GetIsRotating()
    {
        return isRotating;
    }
}