using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zombie : MonoBehaviour
{
    Vector2 dirToWalk;
    public GameObject targetPoint;
    public float walkSpd;
    public Rigidbody2D rb;

    private void Update()
    {
        dirToWalk = targetPoint.transform.position - transform.position;
        rb.velocity = dirToWalk.normalized * walkSpd;
    }
}
