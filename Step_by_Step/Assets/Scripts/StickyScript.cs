using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StickyScript : MonoBehaviour
{
    public GameObject character;
    public GameObject positionLink;
    private Vector3 lastLinkPosition;

    public bool linkedUp = false;
    public bool touching = false;
    public StickyScript otherFoot;

    void OnCollisionEnter(Collision collision)
    {
        if (!Input.GetMouseButton(0) && !Input.GetMouseButton(1))
        {
            if (collision.gameObject.tag == "Lilypads")
            {
                positionLink = collision.gameObject;
                lastLinkPosition = collision.gameObject.transform.position;
                if(!otherFoot.linkedUp) linkedUp = true;
                touching = true;
            }
        }
    }
    void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "Lilypads")
        {
            linkedUp = false;
            touching = false;
            if (otherFoot.touching) otherFoot.linkedUp = true;
        }
    }

    void Update()
    {
        Vector3 deltaLinkPosition = positionLink.transform.position - lastLinkPosition;
        lastLinkPosition = positionLink.transform.position;

        if(linkedUp) character.transform.localPosition = character.transform.localPosition + deltaLinkPosition;
    }
}
