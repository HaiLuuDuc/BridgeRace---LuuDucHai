using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MaterialType
{
    Blue = 0,
    Red = 1, 
    Green = 2, 
    Grey = 3, 
    Transparent = 4
}

[CreateAssetMenu(fileName = "ColorData", menuName = "ScriptableObjects/ColorData", order = 1)]
public class ColorData : ScriptableObject
{
    [SerializeField] private Material[] materials;

    public Material GetMat(MaterialType materialType)
    {
        return materials[(int)materialType];
    }
}
