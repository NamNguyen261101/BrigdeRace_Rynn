using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ColorData", menuName = "ScriptableObjects/ColorData", order = 1)]
public class ColorData : ScriptableObject
{
    [SerializeField] Material[] colorMats;

    public Material GetColorMat(ColorTypeObject colorTypeObj)
    {
        return colorMats[(int)colorTypeObj];
    }


    public enum ColorTypeObject
    {
        Black = 0,
        Red = 1,
        Blue = 2,
        Green = 3,
        Yellow = 4,
        Orange = 5,
        Brown = 6,
        Violet = 7
    }

}
