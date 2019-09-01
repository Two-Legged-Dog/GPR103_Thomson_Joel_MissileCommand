using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMissileSpawn : MonoBehaviour
{
    [SerializeField] private GameObject missilePrefab;
    [SerializeField] private float Ypadding = 0.5f;

    private float minX, maxX;

    public int missilesThisRound = 10;
    public float missileDelay = .5f;

    float yVal;

    
    void Awake()
    {
        minX = Camera.main.ViewportToWorldPoint(new Vector3(0, 1, 0)).x;
        maxX = Camera.main.ViewportToWorldPoint(new Vector3(1, 1, 0)).x;

        float randomX = Random.Range(minX, maxX);
        yVal = Camera.main.ViewportToWorldPoint(new Vector3(0, 1, 0)).y;
       
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartRound()
    {
        StartCoroutine(SpawnMissiles());
    }

    public IEnumerator SpawnMissiles()
    {
        while (missilesThisRound > 0)
        {
            float randomX = Random.Range(minX, maxX);
          
            Instantiate(missilePrefab, new Vector3(randomX, yVal + Ypadding, 0), Quaternion.identity);

            missilesThisRound--;

            yield return new WaitForSeconds(missileDelay);
        }
    }
}
