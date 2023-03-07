using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Step : ObjectColor
{
    [SerializeField] private MeshRenderer meshRenderer;
    [SerializeField] Material whiteMaterial;
    public void Start()
    {
        ChangeColor(MaterialType.Transparent);
    }
    public IEnumerator ChangeColorStep(MaterialType materialType)
    {
        if (this.materialType != materialType)
        {
            ChangeColor(materialType);
            yield return null;
            float elapsedTime = 0f;
            float duration = 0.4f;
            while (elapsedTime < duration)
            {
                elapsedTime += Time.deltaTime;
                meshRenderer.material.Lerp(whiteMaterial, colorData.GetMat(materialType), elapsedTime / duration);
                yield return null;
            }
        }
    }
}
