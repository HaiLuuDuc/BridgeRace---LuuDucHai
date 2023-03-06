using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NavMeshControler : MonoBehaviour
{
    public NavMeshSurface navMeshSurface;

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space)) {
            navMeshSurface.BuildNavMesh();
        }
    }
}
