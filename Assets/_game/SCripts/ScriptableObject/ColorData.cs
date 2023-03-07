using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MaterialType
{
    Transparent = 0,
    Grey = 1, 
    Blue = 2,
    Red = 3, 
    Green = 4, 
    Yellow = 5,
    Pink = 6,
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
