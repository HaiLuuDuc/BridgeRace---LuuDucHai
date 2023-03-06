using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Brick : ObjectColor
{
    public Vector3 firstPosition;
    public bool isGround = true;
    [SerializeField] Rigidbody rb;
    [SerializeField] BoxCollider boxCollider;
    private void Start()
    {
        firstPosition= transform.position;
    }
    public IEnumerator MoveToBalo(Vector3 target)
    {
        float duration = 0.3f;
        float elapsedTime = 0f;
        while(elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            transform.localPosition = Vector3.Lerp(transform.localPosition, target, (float)(elapsedTime / duration));
            transform.localRotation = Quaternion.Euler(Vector3.zero);
            yield return null;
        }

    }
    public void HideRenderer()
    {
        if (renderer != null)
        {
            renderer.enabled = false;
        }
    }
    public void ShowRenderer()
    {
        if (renderer != null)
        {
            renderer.enabled = true;
        }
    }
    public void MoveToFirstPosition()
    {
        transform.position = firstPosition;
    }
    public void TurnOffPhysics()
    {
        boxCollider.isTrigger = true;
        rb.isKinematic = true;
    }
    public void TurnOnPhysics()
    {
        rb.isKinematic = false;
        boxCollider.isTrigger = false;
    }
}
