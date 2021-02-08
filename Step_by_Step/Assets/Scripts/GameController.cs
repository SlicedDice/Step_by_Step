using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public GameObject gameOverScreen;
    public CharacterController characterController;
    //  public Animator audioFadeOut;

    private MusicController musicController; // george code


    public AudioSource audioSource;
    //   public AudioClip music0;
    //   public AudioClip music1;

    public GameObject Character;
    public Vector3 respawnLocation;
    public Quaternion respawnRotation;

    public bool dead = false;

    //  private bool boMusic0 = true;

    private GameObject mainCamera;
    private CameraController mainCam;

    public GameObject pauseMenu;
    private bool activePauseMenu = false;

    public bool foundBeanstalkCollectible = false; //Checks to see if the collectible items were picked up
    public bool foundShipCollectible = false;
    public bool foundRuinCollectible = false;

    public bool invertedControls;
    public bool movementByCamera;

    private void Start()
    {
        musicController = GameObject.FindGameObjectWithTag("GameController").GetComponent<MusicController>(); // Part of George's code

        mainCamera = GameObject.FindGameObjectWithTag("MainCamera");
        mainCam = mainCamera.GetComponent<CameraController>();

        characterController = GameObject.FindGameObjectWithTag("Player").GetComponent<CharacterController>();

        LoadPlayer();
        ResumeGame();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (activePauseMenu)
            {
                pauseMenu.SetActive(false);
                activePauseMenu = false;
                ResumeGame();
            }
            else if (!activePauseMenu)
            {
                pauseMenu.SetActive(true);
                activePauseMenu = true;
                PauseGame();
            }
        }
    }
    public void SavePlayer()
    {
        SaveSystem.SavePlayer(characterController);
    }
    public void LoadPlayer()
    {
        if(SaveSystem.LoadPlayer() != null)
        {
            PlayerData data = SaveSystem.LoadPlayer();

            respawnLocation = new Vector3(data.respawnLocation[0], data.respawnLocation[1], data.respawnLocation[2]);
            respawnRotation = new Quaternion(data.respawnRotation[0], data.respawnRotation[1], data.respawnRotation[2], data.respawnRotation[3]);

            characterController.loadPlayer(data);

            characterController.invertedControls = data.invertedControls;
            characterController.movementByCamera = data.movementByCamera;

            reset();

        }
    }
    public void ExitGame()
    {
        SceneManager.LoadScene(sceneName: "Main Menu");
    }
    public void PauseGame()
    {
        Time.timeScale = 0;
    }
    public void ResumeGame()
    {
        Time.timeScale = 1;
    }
    public void toggleInverted()
    {
        characterController.invertedControls = !characterController.invertedControls;
    }
    public void toggleControls()
    {
        characterController.movementByCamera = !characterController.movementByCamera;
    }

    public void GameOver()
    {
        if (!dead)
        {
            gameOverScreen.SetActive(true);
            //    audioFadeOut.SetTrigger("Death");


            dead = true;
            characterController.death(dead);

            musicController.MusicStop(); // george code

        }

    }

    public void reset()
    {
        gameOverScreen.SetActive(false);

        //   if (boMusic0) audioSource.clip = music1;
        //   else audioSource.clip = music0;

        //   audioSource.Play();
        //   boMusic0 = !boMusic0;

        //   audioFadeOut.SetTrigger("Reset");

        invertedControls = characterController.invertedControls;
        movementByCamera = characterController.movementByCamera;

        Destroy(characterController.gameObject);
        GameObject c = Instantiate(Character, respawnLocation, respawnRotation);
        characterController = c.GetComponent<CharacterController>();

        characterController.foundBeanstalkCollectible = foundBeanstalkCollectible;
        characterController.foundShipCollectible = foundShipCollectible;
        characterController.foundRuinCollectible = foundRuinCollectible;
        characterController.invertedControls = invertedControls;
        characterController.movementByCamera = movementByCamera;

        musicController.MusicRestart(); // george code
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
                mainCamera.transform.rotation = new Quaternion(0.0120231612f, 0.960346818f, -0.275374949f, 0.0419297516f);
                break;
            case 3: //y = 135
                mainCamera.transform.rotation = new Quaternion(0.105481833f, 0.888090074f, -0.254655689f, 0.367858946f);
                
                break;
            default:
                break;
        }
    }
}