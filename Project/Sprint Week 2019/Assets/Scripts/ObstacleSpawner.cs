using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class ObstacleSpawner : MonoBehaviour
{
    public float timeSinceLastSpawn;
    public float coolDownTime;
    public float minSpawnDistance;
    public int maxSpawns;
    public GameObject obstaclePrefab;
    public List<GameObject> activeObstacles;

    public GridSystem gridSystem;
    private void Awake()
    {
        activeObstacles = new List<GameObject>();
        gridSystem = GridSystem.Instance;
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
                //Find the nearest point for the newly spawned obstacle/resource generator
                newObstacle.transform.position = new Vector3(Mathf.Round(transform.position.x) + 0.5f, Mathf.Round(transform.position.y) + 0.5f);

                //newObstacle.transform.position = new Vector3(DetermineNearestDot().transform.position.x, DetermineNearestDot().transform.position.y);
                newObstacle.GetComponent<Obstacle>().owner = this;
                activeObstacles.Add(newObstacle);
                timeSinceLastSpawn = 0;
            }
        }
    }

    public GameObject DetermineNearestDot()
    {
        var gridLocations = gridSystem.gridLocations;

        int nearestIndex = -1;

        for (int i = 1; i < gridLocations.Count; i++)
        {
            var shortestDistance = Vector3.Distance(transform.position, gridLocations[i].position);

            if(shortestDistance <= minSpawnDistance)
            {
                nearestIndex = i;
                minSpawnDistance = shortestDistance;
            }
        }

        return gridLocations[nearestIndex].gameObject;
    }
}
