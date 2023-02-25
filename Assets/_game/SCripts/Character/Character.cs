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


    [SerializeField] private GameObject brickTile;
    [SerializeField] protected BrickManager brickManager;
    [SerializeField] private CharacterMagnet characterMagnet;
    [SerializeField] private GameObject fallenBricks;
    [SerializeField] private GameObject balo;
    [SerializeField] protected GameObject bridge;
    private float brickHeight = 0.6f;
    protected Vector3 bridgeDirection;
    protected string currentAnimName = CachedString.IDLE;
    public Rigidbody rb;
    public Animator anim;
    public Vector3 direction;
    public List<GameObject> baloBrickObjectList = new List<GameObject>();
    public Vector3 targetPosition = Vector3.zero;
    public float speed;
    public bool isFalling = false;
    public float maxPosY = 20f;
    public bool onBridge = false;

    IEnumerator WaitAndStandUp()
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

    protected virtual void OnNewStage(GameObject stage)
    {
        // khoi tao lai cac manager cua tung stage
        GameObject newManager = stage.transform.Find(CachedString.MANAGER).gameObject;
        brickManager = newManager.transform.Find(CachedString.BRICK_MANAGER).gameObject.GetComponent<BrickManager>();
        brickTile = stage.transform.Find(CachedString.GRID).GetChild(0).gameObject;
        fallenBricks = stage.transform.Find(CachedString.FALLEN_BRICKS).gameObject;
        maxPosY = transform.position.y;
        for (int i = 0; i < brickManager.groundBrickObjectList.Count; i++)
        {
            Brick brickComponent = brickManager.groundBrickObjectList[i].GetComponent<Brick>();
            if (brickComponent.materialType == materialType)
            {
                brickComponent.ShowRenderer();  // khi character di vao stage thi cac brick cua character moi xuat hien
            }
        }
        rb.velocity = Vector3.zero;
    }
    public void EatGroundBrick(GameObject brick)
    {
        Brick brickComponent = brick.GetComponent<Brick>();
        brickComponent.ChangeColor(this.materialType);
        brickComponent.TurnOffPhysics();
        brick.transform.SetParent(balo.transform);

        if (baloBrickObjectList.Count > 0)
        {
            targetPosition += new Vector3(0, brickHeight, 0);
        }
        else
        {
            targetPosition = new Vector3(0, 0, 0);
        }
        StartCoroutine(brickComponent.MoveToBalo(targetPosition));
        baloBrickObjectList.Add(brick);
        brick.transform.rotation = balo.transform.rotation;

        /*brick.GetComponent<Brick>().ChangeColor(this.materialType);
        int index=-1;
        if (brickManager.groundBrickPositionList.IndexOf(brick.transform.position) >=0  )
        {
            index = brickManager.groundBrickPositionList.IndexOf(brick.transform.position);
            brickManager.groundBrickStatusList[index] = false; // set status theo position vua an
            baloBrickObjectList.Add(brick);
            brick.transform.SetParent(balo.transform);
            if (baloBrickObjectList.Count > 0)
            {
                targetPosition = targetPosition + new Vector3(0, brickHeight, 0);
            }
            else
            {
                targetPosition = new Vector3(0, 0, 0);
            }
            StartCoroutine(brick.GetComponent<Brick>().MoveToBalo(targetPosition));
            brick.transform.rotation = balo.transform.rotation;
        }*/

    }
    /*    public void EatFallBrick(GameObject brick)
        {
            brick.GetComponent<Brick>().ChangeColor(materialType);
            brick.gameObject.GetComponent<Brick>().isFall = false;

            // bat physics cho brick
            brick.gameObject.GetComponent<Rigidbody>().useGravity = false;
            brick.gameObject.GetComponent<Rigidbody>().isKinematic = true;
            brick.gameObject.GetComponent<BoxCollider>().isTrigger = true;
            brick.transform.SetParent(balo.transform);
            baloBrickObjectList.Add(brick);
            if (baloBrickObjectList.Count > 0)
            {
                targetPosition = targetPosition + new Vector3(0, brickHeight, 0);
            }
            else
            {
                targetPosition = new Vector3(0, 0, 0);
            }
            StartCoroutine(brick.GetComponent<Brick>().MoveToBalo(targetPosition)); // hieu ung brick bay len
            brick.transform.rotation = balo.transform.rotation;
        }*/
    public void DropBrick()
    {
        targetPosition = targetPosition - new Vector3(0, brickHeight, 0);
        baloBrickObjectList[baloBrickObjectList.Count - 1].transform.localPosition = Vector3.zero;
        baloBrickObjectList[baloBrickObjectList.Count - 1].transform.SetParent(null);
        baloBrickObjectList[baloBrickObjectList.Count - 1].GetComponent<Brick>().MoveToFirstPosition();
        baloBrickObjectList[baloBrickObjectList.Count - 1].GetComponent<Brick>().isGround = true;
        baloBrickObjectList[baloBrickObjectList.Count - 1].transform.rotation = Quaternion.Euler(Vector3.zero);
        baloBrickObjectList[baloBrickObjectList.Count - 1].transform.SetParent(brickTile.transform);
        baloBrickObjectList.RemoveAt(baloBrickObjectList.Count - 1);
    }



    public void FallAllBricks()
    {
        // set all parent null,
        if(baloBrickObjectList.Count>=0)
        for (int i = baloBrickObjectList.Count - 1; i >= 0; i--)
        {
            Brick brickComponent = baloBrickObjectList[i].GetComponent<Brick>();
            brickComponent.ChangeColor(MaterialType.Grey);
            brickComponent.TurnOnPhysics();
            brickComponent.isGround=true;
            baloBrickObjectList[i].transform.SetParent(null);
        }
        baloBrickObjectList.Clear();
    }
    public IEnumerator FallBack()
    {
        float elapsedTime = 0;
        float duration = 0.2f;
        targetPosition = Vector3.zero;
        while (elapsedTime < duration)
        {
            elapsedTime+= Time.deltaTime;
            yield return null;
        }
    }

    protected virtual void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.CompareTag(CachedString.DOOR_HIGH))
        {
            StartCoroutine(other.gameObject.GetComponent<DoorHight>().Open());
        }
        if (other.gameObject.CompareTag(CachedString.GROUND))
        {
            OnNewStage(other.gameObject.transform.parent.transform.parent.gameObject);
        }

        if (other.gameObject.CompareTag(CachedString.CHARACTER))
        {
            if (other.gameObject.GetComponent<Character>().baloBrickObjectList.Count >= baloBrickObjectList.Count)
            {
                isFalling = true;
                FallAllBricks();
                StartCoroutine(FallBack());
                ChangeAnim(CachedString.FALL);
                characterMagnet.gameObject.SetActive(false);
                StartCoroutine(WaitAndStandUp());
            }
        }
        if (other.gameObject.CompareTag(CachedString.BRIDGE_TRIGGER))
        {
            onBridge = true;
            bridge = other.gameObject.transform.parent.gameObject;
            bridgeDirection = bridge.GetComponent<Bridge>().bridgeDirection;
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