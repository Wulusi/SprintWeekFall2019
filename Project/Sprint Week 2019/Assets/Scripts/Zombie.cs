using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Zombie : MonoBehaviour
{
    Vector2 dirToWalk;
    public int targetPoint = 0;

    public Vector2 endPoint;

    public Vector2[] pathToFollow;
    public float walkSpd;
    public Rigidbody2D rb;
    public float damageVal, forcePushAmt;

    ZombieSpawner zombieSpawnerInstance;

    public bool isAtEnd;
    bool baseDestroyed;
    Vector2 nextBase;

    private void Start()
    {
        isAtEnd = false;
        zombieSpawnerInstance = GameObject.Find("ZombieSpawner").GetComponent<ZombieSpawner>();
    }

    private void Update()
    {

        if (isAtEnd)
        {
            Vector2 nextNearestBase = Vector2.positiveInfinity;
            for (int i = 0;i<zombieSpawnerInstance.baseTargets.Length; i++)
            {
                if (zombieSpawnerInstance.baseTargets[i].activeSelf)
                {
                    if (Vector2.Distance(rb.position, zombieSpawnerInstance.baseTargets[i].transform.position) < Vector2.Distance(rb.position, nextNearestBase))
                    {
                        nextNearestBase = zombieSpawnerInstance.baseTargets[i].transform.position;
                    }
                }
            }
            dirToWalk = (Vector2) nextNearestBase - (Vector2) transform.position;
        }

        else dirToWalk = pathToFollow[targetPoint] - (Vector2)transform.position;
        rb.velocity = Vector2.Lerp(rb.velocity, dirToWalk.normalized * walkSpd, 0.1f);

        GameObject[] allBasesLeft = GameObject.FindGameObjectsWithTag("Base");

        //GameObject.FindGameObjectsWithTag("Base");
        if (allBasesLeft.Length == 0)
        {
            SceneManager.LoadScene("MainMenu");


        }

        if (Vector2.Distance(pathToFollow[targetPoint], transform.position) < 0.1f && !isAtEnd)
        {
            if (targetPoint < pathToFollow.Length - 1)
            {
                targetPoint++;

                if(targetPoint >= pathToFollow.Length - 1)
                {
                    Debug.Log("Beep");
                    isAtEnd = true;
                }
            }
        }

        //if(isAtEnd)
        //{
        //    dirToWalk = endPoint - (Vector2)transform.position;
        //    rb.velocity = Vector2.Lerp(rb.velocity, dirToWalk.normalized * walkSpd, 0.1f);


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
        //    endPoint = allBasesLeft[indexWithShortestDistance].transform.position;
        //}

        
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

    private void UpdateBaseSize()
    {

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.gameObject.layer == LayerMask.NameToLayer("Players"))
        {
            collision.collider.GetComponent<Health>().TakeDamage(damageVal, gameObject.GetComponent<Health>().elementType);
            collision.collider.GetComponent<Rigidbody2D>().AddForce((collision.transform.position - transform.position) * forcePushAmt, ForceMode2D.Impulse);
        }
        else if (collision.collider.gameObject.layer == LayerMask.NameToLayer("Bases") || collision.collider.gameObject.layer == LayerMask.NameToLayer("Obstacles"))
        {
            collision.collider.GetComponent<Health>().TakeDamage(damageVal, gameObject.GetComponent<Health>().elementType);
        }
        rb.AddForce((transform.position - collision.collider.transform.position) * forcePushAmt, ForceMode2D.Impulse);
    }
}
