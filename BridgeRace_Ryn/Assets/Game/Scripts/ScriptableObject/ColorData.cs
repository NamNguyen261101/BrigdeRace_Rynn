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
        Default = 0,
        Black = 1,
        Red = 2,
        Blue = 3,
        Green = 4,
        Yellow = 5,
        Orange = 6,
        Brown = 7,
        Violet = 8
    }

}
