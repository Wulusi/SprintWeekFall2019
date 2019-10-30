using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    public ObstacleSpawner owner;

    public GameObject currentDot;

    GridSystem gridSystem;

    private void OnEnable()
    {
        owner = GetComponentInParent<ObstacleSpawner>();
        gridSystem = GridSystem.Instance;
    }

    private void OnDisable()
    {
        if (owner != null)
        {
            owner.activeObstacles.Remove(this.gameObject);
        }

        if (currentDot != null)
        {
            gridSystem.gridLocations.Add(currentDot.transform);
        }
    }

    //private void OnDestroy()
    //{
    //    owner.activeObstacles.Remove(this.gameObject);
    //}
}
