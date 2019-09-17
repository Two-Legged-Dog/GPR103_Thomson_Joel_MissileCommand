using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMissile : MonoBehaviour
{
    [SerializeField] private float speed = 5f;
    [SerializeField] private GameObject explosionPrefab;
    GameObject[] groundBuildings;

    private GameController gameController;

    public float lineDrawSpeed = 6f;

    Vector3 target;

    private float randomTimer;
    [SerializeField] private GameObject missilePrefab;

    void Start()
    {
        gameController = GameObject.FindObjectOfType<GameController>();
        groundBuildings = GameObject.FindGameObjectsWithTag("GroundBuildings");
        target = groundBuildings[Random.Range(0, groundBuildings.Length)].transform.position;
        speed = gameController.enemySpeed;
        
        //Split Missile
        randomTimer = Random.Range(0.1f, 50f);
        randomTimer = randomTimer / gameController.enemySpeed;
        Invoke("SplitMissile", randomTimer);
    }

    void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, target, speed * Time.deltaTime);
        if (transform.position == target)
        {
            MissileExplode();
        }
    }


    private void OnTriggerEnter2D(Collider2D col)
    {
        if(col.tag == "GroundBuildings")
        {
            gameController.EnemyMissileDestroyed();
            MissileExplode();
            if (col.GetComponent<EnemyMissile>() != null)
            {
                //gameController.MissileLauncherHit();
                return;
            }
            gameController.citiesLeft--;
            Destroy(col.gameObject);
        }
        else if (col.tag == "Explosions")
        {
            gameController.MissileDestroyedPoints();
            MissileExplode();
        }
    }
    private void MissileExplode()
    {
        Instantiate(explosionPrefab, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
    private void SplitMissile()
    {
        float yVal = Camera.main.ViewportToWorldPoint(new Vector3(0, .25f, 0)).y;
        if (transform.position.y >= yVal)
        {
            gameController.enemyMissilesLeftInRound++;
            Instantiate(missilePrefab, transform.position, Quaternion.identity);
        }
    }
}
