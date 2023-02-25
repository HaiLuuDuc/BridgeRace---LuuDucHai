using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UIElements;

public class Bot : Character
{
    

    float minDistance;
    IState currentState;
    int randomIndex;
    public int maxBrickCount;
    GameObject bridges;
    public bool isPassBridge = false;
    GameObject usedBridge;
    [SerializeField] public NavMeshAgent navMeshAgent;
    [SerializeField] private CameraFollow cameraFollow;
    public Vector3 endSpot;

    public IEnumerator WaitAndPatrol()
    {
        yield return new WaitForSeconds(2f);
        ChangeState(new PatrolState());
        yield return null;
    }
    public void Start()
    {
        // random color
        randomIndex = Random.Range(1, 3);
        Debug.Log("Bot OnStart !!!");
        speed = 15f;
        while (ColorManager.instance.usedColorArray[randomIndex] == true) // tranh bots bi trung mau
        {
            randomIndex = Random.Range(1, 3);
        }
        ChangeColor((MaterialType)randomIndex);
        ColorManager.instance.usedColorArray[randomIndex] = true;
        maxBrickCount = Random.Range(3, 6);
        ChangeState(new PatrolState());
    }

    protected override void Update()
    {
        base.Update();
        if (isFalling)
        {
            currentState = null;
            StartCoroutine(WaitAndPatrol());
        }
        if (currentState != null) currentState.OnExecute(this);
        Debug.Log(currentState);
    }


    public void ChangeState(IState newState)
    {
        if (currentState != null)
        {
            currentState.OnExit(this);
        }
        currentState = newState;
        if (currentState != null)
        {
            currentState.OnEnter(this);
        }
    }
    protected override void OnNewStage(GameObject stage)
    {
        base.OnNewStage(stage);
        maxPosY = transform.position.y;

        if (stage.transform.Find(CachedString.BRIDGES).gameObject != bridges) {
            bridges = stage.transform.Find(CachedString.BRIDGES).gameObject;
            usedBridge = stage.transform.Find(CachedString.USED_BRIDGE).gameObject;
            int randomIndex = Random.Range(0, bridges.transform.childCount);
            if (bridges.transform.childCount > 0)
            {
                bridge = bridges.transform.GetChild(randomIndex).gameObject;  // xac dinh bridge de treo len 
            }
            else
            {
                bridge = usedBridge.transform.GetChild(0).gameObject;  // truong hop so luong bot nhieu hon so luong bridge thi dung chung bridge
            }
            bridge.transform.SetParent(usedBridge.transform);
            direction = bridge.GetComponent<Bridge>().bridgeDirection + Vector3.up/10;
            isPassBridge = false;
            endSpot = bridge.transform.Find(CachedString.END).transform.position;

        }
        maxBrickCount = Random.Range(3, 6);
        ChangeState(new PatrolState());
    }
    public void MoveToNearestBrick()
    {
        minDistance = 100f;
        Vector3 brickPosition = transform.position;
        for (int i = 0; i < brickManager.groundBrickObjectList.Count; i++)
        {
            GameObject brick = brickManager.groundBrickObjectList[i];
            if(brick != null)
            if ((brick.gameObject.GetComponent<Brick>().materialType == materialType
                || brick.gameObject.GetComponent<Brick>().materialType == MaterialType.Grey)
                && brick.transform.parent.gameObject.name != CachedString.BALO)
            {
                if (Vector3.Distance(transform.position, brick.transform.position) < minDistance)
                {
                    minDistance = Vector3.Distance(transform.position, brick.transform.position);
                    brickPosition = brick.transform.position;
                    brickPosition.y = transform.position.y;
                }
            }
        }
        transform.position = Vector3.MoveTowards(transform.position, brickPosition, speed / 4 * Time.deltaTime);
        transform.rotation = Quaternion.LookRotation(brickPosition - transform.position);
    }
    // iswin islose in gamemanager?
    protected override void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag(CachedString.WIN_ZONE))
        {
            ChangeAnim(CachedString.WIN);
            GameManger.Instance.isLose = true;
            transform.rotation = Quaternion.Euler(new Vector3(0, 180, 0));
            cameraFollow.container = this.GetComponent<Character>();
        }
    }

}
