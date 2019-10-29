using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    public ObstacleSpawner owner;

    private void OnDestroy()
    {
        owner.activeObstacles.Remove(this.gameObject);
    }
}
