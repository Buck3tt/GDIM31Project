using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class SaveSystem { 
    public static void SavePlayerHighScore(PlayerData data)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/highscore.txt";
        FileStream fs = new FileStream(path, FileMode.Create);

        formatter.Serialize(fs, data);
        fs.Close();
    }

    public static PlayerData LoadPlayerHighScore()
    {
        string path = Application.persistentDataPath + "/highscore.txt";
        
        if(File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream fs = new FileStream(path, FileMode.Open);

            PlayerData data = formatter.Deserialize(fs) as PlayerData;
            fs.Close();

            return data;
        }
        else
        {
            PlayerData d = new PlayerData(); 
            SavePlayerHighScore(d);
            return d;
        }
    }
}

[System.Serializable]
public class PlayerData
{
    public int highScore;
    public int lastScore;
    public PlayerData (int current)
    {
        highScore = SaveSystem.LoadPlayerHighScore().highScore;
        if (current > highScore)
        {
            highScore = current;
        }
        lastScore = current;
    }

    public PlayerData ()
    {
        highScore = 0;
        lastScore = 0;
    }


}

