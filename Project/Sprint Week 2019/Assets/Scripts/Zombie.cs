using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zombie : MonoBehaviour
{
    Vector2 dirToWalk;
    public int targetPoint = 0;

    public Vector2[] pathToFollow;
    public float walkSpd;
    public Rigidbody2D rb;
    public float damageVal, forcePushAmt;

    private void Update()
    {

        dirToWalk = pathToFollow[targetPoint] - (Vector2)transform.position;
        rb.velocity = Vector2.Lerp(rb.velocity, dirToWalk.normalized * walkSpd, 0.1f);

        if (Vector2.Distance(pathToFollow[targetPoint], transform.position) < 0.1f)
        {
            if (targetPoint < pathToFollow.Length - 1)
            {
                targetPoint++;
            }
        }

        //GameObject[] allBasesLeft = GameObject.FindGameObjectsWithTag("Base");
        //if (allBasesLeft.Length == 0)
        //{
        //    //players have lost
        //} else
        //{
        //    int indexWithShortestDistance = 0;
        //    float shortestDistance = Mathf.Infinity;
        //    for (int i = 0; i < allBasesLeft.Length; i++)
        //    {
        //        if (Vector2.Distance(transform.position, allBasesLeft[i].transform.position) < shortestDistance)
        //        {
        //            indexWithShortestDistance = i;
        //            shortestDistance = Vector2.Distance(transform.position, allBasesLeft[i].transform.position);
        //        }
        //    }
        //    targetPoint = allBasesLeft[indexWithShortestDistance].transform.position;
        //}

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.gameObject.layer == LayerMask.NameToLayer("Players"))
        {

        }
        else if (collision.collider.gameObject.layer == LayerMask.NameToLayer("Bases") || collision.collider.gameObject.layer == LayerMask.NameToLayer("Obstacles"))
        {
            collision.collider.GetComponent<Health>().TakeDamage(damageVal, gameObject.GetComponent<Health>().elementType);
        }
        rb.AddForce((transform.position - collision.collider.transform.position) * forcePushAmt, ForceMode2D.Impulse);
    }
}
