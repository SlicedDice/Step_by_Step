using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GateScript : MonoBehaviour
{
    public int activatedPlates;

    public int connectedPlates;


    private Animator doorAnim;
    private AudioSource doorAudio;

    public bool gateOpen = false;
    void Start()
    {
        doorAnim = GetComponent<Animator>();
        doorAudio = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (activatedPlates >= connectedPlates && !gateOpen)
        {
            openGate();
            gateOpen = true;
        }
    }
     
    void openGate()
    {
        doorAnim.SetTrigger("Open Doors");
        doorAudio.Play();
    }

    public void increaseActivated() { activatedPlates++; }
    public void decreaseActivated() { activatedPlates--; }
}
