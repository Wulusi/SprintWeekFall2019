using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieSpawner : MonoBehaviour
{
    public GameObject[] baseTargets;
    public GameObject[] zombiePrefabs;
    public int numZombiesToSpawn;
    public float timeToNextSpawn;
    public float minTimeToSpawn;
    public float maxTimeToSpawn;
    float spawnTimer;
    public Vector2[] pathOne;
    public Vector2[] pathTwo;
    public Vector2[] pathThree;
    public Vector2[] pathFour;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (numZombiesToSpawn > 0)
        {
            spawnTimer += Time.deltaTime;
            if (spawnTimer >= timeToNextSpawn)
            {
                spawnTimer = 0;
                timeToNextSpawn = Random.Range(minTimeToSpawn, maxTimeToSpawn);
                
                //spawn on right edge of camera at random height
                Vector2 spawnPos = new Vector2(Camera.main.transform.position.x + 1 + Camera.main.orthographicSize * Screen.width / Screen.height, 0);

                //GameObject newZombie = Instantiate(zombiePrefab, spawnPos, Quaternion.identity);

                GameObject newZombie = PoolManager.Instance.SpawnFromPool(zombiePrefabs[Mathf.FloorToInt(Random.Range(0,3))].name, spawnPos, Quaternion.identity);

                //find closest target base and walk there
                //int indexWithShortestDistance = 0;
                //float shortestDistance = Mathf.Infinity;
                //for (int i = 0; i < baseTargets.Length; i++)
                //{
                //    if (Vector2.Distance(newZombie.transform.position, baseTargets[i].transform.position) < shortestDistance)
                //    {
                //        indexWithShortestDistance = i;
                //        shortestDistance = Vector2.Distance(newZombie.transform.position, baseTargets[i].transform.position);
                //    }
                //}
                int randPath = Mathf.FloorToInt(Random.Range(1, 5));
                if (randPath == 1)
                {
                    newZombie.GetComponent<Zombie>().pathToFollow = pathOne;
                } else if (randPath == 2)
                {
                    newZombie.GetComponent<Zombie>().pathToFollow = pathTwo;
                }
                else if (randPath == 3)
                {
                    newZombie.GetComponent<Zombie>().pathToFollow = pathThree;
                }
                else if (randPath == 4)
                {
                    newZombie.GetComponent<Zombie>().pathToFollow = pathFour;
                }

                newZombie.transform.position = new Vector2(newZombie.transform.position.x, newZombie.GetComponent<Zombie>().pathToFollow[0].y);
                numZombiesToSpawn--;
            }
        }
    }
}
