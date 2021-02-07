using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkingSound : MonoBehaviour
{
    public AudioSource audioSource;

    public List<AudioClip> hardSurface = new List<AudioClip>();
    public List<AudioClip> swampSurface = new List<AudioClip>();
    public List<AudioClip> wetSurface = new List<AudioClip>();

    public GameObject character;

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
            else if (collision.gameObject.tag == "Wet Ground")
            {
                audioSource.clip = randomClip(wetSurface);
                audioSource.Play();
            }
        }
    }
    void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.tag == "Lilypads")
        {
            Vector3 pos = collision.transform.position;
            character.transform.localPosition = new Vector3(pos.x, character.transform.localPosition.y, pos.z);
        }
    }

    AudioClip randomClip(List<AudioClip> clipList)
    {
        int randomNumber = Random.Range(0, clipList.Count);
        return clipList[randomNumber];
    }
}
