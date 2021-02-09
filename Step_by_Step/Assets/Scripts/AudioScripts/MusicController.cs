using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class MusicController : MonoBehaviour
{
    /* This script was mostly written by George, as he is the Sound Designer of the group.
     * To ensure that what he had in mind could be properly implemented, he chose to work on this script himself.
     */

    // array of sources playing tracks 1 to 3
    public AudioSource [] audioSources; // careful. ends with an S (audiosource"S")

    // array containing 3 currently played tracks
    private AudioClip [] currentsong;

    // array containing 3 tracks of the given song
    public AudioClip [] region1song1;
    public AudioClip [] region1song2;
    public AudioClip [] region2song1;
    public AudioClip [] region2song2;
    public AudioClip [] region3song1;
    public AudioClip [] region3song2;

    public DataTracking tracker;

    private bool fading = false; // true during fade out of music after death
    public bool dead = false; // true if dead

    public int region = 1; // stores information about the region the player is in
    public bool song1 = true; // true if song1, false if song2



    // Start is called before the first frame update
    void Start()
    {
        string path = Application.persistentDataPath + "/walkingTitlePlayer.cgl";

        if (File.Exists(path)) loadMusicSettings();
        UpdateCurrentSong();

        audioSources[0].clip = currentsong[0];
        audioSources[1].clip = currentsong[1];
        audioSources[2].clip = currentsong[2];

        tracker = GameObject.FindGameObjectWithTag("Foot").GetComponent<DataTracking>();

        MusicRestart1();
    }

    //This next method was written by Patrick
    private void loadMusicSettings()
    {

        PlayerData data = SaveSystem.LoadPlayer();

        region = data.musicRegion;
        song1 = data.musicSong;
        
    }

    // function used to fade in and out tracks
    public IEnumerator StartFade(AudioSource audioSource, float duration, float targetVolume, bool stopping) // changes volume of "audiosource" in "duration" seconds to "targetVolume", copied of the internet by george
    {
        float currentTime = 0;
        float start = audioSource.volume;

        while (currentTime < duration)
        {
            currentTime += Time.deltaTime;
            audioSource.volume = Mathf.Lerp(start, targetVolume, currentTime / duration);
            yield return null;
        }
        if (stopping) audioSource.Stop();
        yield break;
    }

    // function used to fade in and out tracks with pitch shift
    public IEnumerator StartFadePitch(AudioSource audioSource, float duration, float targetVolume, float targetPitch, bool stopping) // changes volume of "audiosource" in "duration" seconds to "targetVolume" and pitch to "targetPitch"
    {
        float currentTime = 0;
        float startVol = audioSource.volume;
        float startPitch = audioSource.pitch;

        while (currentTime < duration)
        {
            fading = true;
            currentTime += Time.deltaTime;
            audioSource.volume = Mathf.Lerp(startVol, targetVolume, currentTime / duration);
            audioSource.pitch = Mathf.Lerp(startPitch, targetPitch, currentTime / duration);
            yield return null;
        }

        if (stopping) audioSource.Stop();
        fading = false;
        yield break;
    }



    public void UpdateCurrentSong() // updates current song, used after updating region and song1 when restarting music
    {
        Song1or2();

        if (region == 1 && song1) currentsong = region1song1;
        else if (region == 1 && !song1) currentsong = region1song2;
        else if (region == 2 && song1) currentsong = region2song1;
        else if (region == 2 && !song1) currentsong = region2song2;
        else if (region == 3 && song1) currentsong = region3song1;
        else if (region == 3 && !song1) currentsong = region3song2;
    }

    public void Song1or2()
    {
        float stepdistance = tracker.AverageStepDistance();
        float steptime = tracker.AverageStepTime();
        float standtime = tracker.AverageTimeBetweenSteps();

        int points = 0; // wonky points

        if (stepdistance <= 1.2f)
        {
            points++;
        }
        if (steptime <= 2.5f)
        {
            points++;
        }
        if (standtime <= 0.6f)
        {
            points++;
        }


        if (points > 1)
        {
            song1 = false;
        }
        else
        {
            song1 = true;
        }

        //    Debug.Log("             " + points + "       " + song1);

        tracker.ResetData();
    }

    public void MusicStop() // fades music out with pitch shift, used when character dies
    {
        // UpdateCurrentSong();

        StartCoroutine(StartFadePitch(audioSources[0], 3f, 0f, 0.5f, true));
        StartCoroutine(StartFadePitch(audioSources[1], 3f, 0f, 0.5f, true));
        StartCoroutine(StartFadePitch(audioSources[2], 3f, 0f, 0.5f, true));

        dead = true;
    }

    public void MusicRestart() // fades music back in after 3 seconds if fading out, or instantly if not fading out, use when starting game and restarting from checkpoint; press r does this
    {
        tracker = GameObject.FindGameObjectWithTag("Foot").GetComponent<DataTracking>();
        UpdateCurrentSong();

        if (fading == true)
        {
            Invoke("MusicRestart1", 3f);
        }
        else
        {
            MusicRestart1();
        }
    }

    public void MusicRestart1() // fades track 0 and 1 back in, track 2 plays at volume 0 to stay in sync
    {
        dead = false;

        audioSources[0].clip = currentsong[0];
        audioSources[1].clip = currentsong[1];
        audioSources[2].clip = currentsong[2];

        if (audioSources[0].isPlaying == false)
        {
            audioSources[0].Play();
        }
        audioSources[0].volume = 0f;
        audioSources[0].pitch = 1f;
        StartCoroutine(StartFade(audioSources[0], 3f, 0.8f, false));

        if (audioSources[1].isPlaying == false)
        {
            audioSources[1].Play();
        }
        audioSources[1].volume = 0f;
        audioSources[1].pitch = 1f;
        StartCoroutine(StartFade(audioSources[1], 3f, 0.8f, false));

        if (audioSources[2].isPlaying == false)
        {
            audioSources[2].Play();
        }
        audioSources[2].volume = 0f;
        audioSources[2].pitch = 1f;
    }

    public void ChangeSong(int newregion, bool songOne) // changes song from current to the one with the parameters, use when entering new region
    {
        region = newregion;
        song1 = songOne;

        UpdateCurrentSong();

        StartCoroutine(StartFade(audioSources[0], 3f, 0f, true));
        StartCoroutine(StartFade(audioSources[1], 3f, 0f, true));
        StartCoroutine(StartFade(audioSources[2], 3f, 0f, true));

//        currentsong = newSong;
        Invoke("ChangeSong1", 3.1f);

    }

    public void ChangeSong1()
    {
        audioSources[0].clip = currentsong[0];
        audioSources[1].clip = currentsong[1];
        audioSources[2].clip = currentsong[2];

        if (dead == false)
        {
            MusicRestart();
        }
    }

    public void FadeInTrack(int track) // fades in track with number "track", use when hitting fade trigger
    {
        if (dead == false)
        {
            audioSources[track].pitch = 1f;
            StartCoroutine(StartFade(audioSources[track], 3f, 0.8f, false));
        }
    }

    public void FadeOutTrack(int track) // fades out track with number "track", use when hitting fade trigger
    {
        if (dead == false)
        {
            StartCoroutine(StartFade(audioSources[track], 3f, 0f, false));
        }
    }

}
