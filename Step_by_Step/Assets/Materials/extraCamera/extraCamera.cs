using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class extraCamera : MonoBehaviour
{
    private bool inControl = false;
    public GameObject character;
    public Camera mainCamera;

    public Camera thisOne;

    private float buffer = 0f;
    void Update()
    {
        if (buffer <= 0f)
        {
            if (Input.GetKey("v") && !inControl)
            {
                inControl = true;
                character.SetActive(false);
                mainCamera.enabled = false;
                thisOne.enabled = true;
                buffer = 0.1f;
            }
            else if (Input.GetKey("v") && inControl)
            {
                inControl = false;
                character.SetActive(true);
                mainCamera.enabled = true;
                thisOne.enabled = false;
                buffer = 0.1f;
            }

        }
        else
        {
            buffer -= Time.deltaTime;
        }

        if (inControl)
        {
            if (Input.GetKey("w")) transform.position += (transform.forward * 0.1f);
            if (Input.GetKey("a")) transform.position -= (transform.right * 0.1f);
            if (Input.GetKey("s")) transform.position -= (transform.forward * 0.1f);
            if (Input.GetKey("d")) transform.position += (transform.right * 0.1f);

            if (Input.GetMouseButton(0))
            {
                transform.Rotate(0, Input.GetAxis("Mouse X") * 5f, 0);
                transform.Rotate(-Input.GetAxis("Mouse Y") * 5f, 0, 0);
            }

        }
    }
}
