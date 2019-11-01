using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectAoE : MonoBehaviour
{
    public Element thisElement;
    public Element neutralElement;

    public CircleCollider2D circleOfEffect;

    public float colliderRadius;

    public Color shotColour;

    private void Start()
    {
        GetData();
    }

    private void GetData()
    {
        circleOfEffect = GetComponent<CircleCollider2D>();
        circleOfEffect.radius = colliderRadius;
        shotColour = transform.GetChild(0).GetComponent<SpriteRenderer>().color;

        var ConvertedMarkerRadius = colliderRadius / 2.6f;

        transform.GetChild(1).GetComponent<Transform>().localScale = new Vector3(ConvertedMarkerRadius, ConvertedMarkerRadius, ConvertedMarkerRadius);

        var transparentAoe = shotColour;
        transparentAoe.a = 0.25f;
        transform.GetChild(1).GetComponent<SpriteRenderer>().color = transparentAoe;
    }

    private void OnTriggerEnter2D(Collider2D obj)
    {
        if (obj.gameObject.layer == LayerMask.NameToLayer("Players"))
        {
            if (obj.GetComponent<ChargedShotTest>())
            {
                obj.GetComponent<ChargedShotTest>().currentElement = thisElement;
                obj.GetComponent<ChargedShotTest>().elementColour = shotColour;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D obj)
    {
        if (obj.GetComponent<ChargedShotTest>())
        {
            obj.GetComponent<ChargedShotTest>().currentElement = neutralElement;
            obj.GetComponent<ChargedShotTest>().elementColour = Color.yellow;
        }
    }
}
