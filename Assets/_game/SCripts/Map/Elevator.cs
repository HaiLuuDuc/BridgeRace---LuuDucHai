using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elevator : MonoBehaviour
{
    [SerializeField] private GameObject door1;
    [SerializeField] private GameObject door2;
    [SerializeField] private float speed;
    private bool isOccupied = false;
    private Vector3 firstPosition;
    private Vector3 lastPosition;
    public bool isMoving = false;
    public Transform beginSpot;
    IEnumerator MoveUp()
    {
        isMoving = true;
        while(transform.position.y<lastPosition.y)
        {
            transform.position = Vector3.MoveTowards(transform.position,lastPosition,speed * Time.deltaTime);
            yield return null;
        }
        OpenDoor(door2);
        isMoving = false;
    }
    IEnumerator MoveDown()
    {
        isMoving = true;
        while (transform.position.y > firstPosition.y)
        {
            transform.position = Vector3.MoveTowards(transform.position, firstPosition,speed *  Time.deltaTime);
            yield return null;
        }
        OpenDoor(door1);
        isMoving = false;
    }
    private void Start()
    {
        firstPosition = transform.position;
        lastPosition = firstPosition + Vector3.up * 18.8f;
        speed = 6f;
        OpenDoor(door1);
        CloseDoor(door2);
    }
    public void CloseDoor(GameObject door)
    {
        door.SetActive(true);
    }
    public void OpenDoor(GameObject door)
    {
        door.SetActive(false);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag(CachedString.CHARACTER))
        {
            Character character = other.GetComponent<Character>();
            character.maxPosY = 1000;
            character.onElevator = true;
            other.transform.SetParent(this.transform);
            isOccupied = true;
            if (!isMoving)
            {
                CloseDoor(door1);
                StopCoroutine(MoveDown());
                StartCoroutine(MoveUp());


            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag(CachedString.CHARACTER))
        {
            Character character = other.GetComponent<Character>();
            character.onElevator = false;
            other.transform.SetParent(null);
            isOccupied = false;
            if (!isMoving)
            {
                CloseDoor(door2);
                StopCoroutine(MoveUp());
                StartCoroutine(MoveDown());
            }
        }
    }
}
