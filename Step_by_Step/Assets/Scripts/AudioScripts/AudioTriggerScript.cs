using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioTriggerScript : MonoBehaviour
{
    /* This script was entirely written by George, as he is the Sound Designer of the group.
     * To ensure that what he had in mind could be properly implemented, he chose to work on this script himself.
     */
    private MusicController musicController;
    public int audiotrack;
    public bool fadein;
    public bool switchsong;
    public int newregion;


    // Start is called before the first frame update
    void Start()
    {
        musicController = GameObject.FindGameObjectWithTag("GameController").GetComponent<MusicController>();
    }

    void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "Player")
        {


            if (switchsong)
            {
                // swtitchsong to newregion's song
                musicController.region = newregion;
                musicController.ChangeSong(newregion,musicController.song1);

            } else if (fadein) {
                // fadein
                musicController.FadeInTrack(audiotrack);
            } else
            {
                // fadeout
                musicController.FadeOutTrack(audiotrack);
            }
        }
    }


}
