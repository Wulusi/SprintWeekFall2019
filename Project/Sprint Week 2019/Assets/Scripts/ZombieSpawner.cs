using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieSpawner : MonoBehaviour
{
    public GameObject[] baseTargets;
    public GameObject zombiePrefab;
    public int numZombiesToSpawn;
    public float timeToNextSpawn;
    public float minTimeToSpawn;
    public float maxTimeToSpawn;
    float spawnTimer;
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
                Vector2 spawnPos = new Vector2(Camera.main.transform.position.x + 1 + Camera.main.orthographicSize * Screen.width / Screen.height, Random.Range(Camera.main.transform.position.y - Camera.main.orthographicSize, Camera.main.transform.position.y + Camera.main.orthographicSize));

                //GameObject newZombie = Instantiate(zombiePrefab, spawnPos, Quaternion.identity);

                GameObject newZombie = PoolManager.Instance.SpawnFromPool(zombiePrefab.name, spawnPos, Quaternion.identity);

                //find closest target base and walk there
                int indexWithShortestDistance = 0;
                float shortestDistance = Mathf.Infinity;
                for (int i = 0; i < baseTargets.Length; i++)
                {
                    if (Vector2.Distance(newZombie.transform.position, baseTargets[i].transform.position) < shortestDistance)
                    {
                        indexWithShortestDistance = i;
                        shortestDistance = Vector2.Distance(newZombie.transform.position, baseTargets[i].transform.position);
                    }
                }

                newZombie.GetComponent<Zombie>().targetPoint = baseTargets[indexWithShortestDistance];
                numZombiesToSpawn--;
            }
        }
    }
}
