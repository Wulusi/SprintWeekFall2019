using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using GamepadInput;

public class MainMenuScript : MonoBehaviour
{
    Slider slider;
    public GamePad.Index playerIndex;
    bool buttonPressA;
    Vector2 dpadPress;
    Vector2 leftStick;
    Text playText;
    Text quitText;
    Color highlightColour, darkColour;

    private void Start()
    {
        slider = GetComponent<Slider>();
        //Text[] textObjects = transform.GetComponentsInChildren<Text>();
        //foreach (Text text in textObjects)
        //{
        //    if (text.name == "Play") playText = text;
        //    if (text.name == "Quit") quitText = text;
        //}
        //highlightColour = playText.color;
        //darkColour = quitText.color;
        StartCoroutine(MenuAction());
    }
    private void Update()
    {
        CheckInput();
    }

    void CheckInput()
    {
        buttonPressA = GamePad.GetButtonDown(GamePad.Button.A, GamePad.Index.One);
        dpadPress = GamePad.GetAxis(GamePad.Axis.Dpad, GamePad.Index.One);
        leftStick = GamePad.GetAxis(GamePad.Axis.LeftStick, GamePad.Index.One);
    }

    IEnumerator MenuAction()
    {
        while (true)
        {
            if (buttonPressA)
            {
                if (slider.value == 0) SceneManager.LoadScene("MainScene");
                else if (slider.value == 1) Application.Quit();
            }

            if (dpadPress.y < 0 || leftStick.y < 0)
            {
                if (slider.value == 0) slider.value = 1;
                else
                {
                    slider.value++;
                    //playText.color = darkColour;
                    //quitText.color = highlightColour;
                }
                while (dpadPress.y > 0) yield return null;             
            }

            if (dpadPress.y > 0 || leftStick.y > 0)
            {
                if (slider.value == 1) slider.value = 0;
                else
                {
                    slider.value--;
                    //playText.color = highlightColour;
                    //quitText.color = darkColour;
                }
                while (dpadPress.y < 0) yield return null;
            }
            yield return null;
        }
    }
}
