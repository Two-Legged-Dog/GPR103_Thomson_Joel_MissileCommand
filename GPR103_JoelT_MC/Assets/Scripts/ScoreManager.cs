using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ScoreManager : MonoBehaviour
{

    private List<HighscoreEntry> highscoreList;
    public static ScoreManager instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {


    }


    public void SaveScores()
    {
        //sorting highscore list
        for (int i = 0; i < highscoreList.Count; i++)
        {
            for (int j = i + 1; j < highscoreList.Count; j++)
            {
                if (highscoreList[j].score > highscoreList[i].score)
                {    //swap
                    HighscoreEntry tmpEntry = highscoreList[i];
                    highscoreList[i] = highscoreList[j];
                    highscoreList[j] = tmpEntry;
                }
            }
        }
        SaveLoadManager.SaveScore(highscoreList);
    }

    public bool IsThisANewHighscore(int score)
    {
        highscoreList = SaveLoadManager.LoadScores();

        for (int i = 0; i < highscoreList.Count; i++)
        {
            if (score > highscoreList[i].score)
            {
                return true;
            }
        }
        return false;
    }

    public void AddNewHighscore(HighscoreEntry highscoreEntry)
    {
        highscoreList.Add(highscoreEntry);
        SaveScores();

        highscoreList.RemoveAt(5);
    }


}

[Serializable]
public class HighscoreEntry
{
    public int score;
    public string name;
}

