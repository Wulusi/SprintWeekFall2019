using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBehaviour : MonoBehaviour, ObjectInterface
{
    public float timer, startTime, shrinkSpeed;

    public bool startShrink;

    [HideInInspector]
    public Rigidbody2D _rb;
    public void OnObjectSpawn()
    {
        Debug.Log("Bullet Recycled");
        this.transform.localScale = Vector3.zero;
        timer = startTime;
        StartCoroutine(CountDown());
        startShrink = false;
    }

    // Start is called before the first frame update
    void Start()
    {
        timer = startTime;

    }

    void CheckShotSize()
    {
        if (transform.localScale.x <= 0.2f)
        {
            this.gameObject.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        ShrinkShot();
        CheckShotSize();
    }

    private IEnumerator CountDown()
    {
        float duration = timer;
        float time = 0;

        while (time < duration)
        {
            time += Time.deltaTime;
            yield return null;
        }
        startShrink = true;
    }

    private void ShrinkShot()
    {
        if (startShrink)
        {
            this.transform.localScale -= Vector3.one * Time.deltaTime * shrinkSpeed;
        }
    }

    private void OnDisable()
    {
        _rb = GetComponent<Rigidbody2D>();
        _rb.velocity = Vector2.zero;
    }
}
