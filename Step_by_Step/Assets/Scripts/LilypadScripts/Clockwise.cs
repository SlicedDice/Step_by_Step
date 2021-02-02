using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clockwise : MonoBehaviour
{
    public float degreeValue;

    // Update is called once per frame
    //rotates Object around z-axis in clockwise direction
    void Update()
    {
        transform.Rotate(0f, degreeValue * Time.deltaTime, 0f, Space.Self);
    }
}
