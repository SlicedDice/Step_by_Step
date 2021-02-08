using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class MainMenu : MonoBehaviour
{
    public void Exit()
    {
        Application.Quit();
    }
    public void StartGame()
    {
        SceneManager.LoadScene(sceneName:"TestLevel");
    }

    public static void StartNewGame()
    {
        string path = Application.persistentDataPath + "/player.cgl";
        File.Delete(path);
    }
}
