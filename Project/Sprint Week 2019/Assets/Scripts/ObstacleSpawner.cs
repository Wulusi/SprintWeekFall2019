using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class ObstacleSpawner : MonoBehaviour
{
    public float timeSinceLastSpawn;
    public float coolDownTime;
    public float minSpawnDistance;
    public int maxSpawns;
    public GameObject obstaclePrefab, currentPlaceMent;
    public List<GameObject> activeObstacles;

    public GridSystem gridSystem;

    public Image BuildMeter;
    private void Awake()
    {
        BuildMeter = GetComponentInChildren<Image>();
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
                //Spawn to the right hand side of the player if there are available spawn locations
                if (gridSystem.gridLocations.Count > 0)
                {
                    //GameObject newObstacle = Instantiate(obstaclePrefab, transform.position + transform.up, transform.rotation);
                    GameObject newObstacle = PoolManager.Instance.SpawnFromPool(obstaclePrefab.name, transform.position + transform.up, transform.rotation);

                    //Find the nearest point for the newly spawned obstacle/resource generator
                    //newObstacle.transform.position = new Vector3(Mathf.Round(transform.position.x) + 0.5f, Mathf.Round(transform.position.y) + 0.5f);
                    //currentPlaceMent = newObstacle;
                    newObstacle.transform.rotation = Quaternion.identity;

                    GameObject closestDot = GetClosestEnemy(gridSystem.gridLocations).gameObject;


                    newObstacle.transform.position = Vector3.right + new Vector3(closestDot.transform.localPosition.x, closestDot.transform.localPosition.y);

                    //closestDot.GetComponent<DotTracker>().isOccupied = true;
                    gridSystem.gridLocations.Remove(closestDot.transform);
                    newObstacle.GetComponent<Obstacle>().currentDot = closestDot;
                    newObstacle.GetComponent<Obstacle>().owner = this;
                    closestDot.SetActive(false);

                    //Vector3 DebugVector = new Vector3(GetClosestEnemy(gridSystem.gridLocations).transform.localPosition.x, GetClosestEnemy(gridSystem.gridLocations).transform.localPosition.y);

                    //Debug.Log("Nearest Vector is " + DebugVector);
                    activeObstacles.Add(newObstacle);
                    timeSinceLastSpawn = 0;
                }
            }
        }
    }

    public Transform GetClosestEnemy(List<Transform> enemies)
    {
        Transform bestTarget = null;
        float closestDistanceSqr = Mathf.Infinity;
        Vector3 currentPosition = transform.position;
        foreach (Transform potentialTarget in enemies)
        {
            Vector3 directionToTarget = potentialTarget.position - currentPosition;
            float dSqrToTarget = directionToTarget.sqrMagnitude;
            if (dSqrToTarget < closestDistanceSqr)
            {
                closestDistanceSqr = dSqrToTarget;
                bestTarget = potentialTarget;
            }
        }

        return bestTarget;
    }
}
