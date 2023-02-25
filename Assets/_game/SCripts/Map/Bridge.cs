using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bridge : MonoBehaviour
{
    [SerializeField] private Vector3 beginSpot;
    [SerializeField] private Vector3 endSpot;
    public Vector3 bridgeDirection;
    private void Start()
    {
        beginSpot = gameObject.transform.Find("Begin").transform.position;
        endSpot = gameObject.transform.Find("End").transform.position;
        bridgeDirection = (endSpot - beginSpot).normalized;
    }
}
