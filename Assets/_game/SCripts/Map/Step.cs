using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Step : ObjectColor
{
    [SerializeField] private MeshRenderer meshRenderer;
    public void Start()
    {
        materialType = MaterialType.Transparent;
        ChangeColor(MaterialType.Transparent);
    }
    public IEnumerator ChangeColorStep(MaterialType materialType)
    {
        if (this.materialType != materialType)
        {
            this.materialType = materialType;
            float elapsedTime = 0f;
            float duration = 0.2f;
            while (elapsedTime < duration)
            {
                elapsedTime += Time.deltaTime;
                meshRenderer.material.color = Color.Lerp(Color.white, colorData.GetMat(materialType).color, elapsedTime / duration);
                yield return null;
            }
        }
    }
}
