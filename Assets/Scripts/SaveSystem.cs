using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class SaveSystem { 
    public static void SavePlayerHighScore(int score)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/highscore.txt";
        FileStream fs = new FileStream(path, FileMode.Create);

        formatter.Serialize(fs, score);
        fs.Close();
    }

    public static float LoadPlayerHighScore()
    {
        string path = Application.persistentDataPath + "/highscore.txt";
        
        if(File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream fs = new FileStream(path, FileMode.Open);

            int score = (int) formatter.Deserialize(fs);
            fs.Close();

            return score;
        }
        else
        {
            SavePlayerHighScore(0);
            return 0;
        }
    }
}



