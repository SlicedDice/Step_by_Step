using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public bool controlsByCamera = false;
    public bool inverted = false;

    public Button byCameraButton;
    public Button byCharacterButton;
    public Button invertedButton;
    public Button regularButton;


    public void Start()
    {
        string path = Application.persistentDataPath + "/walkingTitlePlayer.cgl";

        //Same as loadOptions(), make it work

        if (File.Exists(path)) loadOptions();
        else
        {
            changeButtonColors();
            saveOptions();
        }
    }


    public void Exit()
    {
        Application.Quit();
    }
    public void StartGame()
    {
        SceneManager.LoadScene(sceneName: "TestLevel");
    }

    public void StartNewGame()
    {
        string path = Application.persistentDataPath + "/walkingTitlePlayer.cgl";

        File.Delete(path);

        SceneManager.LoadScene(sceneName: "TestLevel");
    }

    public void saveOptions()
    {
        SaveSystem.SaveOptions(controlsByCamera, inverted);
    }
    public void loadOptions()
    {
        OptionData data = SaveSystem.LoadOptions();

        controlsByCamera = data.movementByCamera;
        inverted = data.inverted;

        changeButtonColors();
    }

    public void changeButtonColors()
    {
        if (controlsByCamera)
        {
            byCameraButton.interactable = false;
            byCharacterButton.interactable = true;
        }
        else if (!controlsByCamera)
        {
            byCharacterButton.interactable = false;
            byCameraButton.interactable = true;
        }
        if (inverted)
        {
            invertedButton.interactable = false;
            regularButton.interactable = true;
        }
        else if (!inverted)
        {
            regularButton.interactable = false;
            invertedButton.interactable = true;
        }
    }

    public void setControlsByCamera()
    {
        controlsByCamera = true;
        changeButtonColors();
    }
    public void setControlsByCharacter()
    {
        controlsByCamera = false;
        changeButtonColors();
    }
    public void setControlsInverted()
    {
        inverted = true;
        changeButtonColors();
    }
    public void setControlsRegular()
    {
        inverted = false;
        changeButtonColors();
    }
}
