 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LinearMotion : MonoBehaviour
{
    //this script was written by Kai
    public float moveDistance;
    public bool reachedEdge;

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

    public void setEdgeTrue()
    {
        moveDistance = moveDistance * -1;
    }
}


