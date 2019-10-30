using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    public float timeSinceLastSpawn;
    public float coolDownTime;
    public int maxSpawns;
    public GameObject obstaclePrefab;
    public List<GameObject> activeObstacles;
    private void Awake()
    {
        activeObstacles = new List<GameObject>();
    }

    private void Update()
    {
        if (timeSinceLastSpawn < coolDownTime)
        {
            timeSinceLastSpawn += Time.deltaTime;
        }
    }
    public void SpawnObstacle()
    {
        if (timeSinceLastSpawn >= coolDownTime)
        {
            if (activeObstacles.Count < maxSpawns)
            {
                //GameObject newObstacle = Instantiate(obstaclePrefab, transform.position + transform.up, transform.rotation);
                GameObject newObstacle = PoolManager.Instance.SpawnFromPool(obstaclePrefab.name, transform.position + transform.up, transform.rotation);
                newObstacle.GetComponent<Obstacle>().owner = this;
                activeObstacles.Add(newObstacle);
                timeSinceLastSpawn = 0;
            }
        }
    }
}
