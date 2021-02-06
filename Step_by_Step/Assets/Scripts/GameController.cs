using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public GameObject gameOverScreen;
    public CharacterController characterController;
    public Animator audioFadeOut;

<<<<<<< HEAD
<<<<<<< Updated upstream
    private MusicController musicController; // george code


    public AudioSource audioSource;
  //   public AudioClip music0;
  //   public AudioClip music1;
=======
    private GameObject mainCamera;
    private CameraController mainCam;

    public AudioSource audioSource;
    public AudioClip music0;
    public AudioClip music1;
>>>>>>> Stashed changes
=======
     public AudioSource audioSource;
     public AudioClip music0;
     public AudioClip music1;
>>>>>>> parent of 0cd521de... audio triggers

    public GameObject Character;

    public Vector3 respawnLocation;
    public Quaternion respawnRotation;

    public bool dead = false;

    private bool boMusic0 = true;

    void Start()
    {
        mainCamera = GameObject.FindGameObjectWithTag("MainCamera");
        mainCam = mainCamera.GetComponent<CameraController>();
    }
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
<<<<<<< Updated upstream
        Instantiate(Character, respawnLocation, Quaternion.identity);
<<<<<<< HEAD

        musicController.MusicRestart(); // george code

=======
        Instantiate(Character, respawnLocation, respawnRotation);
    }

    public void rotateCamera(int rot)
    {
        mainCam.camLoc = rot;
        switch (rot)
        {
            case 0: //y = 48
                mainCamera.transform.rotation = new Quaternion(0.251807213f, 0.390980363f, -0.112111792f, 0.878156304f);
                break;
            case 1: //y = -70
                mainCamera.transform.rotation = new Quaternion(0.225788876f, -0.551357031f, 0.15809904f, 0.787419558f);
                break;
            case 2: //y = 180
                mainCamera.transform.rotation = new Quaternion(0f, 0.961261749f, -0.275637329f, 0f);
                break;
            case 3: //y = 135
                mainCamera.transform.rotation = new Quaternion(0.105481833f, 0.888090074f, -0.254655689f, 0.367858946f);
                break;
            default:
                break;
        }
>>>>>>> Stashed changes
=======
>>>>>>> parent of 0cd521de... audio triggers
    }
}
