using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class MainMenu : MonoBehaviour
{
    public bool controlsByCamera = false;;
    public bool inverted = false;

    public void Start()
    {
        //Same as loadOptions(), make it work
    }


    public void Exit()
    {
        Application.Quit();
    }
    public void StartGame()
    {
        SceneManager.LoadScene(sceneName:"TestLevel");
    }

    public void StartNewGame()
    {
        string path = Application.persistentDataPath + "/player.cgl";
        File.Delete(path);

        SceneManager.LoadScene(sceneName: "TestLevel");
    }
    
    public void saveOptions()
    {
        SaveSysten.SaveOptions(controlsByCamera, inverted);
    }
    public void loadOptions()
    {
        //Still have to fill this in, to make sure that the options properly display which option is currently active
    }
}
