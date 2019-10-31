using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetColourOfParent : MonoBehaviour
{
    public GameObject parentColour;

    // Start is called before the first frame update
    void Start()
    {
        //StartCoroutine(waitToGetColour());
    }

    private void OnEnable()
    {
    }

    // Update is called once per frame
    void Update()
    {
        this.gameObject.GetComponent<SpriteRenderer>().color = parentColour.GetComponent<SpriteRenderer>().color;
    }

    private IEnumerator waitToGetColour()
    {
        yield return new WaitForSeconds(0.1f);
        {

        }
    }
}
