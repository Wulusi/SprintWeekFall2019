using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    float currentHealth;
    GameObject owner;

    public void TakeDamage(float dmg)
    {
        currentHealth -= dmg;
        CheckDeath();
    }

    public void CheckDeath()
    {
        if (currentHealth <= 0)
        {
            Destroy(owner);
        }
    }
}
