using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorManager : MonoBehaviour
{
    public static ColorManager instance;
    private void Awake()
    {
        instance = this;
    }
    public bool[] usedColorArray = {true, true, true, false, false, false, false };
    public List<MaterialType> characterColors = new List<MaterialType>();
    private void Start()
    {
        characterColors.Add(MaterialType.Blue);
    }
}
