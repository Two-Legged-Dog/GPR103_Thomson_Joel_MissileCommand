using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement; 

public class GameController : MonoBehaviour
{
    [SerializeField] private GameObject endRoundPanel;

    EnemyMissileSpawn enemyMissileSpawn;

    public int score = 0;
    public int level = 1;
    public float enemySpeed = 5f;
    public int citiesLeft = 0;
    [SerializeField] private float enemySpeedMultiplier = 2f;
    public int missilesBattery1 = 6;
    public int missilesBattery2 = 6;
    public int missilesBattery3 = 6;
    public int totalPlayerMissiles;
    private int enemyMissilesThisRound = 20;
    private int enemyMissilesLeftInRound = 0;
    [SerializeField] private int missileEndOfRoundPoints = 5;
    [SerializeField] private int cityEndOfRoundPoints = 100;

    private int missileDestroyPoints = 25;


    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI levelText;
    [SerializeField] private TextMeshProUGUI countdownText;

    [SerializeField] private TextMeshProUGUI leftOverMissileText;
    [SerializeField] private TextMeshProUGUI leftOverCityText;
    [SerializeField] private TextMeshProUGUI totalBonusText;

    private bool roundOver = false;

    // Start is called before the first frame update
    void Start()
    {
        citiesLeft = GameObject.FindObjectsOfType<City>().Length;
        Debug.Log(citiesLeft);
        enemyMissileSpawn = GameObject.FindObjectOfType<EnemyMissileSpawn>();

        UpdateScore();
        UpdateLevel();

        StartRound();

    }

    // Update is called once per frame
    void Update()
    {
        totalPlayerMissiles = missilesBattery1 + missilesBattery2 + missilesBattery3;
        if (enemyMissilesLeftInRound <= 0 && !roundOver)
        {
            roundOver = true;
            StartCoroutine(EndOfRound());
        }
        if(citiesLeft <= 0 )
        {
            SceneManager.LoadScene("EndGame");
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

        leftOverMissileText.text = "Left over missile bonus: " + leftOverMissileBonus;
        leftOverCityText.text = "Left over city bonus: " + leftOverCityBonus;
        totalBonusText.text = "Total Bonus: " + totalBonus;

        score += totalBonus;
        UpdateScore();

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
}
