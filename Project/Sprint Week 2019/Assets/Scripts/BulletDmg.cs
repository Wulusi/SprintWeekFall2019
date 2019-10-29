using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletDmg : MonoBehaviour
{
    public Element elementType;
    public float dmgVal;
    List<string> tagsToSearch;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<Health>())
        {
            Debug.Log("Damaging enemy");
            collision.gameObject.GetComponent<Health>().TakeDamage(dmgVal, elementType);
        }
    }
}
