using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMissile : MonoBehaviour
{
    [SerializeField] private float speed = 5f;
    [SerializeField] private GameObject explosionPrefab;
    GameObject[] groundBuildings;

    private GameController gameController;

    Vector3 target;

    


    // Start is called before the first frame update
    void Start()
    {
        gameController = GameObject.FindObjectOfType<GameController>();
        groundBuildings = GameObject.FindGameObjectsWithTag("GroundBuildings");
        
        target = groundBuildings[Random.Range(0, groundBuildings.Length)].transform.position;

        speed = gameController.enemySpeed;
        Debug.Log(speed);

    }

    // Update is called once per frame
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
}
