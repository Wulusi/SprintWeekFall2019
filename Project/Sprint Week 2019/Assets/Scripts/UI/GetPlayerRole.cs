using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GetPlayerRole : MonoBehaviour
{
    ChargedShotTest chargedShotTest;
    PlaceBarrier placeBarrier;

    public Text playerText;
    // Start is called before the first frame update
    void Start()
    {
        playerText = GetComponent<Text>();
        StartCoroutine(waitToGetData());
    }

    // Update is called once per frame
    void Update()
    {

    }

    IEnumerator waitToGetData()
    {
        yield return new WaitForSeconds(0.25f);
        DetermineRole();
    }

    void DetermineRole()
    {
        var getAttackerComponent = GetComponentInParent<ChargedShotTest>();

        if (getAttackerComponent)
        {
            chargedShotTest = GetComponentInParent<ChargedShotTest>();
            GetID(chargedShotTest.playerIndex.ToString());

        }

        var getDefenderComponent = GetComponentInParent<PlaceBarrier>();

        if (getDefenderComponent)
        {
            placeBarrier = GetComponentInParent<PlaceBarrier>();
            GetID(placeBarrier.playerIndex.ToString());
        }
    }
    void GetID(string text)
    {
        if (text == "One")
        {
            playerText.text = "P1";
        }

        if (text == "Two")
        {
            playerText.text = "P2";
        }

        if (text == "Three")
        {
            playerText.text = "P3";
        }

        if (text == "Four")
        {
            playerText.text = "P4";
        }

    }
}
