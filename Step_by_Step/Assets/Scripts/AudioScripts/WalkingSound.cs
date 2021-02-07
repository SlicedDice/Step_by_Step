using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkingSound : MonoBehaviour
{
    public AudioSource audioSource;

    public List<AudioClip> hardSurface = new List<AudioClip>();
    public List<AudioClip> swampSurface = new List<AudioClip>();
    public List<AudioClip> wetSurface = new List<AudioClip>();


    void OnCollisionEnter(Collision collision)
    {
        if (!Input.GetMouseButton(0) && !Input.GetMouseButton(1))
        {
            if (collision.gameObject.tag == "Ground" || collision.gameObject.tag == "Pressure Plate")
            {
                audioSource.clip = randomClip(hardSurface);
                audioSource.Play();
            }
            else if (collision.gameObject.tag == "Swamp Ground")
            {
                audioSource.clip = randomClip(swampSurface);
                audioSource.Play();
            }
            else if (collision.gameObject.tag == "Wet Ground" || collision.gameObject.tag == "Lilypads")
            {
                audioSource.clip = randomClip(wetSurface);
                audioSource.Play();
            }
        }
    }


    AudioClip randomClip(List<AudioClip> clipList)
    {
        int randomNumber = Random.Range(0, clipList.Count);
        return clipList[randomNumber];
    }
}
