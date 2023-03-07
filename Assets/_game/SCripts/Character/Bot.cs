using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UIElements;
using UnityEngine.WSA;

public class Bot : Character
{
    [SerializeField] private CameraFollow cameraFollow;
    [SerializeField] private List<Transform> endSpotList = new List<Transform>();
    [SerializeField] private List<Stage> stageList = new List<Stage>();
    private float minDistance;
    private int randomIndex;
    private Vector3 brickPosition;
    public NavMeshAgent navMeshAgent;
    public Elevator elevator;
    public IState currentState;
    public int maxBrickCount;
    public Transform endSpot;
    public Transform beginSpot;


    protected override IEnumerator WaitAndStandUp()
    {
        DeactiveNavMeshAgent();
        currentState = null;
        StartCoroutine(base.WaitAndStandUp());
        ChangeAnim(CachedString.RUN);
        ActiveNavMeshAgent();
        ChangeState(new MoveToBrickState());
        yield return null;
    }
    public void Start()
    {
        // random color
        randomIndex = Random.Range(3,7);
        speed = 15f;
        while (ColorManager.instance.usedColorArray[randomIndex] == true) // tranh bots bi trung mau
        {
            randomIndex = Random.Range(3,7);
        }
        ColorManager.instance.usedColorArray[randomIndex] = true;
        ChangeColor((MaterialType)randomIndex);
        ColorManager.instance.characterColors.Add(this.materialType);
        maxBrickCount = Random.Range(3, 6);
    }

    protected override void Update()
    {
        base.Update();
        if (GameManger.Instance.isLose)
        {
            ChangeState(new WinState());
            return;
        }
        if (GameManger.Instance.isWin)
        {
            ChangeState(new IdleState());
            return;
        }

        if (currentState != null) currentState.OnExecute(this);

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
    protected override void OnNewStage(Stage stage)
    {
        base.OnNewStage(stage);

        ChooseEndSpot();
        maxBrickCount = Random.Range(3, 6);

        ChangeState(new MoveToBrickState());
    }

    private void ChooseEndSpot()
    {
        endSpot = endSpotList[stageList.IndexOf(stage)];
    }
    public void ActiveNavMeshAgent()
    {
        navMeshAgent.isStopped = false;
        navMeshAgent.enabled = true;
    }

    public void DeactiveNavMeshAgent()
    {
        navMeshAgent.SetDestination(transform.position);
        navMeshAgent.velocity = Vector3.zero;
        navMeshAgent.isStopped = true;
        navMeshAgent.enabled = false;
    }
    public void MoveToNearestBrick()
    {
        //tim vien gach gan nhat, sau do di chuyen den no
        minDistance = 100f;
        bool find = false;
        for (int i = 0; i < brickManager.groundBrickObjectList.Count; i++)
        {
            Brick brick = brickManager.groundBrickObjectList[i];
            if(brick != null)
            if ((brick.materialType == materialType
                || brick.materialType == MaterialType.Grey)
                && brick.isGround)
            {
                if (Vector3.Distance(transform.position, brick.transform.position) < minDistance)
                {
                    minDistance = Vector3.Distance(transform.position, brick.transform.position);
                    brickPosition = brick.transform.position;
                    brickPosition.y = transform.position.y;
                    find = true;
                }
            }
        }
        if (!isFalling)
        {
            if (find)
            {
                navMeshAgent.SetDestination(brickPosition);
            }
            else
            {
                ChangeState(new IdleState());
            }
        }
            
        transform.rotation = Quaternion.LookRotation(brickPosition - transform.position);
        
    }

    protected override void OnTriggerEnter(Collider other)
    {
        base.OnTriggerEnter(other);
        if (other.gameObject.CompareTag(CachedString.WIN_ZONE))
        {
            ChangeState(new WinState());
            GameManger.Instance.isLose = true;
            transform.rotation = Quaternion.Euler(new Vector3(0, 180, 0));
            cameraFollow.container = this;
        }
        if (other.gameObject.CompareTag(CachedString.STEP))
        {

            if (other.gameObject.GetComponent<Step>().materialType != this.materialType)
            {
                if (baloBrickObjectList.Count > 0)
                {
                    StartCoroutine(other.gameObject.GetComponent<Step>().ChangeColorStep(materialType));
                    DropBrick();
                }
            }
        }
        if(other.gameObject.CompareTag(CachedString.ELEVATOR_TRIGGER))
        {
            elevator =  other.gameObject.GetComponent<Elevator>();
        }

    }
    protected override void OnTriggerExit(Collider other)
    {
        base.OnTriggerExit(other);
        if (other.gameObject.CompareTag(CachedString.ELEVATOR_TRIGGER))
        {
            elevator = null;
        }
    }

}
