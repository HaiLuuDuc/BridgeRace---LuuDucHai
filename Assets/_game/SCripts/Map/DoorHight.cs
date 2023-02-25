using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorHight : MonoBehaviour
{
    GameObject doorLeft;
    GameObject doorRight;
    Vector3 leftMaxPosition;
    Vector3 rightMaxPosition;

    private void Start()
    {
        doorRight = transform.GetChild(5).gameObject;
        doorLeft = transform.GetChild(4).gameObject;
        leftMaxPosition = doorLeft.transform.position - Vector3.right*2;
        rightMaxPosition = doorRight.transform.position + Vector3.right*2;
    }
    public IEnumerator Open()
    {
        while (Vector3.Distance(leftMaxPosition, doorLeft.transform.position) > 0.1f)
        {
            doorLeft.transform.position = Vector3.MoveTowards(doorLeft.transform.position, leftMaxPosition, 2*Time.deltaTime);
            doorRight.transform.position = Vector3.MoveTowards(doorRight.transform.position, rightMaxPosition, 2 * Time.deltaTime);

            yield return null;
        }
    }

}
