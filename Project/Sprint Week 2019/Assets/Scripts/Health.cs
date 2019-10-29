using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour, ObjectInterface
{
    public Element elementType;
    public float maxHealth;
    public float currentHealth;
    public GameObject owner;

    public void TakeDamage(float dmg, Element type)
    {
        if (elementType.vulnerableTo == type)
        {
            currentHealth -= dmg * 2;
        }
        else if (elementType.strongAgainst == type)
        {
            currentHealth -= dmg / 2;
        }
        else
        {
            currentHealth -= dmg;
        }
        CheckDeath();
    }

    public void CheckDeath()
    {
        if (currentHealth <= 0)
        {
            Destroy(owner);
        }
    }

    public void OnObjectSpawn()
    {
        currentHealth = maxHealth;
    }
}
