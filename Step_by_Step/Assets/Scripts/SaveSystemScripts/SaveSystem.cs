using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class SaveSystem
{
   

    public static void SavePlayer (CharacterController player, MusicController music)
    {

        string path = Application.persistentDataPath + "/walkingTitlePlayer.cgl";

        BinaryFormatter formatter = new BinaryFormatter();
        FileStream stream = new FileStream(path, FileMode.Create);

        PlayerData data = new PlayerData(player, music);

        formatter.Serialize(stream, data);
        stream.Close();
    }
    public static PlayerData LoadPlayer()
    {
        string path = Application.persistentDataPath + "/walkingTitlePlayer.cgl";

        if (File.Exists(path))
        {

            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            PlayerData data = formatter.Deserialize(stream) as PlayerData;
            stream.Close();

            return data;
        }
        else
        {
            Debug.LogError("Save file not found in " + path);
            return null;
        }
    }


    public static void SaveOptions(bool movementByCamera, bool inverted)
    {
        string path = Application.persistentDataPath + "/walkingTitleOptions.cgl";

        BinaryFormatter formatter = new BinaryFormatter();
        FileStream stream = new FileStream(path, FileMode.Create);

        OptionData data = new OptionData(movementByCamera, inverted);

        formatter.Serialize(stream, data);
        stream.Close();
    }
    public static OptionData LoadOptions()
    {
        string path = Application.persistentDataPath + "/walkingTitleOptions.cgl";

        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            OptionData data = formatter.Deserialize(stream) as OptionData;
            stream.Close();

            return data;
        }
        else return null;
    }
}
