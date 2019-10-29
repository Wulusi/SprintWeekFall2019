using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIAlwaysUp : MonoBehaviour
{
    // Update is called once per frame
    void LateUpdate()
    {
        AlwaysUp();
    }

    void AlwaysUp()
    {
        this.transform.up = Vector2.up;
    }
}
