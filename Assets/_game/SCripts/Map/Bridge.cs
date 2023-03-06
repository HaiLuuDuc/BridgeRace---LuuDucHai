using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.VirtualTexturing;

public class Bridge : MonoBehaviour
{
    public Transform beginSpot;
    public Transform endSpot;
    public Vector3 bridgeDirection;
    private void Start()
    {
        bridgeDirection = (endSpot.position- beginSpot.position).normalized;
    }
}
