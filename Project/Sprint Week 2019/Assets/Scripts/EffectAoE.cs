using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectAoE : MonoBehaviour
{
    public Element thisElement;
    public Element neutralElement;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.gameObject.layer == LayerMask.NameToLayer("Players"))
        {
            if (collision.collider.GetComponent<ChargedShotTest>())
            {
                collision.collider.GetComponent<ChargedShotTest>().currentElement = thisElement;
            }
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.collider.GetComponent<ChargedShotTest>())
        {
            collision.collider.GetComponent<ChargedShotTest>().currentElement = neutralElement;
        }
    }
}
