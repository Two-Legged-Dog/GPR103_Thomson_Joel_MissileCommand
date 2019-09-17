using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using System;

public class MenuManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI[] highscoreText;
    [SerializeField] private GameObject highscorePanel;
    
    public static MenuManager instance;

    private void Start()
    {/*
        highscoreList = new List<HighscoreEntry>()
        {
            new HighscoreEntry { score = 1000000, name = "AAA"},
            new HighscoreEntry { score = 100, name = "AAA"},
            new HighscoreEntry { score = 1050, name = "BAA"},
            new HighscoreEntry { score = 14400, name = "AAA"},
            new HighscoreEntry { score = 10, name = "AAA"},
        };
        SaveScores();
      */  
    }

    public void HighScores()
    {
        displayHighscores();
        highscorePanel.SetActive(true);
    }

    public void displayHighscores()
    {
        List<HighscoreEntry> highscoreList = SaveLoadManager.LoadScores();

        //Get highscores on UI
        for(int i = 0; i < 5; i++)
        {
            highscoreText[i].text = "    " + highscoreList[i].name + "                                      " + highscoreList[i].score;
        }
    }
}

