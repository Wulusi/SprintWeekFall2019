using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBehaviour : MonoBehaviour, ObjectInterface
{
    public float timer, startTime;
    public void OnObjectSpawn()
    {
        Debug.Log("Bullet Recycled");
        this.transform.localScale = Vector3.zero;
        timer = startTime;
        StartCoroutine(CountDown());
    }

    // Start is called before the first frame update
    void Start()
    {
        timer = startTime;
      
    }

    // Update is called once per frame
    void Update()
    {

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
        this.gameObject.SetActive(false);
    }
}
