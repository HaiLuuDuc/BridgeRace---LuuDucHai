using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    private Queue<GameObject> availableObjects = new Queue<GameObject>();
    private List<GameObject> inUseObjects = new List<GameObject>();
    public GameObject prefab;
    public int initialPoolSize = 100;

/*    private static ObjectPool instance;

    public static ObjectPool Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<ObjectPool>();
            }
            return instance;
        }
    }*/

    private void Start()
    {
        for (int i = 0; i < initialPoolSize; i++)
        {
            GameObject obj = Instantiate(prefab);
            obj.SetActive(false);
            availableObjects.Enqueue(obj);
        }
    }

    public GameObject GetObject()
    {
        GameObject obj;

        if (availableObjects.Count == 0)
        {
            obj = Instantiate(prefab);
        }
        else
        {
            obj = availableObjects.Dequeue();
            obj.SetActive(true);
        }

        inUseObjects.Add(obj);
        return obj;
    }

    public void ReturnObject(GameObject obj)
    {
        inUseObjects.Remove(obj);
        obj.SetActive(false);
        availableObjects.Enqueue(obj);
    }
}