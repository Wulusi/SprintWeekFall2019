using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GamepadInput;

public class ControllerTest : MonoBehaviour
{
    public string m_playerID;


    public enum playerID
    {
        One,
        Two,
        Three,
        Four,
    }

    public playerID m_playerIDEnum;

    // Start is called before the first frame update
    void Start()
    {
        GetID();
    }

    void GetID()
    {
        m_playerID = m_playerIDEnum.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        GetInput();
    }

    void GetInput()
    {
        if (m_playerID == "One")
        {
            Debug.Log("Hello" + GamePad.GetAxis(GamePad.Axis.LeftStick, GamePad.Index.One));
        }

        if (m_playerID == "Two")
        {
            Debug.Log("Hello" + GamePad.GetAxis(GamePad.Axis.LeftStick, GamePad.Index.Two));
        }

        if (m_playerID == "Three")
        {
            Debug.Log("Hello" + GamePad.GetAxis(GamePad.Axis.LeftStick, GamePad.Index.Three));
        }

        if (m_playerID == "Four")
        {
            Debug.Log("Hello" + GamePad.GetAxis(GamePad.Axis.LeftStick, GamePad.Index.Four));
        }
    }
}
