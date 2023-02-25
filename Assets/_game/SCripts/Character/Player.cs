using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Player : Character
{
    Vector3 firstMousePosition;
    Vector3 currentMousePosition;
    [SerializeField] private GameObject joystick;
    [SerializeField] private GameObject joystickBackground;
    [SerializeField] private GameObject joystickHandle;
    [SerializeField] private float offset;
    float joystickRadius = 120f;
    public void Start()
    {
        speed = 12f;
        ChangeColor(MaterialType.Blue);
    }

    protected override void Update()
    {

        base.Update();
        if (GameManger.Instance.isWin) {
            ChangeAnim(CachedString.WIN);
            return;
        }

        if (isFalling == false)
        {
            if (Input.GetMouseButtonDown(0))
            {
                firstMousePosition = Input.mousePosition;
                joystick.transform.position = firstMousePosition;
            }

            if (Input.GetMouseButton(0))
            {
                currentMousePosition = Input.mousePosition;
                direction = currentMousePosition - firstMousePosition;
                joystickHandle.transform.position = currentMousePosition;
                if (Vector3.Distance(joystickHandle.transform.position, joystickBackground.transform.position) > joystickRadius)
                {
                    joystickHandle.transform.position = joystickBackground.transform.position - (joystickBackground.transform.position - joystickHandle.transform.position).normalized * joystickRadius;
                }
                if (Vector3.Distance(joystickHandle.transform.position, joystickBackground.transform.position) > joystickRadius / 2)
                {
                    transform.rotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.y));
                    ChangeAnim("run");
                    if (onBridge && direction.y > 0.1f)
                    {
                        rb.velocity = new Vector3((float)direction.normalized.x, bridgeDirection.y, (float)direction.normalized.y) * speed / 1.3f;
                    }
                    else if (onBridge && direction.y < 0.1f)
                    {
                        rb.velocity = new Vector3((float)direction.normalized.x, -bridgeDirection.y+0.3f, (float)direction.normalized.y) * speed / 1.5f;
                    }
                    else
                    {
                        rb.velocity = new Vector3((float)direction.normalized.x, 0, (float)direction.normalized.y) * speed / 1.5f;
                    }
                }

            }
        }
        if (Input.GetMouseButtonUp(0))
        {
            if (!isFalling)
            {
                ChangeAnim("idle");
            }
            joystick.transform.position += new Vector3(10000, 0, 0);
            rb.velocity = new Vector3(0, rb.velocity.y, 0);
        }
    }
    /*    private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("winZone"))
            {
                ChangeAnim("win");
                isWin = true;
                GetComponent<CameraFollow>().container = this.gameObject;
            }
        }*/
    protected override void OnTriggerEnter(Collider other)
    {
        base.OnTriggerEnter(other);
        if (other.gameObject.CompareTag(CachedString.STEP))
        {

            if (other.gameObject.GetComponent<Step>().materialType != this.materialType)
            {
                if (baloBrickObjectList.Count > 0 && other.gameObject.transform.TransformPoint(other.transform.localPosition).y > maxPosY)
                {
                    StartCoroutine(other.gameObject.GetComponent<Step>().ChangeColorStep(materialType));
                    DropBrick();
                    maxPosY = other.gameObject.transform.TransformPoint(other.transform.localPosition).y - offset;

                }
            }
            else
            {
                maxPosY = other.gameObject.transform.TransformPoint(other.transform.localPosition).y ;
            }

        }
        if (other.gameObject.CompareTag(CachedString.WIN_ZONE))
        {
            GameManger.Instance.isWin = true;
            joystick.transform.position += new Vector3(1000, 0, 0);
            transform.rotation = Quaternion.Euler(new Vector3(0, 180, 0));

        }
    }
    protected override void OnTriggerExit(Collider other)
    {
        base.OnTriggerExit(other);
        if (other.gameObject.CompareTag(CachedString.BRIDGE_TRIGGER))
        {
            rb.velocity = Vector3.zero;
        }
    }
}
