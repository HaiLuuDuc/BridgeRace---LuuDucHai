using System;
using System.Collections;
using System.Collections.Generic;
using TMPro.EditorUtilities;
using Unity.VisualScripting;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.UIElements;

public class Character : ObjectColor
{

    //[SerializeField] private Stage[] stages;
    [SerializeField] protected BrickManager brickManager;
    [SerializeField] private CharacterMagnet characterMagnet;
    [SerializeField] private GameObject balo;
    [SerializeField] protected Bridge bridge;
    private float brickHeight = 0.5f;
    protected Vector3 bridgeDirection;
    protected string currentAnimName = CachedString.IDLE;
    public Rigidbody rb;
    public Animator anim;
    public Vector3 direction;
    public List<Brick> baloBrickObjectList = new List<Brick>();
    public Vector3 targetPosition = Vector3.zero;
    public float speed;
    public bool isFalling = false;
    public float maxPosY = 20f;
    public bool onBridge = false;
    public bool onElevator = false;
    public Stage stage;


    protected virtual IEnumerator WaitAndStandUp()
    {
        yield return new WaitForSeconds(2f);
        isFalling = false;
        characterMagnet.gameObject.SetActive(true); // sau 2 giay thi dung day va co the tiep tuc an brick
        ChangeAnim(CachedString.IDLE);
        yield return null;
    }
    protected virtual void Update()
    {
        if (GameManger.Instance.isWin)
        {
            ChangeAnim(CachedString.WIN);
            return;
        }
        if (isFalling)
        {
            ChangeAnim(CachedString.FALL);
            return;
        }
        if (transform.position.y > maxPosY)
        {
            transform.position = new Vector3(transform.position.x,maxPosY,transform.position.z);
        }
    }

    public void ChangeAnim(string animName)
    {
        if (currentAnimName != animName)
        {
            anim.ResetTrigger(currentAnimName);
            currentAnimName = animName;
            anim.SetTrigger(currentAnimName);
        }
    }

    protected virtual void OnNewStage(Stage stage)
    {
        // khoi tao lai cac manager cua tung stage
        this.stage = stage;
        brickManager = stage.brickManager;
        for (int i = 0; i < stage.brickManager.groundBrickObjectList.Count; i++)
        {
            Brick brickComponent = stage.brickManager.groundBrickObjectList[i];
            if (brickComponent.materialType == materialType)
            {
                brickComponent.ShowRenderer();  // khi character di vao stage thi cac brick cua character moi xuat hien
            }
        }
        rb.velocity = Vector3.zero;
    }
    public void EatGroundBrick(Brick brickComponent)
    {
        brickComponent.ChangeColor(this.materialType);
        brickComponent.TurnOffPhysics(); 
        brickComponent.transform.SetParent(balo.transform);

        if (baloBrickObjectList.Count > 0)
        {
            targetPosition += new Vector3(0, brickHeight, 0);
        }
        else
        {
            targetPosition = new Vector3(0, 0, 0);
        }
        StartCoroutine(brickComponent.MoveToBalo(targetPosition));
        baloBrickObjectList.Add(brickComponent);
        brickComponent.transform.rotation = balo.transform.rotation;

    }
    public void DropBrick()
    {
        Brick brickComponent = baloBrickObjectList[baloBrickObjectList.Count - 1];
        targetPosition = targetPosition - new Vector3(0, brickHeight, 0);
        brickComponent.transform.localPosition = Vector3.zero;
        brickComponent.transform.SetParent(null);
        brickComponent.MoveToFirstPosition();
        brickComponent.isGround = true;
        brickComponent.transform.rotation = Quaternion.Euler(Vector3.zero);
        baloBrickObjectList.RemoveAt(baloBrickObjectList.Count - 1);
    }



    public void FallAllBricks()
    {
        // set all parent null,
        if(baloBrickObjectList.Count>0)
        for (int i = baloBrickObjectList.Count - 1; i >= 0; i--)
        {
            Brick brickComponent = baloBrickObjectList[i].GetComponent<Brick>();
            baloBrickObjectList[i].transform.SetParent(null);
            brickComponent.ChangeColor(MaterialType.Grey);
            brickComponent.TurnOnPhysics();
            brickComponent.isGround=true;
        }
        baloBrickObjectList.Clear();
    }

    protected virtual void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.CompareTag(CachedString.DOOR_HIGH))
        {
            StartCoroutine(other.gameObject.GetComponent<DoorHight>().Open());
        }
        if (other.gameObject.CompareTag(CachedString.STAGE))
        {
            stage = other.gameObject.GetComponent<Stage>();
            OnNewStage(other.gameObject.GetComponent<Stage>());
        }

        if (other.gameObject.CompareTag(CachedString.CHARACTER))
        {
            if (other.gameObject.GetComponent<Character>().baloBrickObjectList.Count > baloBrickObjectList.Count
                && !other.gameObject.GetComponent<Character>().onBridge && !this.onBridge &&!this.isFalling)
            {
                isFalling = true;
                FallAllBricks();
                ChangeAnim(CachedString.FALL);
                characterMagnet.gameObject.SetActive(false);
                StartCoroutine(WaitAndStandUp());
            }
        }
        if (other.gameObject.CompareTag(CachedString.BRIDGE_TRIGGER))
        {
            onBridge = true;
            bridge = other.gameObject.GetComponent<Bridge>();
            bridgeDirection = bridge.bridgeDirection;
        }
    }
    protected virtual void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag(CachedString.BRIDGE_TRIGGER))
        {
            onBridge = false;
        }
    }
}