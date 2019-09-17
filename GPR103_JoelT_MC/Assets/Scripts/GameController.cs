using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement; 

public class GameController : MonoBehaviour
{
    [SerializeField] private GameObject endRoundPanel;
    [SerializeField] private GameObject cityPrefab;
    [SerializeField] private GameObject highscorePanel;
    [SerializeField] private TMP_InputField newHighscoreInitials;
    EnemyMissileSpawn enemyMissileSpawn;

    public int score = 0;
    private int citiesRespawned = 0;
    public int level = 1;
    [SerializeField] private GameObject[] cityPositions;

    public float enemySpeed = 5f;
    public int citiesLeft = 0;
    [SerializeField] private float enemySpeedMultiplier = 2f;
    public int missilesBattery1 = 10;
    public int missilesBattery2 = 10;
    public int missilesBattery3 = 10;
    public int totalPlayerMissiles;
    private int enemyMissilesThisRound = 20;
    public int enemyMissilesLeftInRound = 0;
    [SerializeField] private int missileEndOfRoundPoints = 5;
    [SerializeField] private int cityEndOfRoundPoints = 100;

    private int missileDestroyPoints = 25;

    private MenuManager myMenuManager;
    private ScoreManager myScoreManager;

    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI levelText;
    [SerializeField] private TextMeshProUGUI countdownText;

    [SerializeField] private TextMeshProUGUI leftOverMissileText;
    [SerializeField] private TextMeshProUGUI leftOverCityText;
    [SerializeField] private TextMeshProUGUI totalBonusText;

    private bool roundOver = false;
    public bool isGameOver = false;

    // Start is called before the first frame update
    void Start()
    {
        citiesLeft = GameObject.FindObjectsOfType<City>().Length;
        Debug.Log(citiesLeft);
        enemyMissileSpawn = GameObject.FindObjectOfType<EnemyMissileSpawn>();
        myMenuManager = GameObject.FindObjectOfType<MenuManager>();
        myScoreManager = GameObject.FindObjectOfType<ScoreManager>();

        UpdateScore();
        UpdateLevel();

        StartRound();

    }

    // Update is called once per frame
    void Update()
    {
        
        if(citiesLeft <= 0 )
        {
            isGameOver = true;
            if(myScoreManager.IsThisANewHighscore(score))
            {
                highscorePanel.SetActive(true);
            }
            else
            {
                SceneManager.LoadScene("EndGame");
            }
        }
        totalPlayerMissiles = missilesBattery1 + missilesBattery2 + missilesBattery3;
        if (enemyMissilesLeftInRound <= 0 && !roundOver && !isGameOver)
        {
            roundOver = true;
            StartCoroutine(EndOfRound());
        }
    }

    public void UpdateScore()
    {
        scoreText.text = "Score: " + score;
    }

    public void UpdateLevel()
    {
        levelText.text = "Level: " + level;
    }
    public void ResetMissiles()
    {
           missilesBattery1 = 6;
           missilesBattery2 = 6;
           missilesBattery3 = 6;
    }

    public void MissileDestroyedPoints()
    {
        score += missileDestroyPoints;
        EnemyMissileDestroyed();
        UpdateScore();
    }
    public void EnemyMissileDestroyed()
    {
        enemyMissilesLeftInRound--;
    }

    public void MissileLauncherHit()
    {
        //////
        return;
    }


    private void StartRound()
    {
        enemyMissileSpawn.missilesThisRound = enemyMissilesThisRound;
        enemyMissilesLeftInRound = enemyMissilesThisRound;
        enemyMissileSpawn.StartRound();
        ResetMissiles();
    }

    public IEnumerator EndOfRound()
    {
        yield return new WaitForSeconds(.5f);
        endRoundPanel.SetActive(true);
        int leftOverMissileBonus = missilesBattery1 + missilesBattery2 + missilesBattery3 * missileEndOfRoundPoints;

        City[] cities = GameObject.FindObjectsOfType<City>();
        int leftOverCityBonus = cities.Length * cityEndOfRoundPoints;

        int totalBonus = leftOverCityBonus + leftOverMissileBonus;

        //Score multiplier for rounds
        if(level >=3 && level < 5)
        {
            totalBonus *= 2;
        }
        else if (level >= 5 && level < 7)
        {
            totalBonus *= 3;
        }
        else if (level >= 7 && level < 9)
        {
            totalBonus *= 4;
        }
        else if (level >= 9 && level < 11)
        {
            totalBonus *= 5;
        }
        else if (level >= 11)
        {
            totalBonus *= 6;
        }

        leftOverMissileText.text = "Left over missile bonus: " + leftOverMissileBonus;
        leftOverCityText.text = "Left over city bonus: " + leftOverCityBonus;
        totalBonusText.text = "Total Bonus: " + totalBonus;

        score += totalBonus;
        UpdateScore();


        //Add city if 10k
        if (score / 10000 > citiesRespawned)
        {
            foreach(GameObject go in cityPositions)
            {
                if (go.GetComponentInChildren<City>() == null)
                {
                    Instantiate(cityPrefab, go.transform.position, Quaternion.identity, go.transform);
                    citiesLeft++;
                    citiesRespawned++;
                    break;
                }
            }
        }

        //Countdown for in between rounds
        countdownText.text = "3";
        yield return new WaitForSeconds(1f);
        countdownText.text = "2";
        yield return new WaitForSeconds(1f);
        countdownText.text = "1";
        yield return new WaitForSeconds(1f);

        endRoundPanel.SetActive(false);

        roundOver = false;

        //new round settings
        enemySpeed *= enemySpeedMultiplier;

        StartRound();
        UpdateLevel();
    }
    public void SubmitButton()
    {
        myScoreManager.AddNewHighscore(new HighscoreEntry { score = this.score, name = newHighscoreInitials.text });
        SceneManager.LoadScene(0);

    }
}
