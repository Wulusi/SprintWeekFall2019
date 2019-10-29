using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zombie : MonoBehaviour
{
    Vector2 dirToWalk;
    public GameObject targetPoint;
    public float walkSpd;
    public Rigidbody2D rb;
    public float damageVal;

    private void Update()
    {
        dirToWalk = targetPoint.transform.position - transform.position;
        rb.velocity = Vector2.Lerp(rb.velocity,dirToWalk.normalized * walkSpd,0.1f);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.gameObject.layer == LayerMask.NameToLayer("Players"))
        {

        }
        else if (collision.collider.gameObject.layer == LayerMask.NameToLayer("Bases")|| collision.collider.gameObject.layer == LayerMask.NameToLayer("Obstacles"))
        {
            collision.collider.GetComponent<Health>().TakeDamage(damageVal,gameObject.GetComponent<Health>().elementType);
        }
        rb.AddForce((transform.position - collision.collider.transform.position), ForceMode2D.Impulse);
    }
}
