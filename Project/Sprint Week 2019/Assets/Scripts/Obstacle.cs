using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    public ObstacleSpawner owner;

    private void OnEnable()
    {
        owner = GetComponentInParent<ObstacleSpawner>();
    }

    private void OnDisable()
    {
        if (owner != null)
        {
            owner.activeObstacles.Remove(this.gameObject);
        }
    }

    //private void OnDestroy()
    //{
    //    owner.activeObstacles.Remove(this.gameObject);
    //}
}
