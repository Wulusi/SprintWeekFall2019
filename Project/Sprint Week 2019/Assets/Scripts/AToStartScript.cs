using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using GamepadInput;

public class AToStartScript : MonoBehaviour
{
    public GamePad.Index playerIndex;
    bool buttonPressA;

    // Update is called once per frame
    void Update()
    {
        buttonPressA = GamePad.GetButtonDown(GamePad.Button.A, GamePad.Index.One);
        if (buttonPressA || Input.GetKeyDown(KeyCode.A)) SceneManager.LoadScene("MainScene");
    }
}
