using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private float speed;
    int baloCount = 0;
    public Character container;
    public Vector3 offset;
    public float space;

    private void LateUpdate()
    {
        // thang hoac thua thi` zoom in character
        if (GameManger.Instance.isWin || GameManger.Instance.isLose)
        {
            if (Vector3.Distance(transform.position, container.transform.position) > space)
            {
                transform.position = Vector3.Lerp(transform.position, container.transform.position + offset, Time.deltaTime * speed / 2);
            }
        }
        // neu khong thi di chuyen theo character
        else
        {
            baloCount = container.baloBrickObjectList.Count;
            transform.position = Vector3.Lerp(transform.position, container.transform.position + offset + offset * baloCount / 50, Time.deltaTime * speed);
        }
    }
}