using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectColor : MonoBehaviour
{
    public MaterialType materialType;
    [SerializeField] protected ColorData colorData;
    [SerializeField] protected MeshRenderer renderer;
    [SerializeField] protected SkinnedMeshRenderer skinnedRenderer;


    public void ChangeColor(MaterialType type)
    {
        materialType = type;
        if(renderer!=null) renderer.material = colorData.GetMat(type);
        if (skinnedRenderer!=null) skinnedRenderer.material= colorData.GetMat(type);
    }

}
