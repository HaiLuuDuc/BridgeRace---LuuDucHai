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
    public Material[] materialArray = new Material[5]; // blue, red, green, grey, transparent
    public bool[] usedColorArray = {true, false, false, false, true };

}
