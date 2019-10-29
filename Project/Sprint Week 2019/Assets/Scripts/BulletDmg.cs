using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletDmg : MonoBehaviour
{
    public float dmgVal;
    List<string> tagsToSearch;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.gameObject.GetComponent<Health>())
        {
            collision.collider.gameObject.GetComponent<Health>().TakeDamage(dmgVal);
        }
    }
}
