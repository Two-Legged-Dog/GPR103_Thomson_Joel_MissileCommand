using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class SaveLoadManager
{
    public static void SaveScore(List<HighscoreEntry> highscoreList)
    {
        // Create binary formatter
        BinaryFormatter myFormatter = new BinaryFormatter();

        //Create a local Path for saving
        string path = Application.persistentDataPath + "/data.dat";

        //Create File
        FileStream stream = new FileStream(path, FileMode.Create);

        // Write to file
        myFormatter.Serialize(stream, highscoreList);

        // Close file
        stream.Close();

    }


    public static List<HighscoreEntry> LoadScores()
    {
        string path = Application.persistentDataPath + "/data.dat";

        //Check if file is there
        if(File.Exists(path))
        {
            
            BinaryFormatter myFormatter = new BinaryFormatter();

            FileStream stream = new FileStream(path, FileMode.Open);

            // Read file and save highscore entry
            List<HighscoreEntry> highscores = myFormatter.Deserialize(stream) as List<HighscoreEntry>;

            //close
            stream.Close();

            return highscores;
        }
        else
        {
            Debug.LogError("Highscore File not found");
            return null;
        }
    }

}
