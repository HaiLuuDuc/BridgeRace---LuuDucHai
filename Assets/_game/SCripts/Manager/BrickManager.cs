using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine;

public class BrickManager : MonoBehaviour
{
    public List<GameObject> groundBrickObjectList = new List<GameObject>(); 
    [SerializeField] private GameObject brickTile;
    [SerializeField] private ColorManager colorManager;


    public void Start()
    { 
        int randomIndex;
        //initial list of ground bricks
        for (int i = 0; i < brickTile.transform.childCount; i++)
        {
            GameObject brick = brickTile.transform.GetChild(i).gameObject;
            randomIndex = Random.Range(0, 3);
            brick.GetComponent<Brick>().ChangeColor((MaterialType)randomIndex);

            // tat physics cua brick
            brick.GetComponent<Brick>().TurnOffPhysics();


            groundBrickObjectList.Add(brick);
            brick.GetComponent<Brick>().HideRenderer(); // giau cac brick cho den khi character di vao stage

        }
        
    }
}
