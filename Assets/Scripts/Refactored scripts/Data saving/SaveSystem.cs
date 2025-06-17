using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public static class SaveSystem
{
    private static string SavePath => Application.persistentDataPath + "/gamesave.save";

    public static void SaveGame(GameSaveData data)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        using (FileStream stream = new FileStream(SavePath, FileMode.Create))
        {
            formatter.Serialize(stream, data);
        }
    }

    public static GameSaveData LoadData()
    {
        if (File.Exists(SavePath))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            using (FileStream stream = new FileStream(SavePath, FileMode.Open))
            {
                return formatter.Deserialize(stream) as GameSaveData;
            }
        }
        else
        {
            Debug.LogWarning("No save file found.");
            return null;
        }
    }

    public static void DeleteSaveData()
    {
        if (File.Exists(SavePath))
        {
            File.Delete(SavePath);
            Debug.Log("Save file deleted.");
        }
        else
        {
            Debug.LogWarning("No save file found to delete.");
        }
    }

}
