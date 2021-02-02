 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LinearMotion : MonoBehaviour
{
    public float moveDistance;
    public float northLimit;
    public float southLimit;
    public static bool reachedEdge = false;

    // Update is called once per frame
    void Update()
    {
        if(reachedEdge == true)
        {
            moveDistance = moveDistance*-1;
            reachedEdge = false;
        }
        transform.localPosition = transform.localPosition + new Vector3(moveDistance * Time.deltaTime, 0, 0);
    }
}


