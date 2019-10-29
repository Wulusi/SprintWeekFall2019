using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Element", menuName = "ScriptableObjects/Element", order = 0)]
public class Element: ScriptableObject
{
    public Element vulnerableTo;
    public Element strongAgainst;
}