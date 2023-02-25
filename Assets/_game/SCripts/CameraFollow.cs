using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] public Character container;
    public Vector3 offset;
    [SerializeField] private float speed;
    int baloCount = 0;
    private void Start()
    {
        
    }
    private void LateUpdate()
    {
        baloCount = container.baloBrickObjectList.Count;
        transform.position = Vector3.Lerp(transform.position, container.transform.position + offset + offset*baloCount/50, Time.deltaTime * speed);
    }
}