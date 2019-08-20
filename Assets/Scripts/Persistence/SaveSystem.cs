using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;    

public static class SaveSystem
{
    private static string dataFileName = "/player.db";

    public static void SavePlayerData(int highscore, int currentLevel, int currentScore)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + dataFileName;
        FileStream stream = new FileStream(path, FileMode.Create);

        PlayerData playerData = new PlayerData(highscore, currentLevel, currentScore);
        formatter.Serialize(stream, playerData);
        stream.Close();
    }

    public static PlayerData LoadPlayerData()
    {
        string path = Application.persistentDataPath + dataFileName;
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
}
