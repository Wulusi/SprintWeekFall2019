using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GetPlayerRole : MonoBehaviour
{
    ChargedShotTest chargedShotTest;
    PlaceBarrier placeBarrier;

    public Text text;
    // Start is called before the first frame update
    void Start()
    {
        text = GetComponent<Text>();
        StartCoroutine(waitToGetData());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator waitToGetData()
    {
        yield return new WaitForSeconds(0.5f);
        DetermineRole();
    }

    void DetermineRole()
    {
        var getAttackerComponent = GetComponentInParent<ChargedShotTest>();

        if (getAttackerComponent)
        {
            chargedShotTest = GetComponentInParent<ChargedShotTest>();
            text.text = "Atk Player " + chargedShotTest.playerIndex.ToString();
        }

        var getDefenderComponent = GetComponentInParent<PlaceBarrier>();

        if (getDefenderComponent)
        {
            placeBarrier = GetComponentInParent<PlaceBarrier>();
            text.text = "Def Player " + placeBarrier.playerIndex.ToString();
        }
    }
}
