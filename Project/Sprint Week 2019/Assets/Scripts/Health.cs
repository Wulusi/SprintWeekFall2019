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
    public Vector3 startPos, startingScale;

    public AudioSource audioSource;
    public AudioClip hurt, death;

    public bool isNotTweening;

    void Start()
    {
        startPos = transform.position;
        startingScale = this.gameObject.transform.localScale;
        audioSource = GetComponent<AudioSource>();
        //audioSource.pitch = Random.Range(-1, 2);
    }

    public void TakeDamage(float dmg, Element type)
    {
        if (elementType.vulnerableTo == type)
        {
            Debug.Log("Weakness Dmg!");
            currentHealth -= dmg * 2;
            //audioSource.pitch = Random.Range(-1, 2);
            audioSource.PlayOneShot(hurt);
            WeakDamageAnimation();
            //ObjectAnimation();
        }
        else if (elementType.strongAgainst == type)
        {
            Debug.Log("Resist Dmg!");
            currentHealth -= dmg / 2;
            //audioSource.pitch = Random.Range(-1, 2);
            audioSource.PlayOneShot(hurt);
            ResistDamageAnimation();
            //ObjectAnimation();
        }
        else
        {
            Debug.Log("Normal Dmg!");
            currentHealth -= dmg;
            //audioSource.pitch = Random.Range(-1, 2);
            audioSource.PlayOneShot(hurt);
            DamageAnimation();
            //ObjectAnimation();
        }
        CheckDeath();
    }

    public void DamageAnimation()
    {
        if (!isNotTweening)
        {
            iTween.ShakeScale(this.gameObject, new Vector2(Random.Range(1, 1.5f), Random.Range(1, 1.5f)), 0.5f);
            this.gameObject.transform.localScale = startingScale;
        }
    }

    public void ObjectAnimation()
    {
        if (isNotTweening && owner.gameObject.layer != LayerMask.NameToLayer("Obstacles"))
        {
            iTween.ShakeScale(this.gameObject, new Vector2(1.2f, 1.2f), 0.5f);
            this.gameObject.transform.localScale = startingScale;
        }
    }

    public void WeakDamageAnimation()
    {
        if (!isNotTweening)
        {
            iTween.ShakePosition(this.gameObject, new Vector2(Random.Range(0, 0.5f), Random.Range(0, 0.5f)), 0.5f);
            this.gameObject.transform.localScale = startingScale;
        }
    }

    public void ResistDamageAnimation()
    {
        if (!isNotTweening)
        {
            iTween.PunchRotation(this.gameObject, new Vector3(0,0,360), 0.5f);
            this.gameObject.transform.localScale = startingScale;
        }
    }

    public void DeathAnimation()
    {
        if (!isNotTweening && owner.gameObject.layer != LayerMask.NameToLayer("Players"))
        {
            Debug.Log("Dying");
            //iTween.RotateBy(this.gameObject, new Vector3(0, 0, 10), 2.5f);
            iTween.FadeTo(this.gameObject, 0, 2.5f);
            iTween.ShakeScale(this.gameObject, new Vector2(Random.Range(1, 1.5f), Random.Range(1, 1.5f)), 0.5f);
            this.gameObject.transform.localScale = startingScale;
        }
    }


    public void CheckDeath()
    {
        if (currentHealth <= 0)
        {
            //Destroy(owner);
            //replace this line for players
            //audioSource.pitch = Random.Range(-1, 2);
            audioSource.PlayOneShot(death);
            DeathAnimation();
            StartCoroutine(Death());
        }
    }

    IEnumerator Death()
    {
        yield return new WaitForSeconds(2.5f);
        if (owner.gameObject.layer != LayerMask.NameToLayer("Players")) owner.gameObject.SetActive(false);
        else StartCoroutine(Respawn());
    }

    IEnumerator Respawn()
    {
        transform.position = startPos;

        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).gameObject.SetActive(false);
        }

        if (GetComponent<SpriteRenderer>())
        {
            GetComponent<SpriteRenderer>().enabled = false;
        }
        //GetComponent<PlayerMotor>().enabled = false;
        GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
        GetComponent<BoxCollider2D>().enabled = false;
        if (GetComponent<ChargedShotTest>() != null) GetComponent<ChargedShotTest>().enabled = false;
        GetComponent<Rigidbody2D>().velocity = Vector3.zero;
        GetComponent<Rigidbody2D>().Sleep();


        float timer = 0;

        while (timer < respawnTimer)
        {
            timer += Time.deltaTime;
            yield return null;
        }


        if (GetComponent<SpriteRenderer>())
        {
            GetComponent<SpriteRenderer>().enabled = false;
        }

        GetComponent<BoxCollider2D>().enabled = true;
        if (GetComponent<ChargedShotTest>() != null) GetComponent<ChargedShotTest>().enabled = true;
        GetComponent<Rigidbody2D>().WakeUp();

        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).gameObject.SetActive(true);
        }
        GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.None;
        OnObjectSpawn();
    }

    public void OnDisable()
    {
        if (this.gameObject.layer == LayerMask.NameToLayer("Players"))
        {
            this.gameObject.transform.localScale = startingScale;
        }
    }
    public void OnObjectSpawn()
    {
        currentHealth = maxHealth;
        if (GetComponent<Rigidbody2D>())
        {
            GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;
        }
    }
}
