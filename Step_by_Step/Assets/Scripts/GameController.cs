using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public GameObject gameOverScreen;
    public CharacterController characterController;
    public Animator audioFadeOut;

    public AudioSource audioSource;
    public AudioClip music0;
    public AudioClip music1;

    public GameObject Character;
    public Vector3 respawnLocation;

    public bool dead = false;

    private bool boMusic0 = true;

    void PauseGame() 
    {
        Time.timeScale = 0;
    }

    void ResumeGame()
    {
        Time.timeScale = 1;
    }

    public void GameOver()
    {
        if (!dead)
        {
            gameOverScreen.SetActive(true);
            audioFadeOut.SetTrigger("Death");
            

            dead = true;
            characterController.death(dead);
        }
        
    }

    public void reset()
    {
        gameOverScreen.SetActive(false);

        if (boMusic0) audioSource.clip = music1;
        else audioSource.clip = music0;

        audioSource.Play();
        boMusic0 = !boMusic0;

        audioFadeOut.SetTrigger("Reset");

        Destroy(characterController.gameObject);
        Instantiate(Character, respawnLocation, Quaternion.identity);
    }
}
