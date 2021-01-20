using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public GameObject gameOverScreen;
    public CharacterController characterController;
    public Animator audioFadeOut;

    private bool dead = false;

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
}
