using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour, ObjectInterface
{
    public Element elementType;
    public float maxHealth;
    public float currentHealth;
    public GameObject owner;
    public float respawnTimer = 10f;
    Vector3 startPos;

    void Start()
    {
        startPos = transform.position;
    }

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
            //Destroy(owner);
            //replace this line for players
            if (owner.gameObject.layer != LayerMask.NameToLayer("Players")) owner.gameObject.SetActive(false);
            else StartCoroutine(Respawn());
        }
    }

    IEnumerator Respawn()
    {
        transform.position = startPos;

        GetComponent<SpriteRenderer>().enabled = false;
        //GetComponent<PlayerMotor>().enabled = false;
        GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
        GetComponent<BoxCollider2D>().enabled = false;
        GetComponent<ChargedShotTest>().enabled = false;
        GetComponent<Rigidbody2D>().velocity = Vector3.zero;
        GetComponent<Rigidbody2D>().Sleep();
        for (int i = 0; i<transform.childCount; i++)
        {
            transform.GetChild(i).gameObject.SetActive(false);
        }

        float timer = 0;

        while (timer < respawnTimer)
        {
            timer += Time.deltaTime;
            yield return null;
        }

        GetComponent<SpriteRenderer>().enabled = true;
        GetComponent<BoxCollider2D>().enabled = true;
        GetComponent<ChargedShotTest>().enabled = true;
        GetComponent<Rigidbody2D>().WakeUp();

        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).gameObject.SetActive(true);
        }
        GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.None;
        OnObjectSpawn();
    }

    public void OnObjectSpawn()
    {
        currentHealth = maxHealth;
    }
}
