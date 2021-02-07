using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioTriggerScript : MonoBehaviour
{
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

    // Update is called once per frame
    void Update()
    {

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
