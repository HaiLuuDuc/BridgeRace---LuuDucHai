using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorHight : MonoBehaviour
{
    [SerializeField] private GameObject doorLeft;
    [SerializeField] private GameObject doorRight;
    Vector3 leftMaxPosition;
    Vector3 rightMaxPosition;
    private float speed = 5f;
    private void Start()
    {
        leftMaxPosition = doorLeft.transform.position - Vector3.right*2;
        rightMaxPosition = doorRight.transform.position + Vector3.right*2;
    }
    public IEnumerator Open()
    {
        while (Vector3.Distance(leftMaxPosition, doorLeft.transform.position) > 0.1f)
        {
            doorLeft.transform.position = Vector3.MoveTowards(doorLeft.transform.position, leftMaxPosition, speed*Time.deltaTime);
            doorRight.transform.position = Vector3.MoveTowards(doorRight.transform.position, rightMaxPosition, speed * Time.deltaTime);

            yield return null;
        }
    }

}
