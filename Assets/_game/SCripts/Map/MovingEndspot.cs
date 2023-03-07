using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingEndspot : MonoBehaviour
{
    [SerializeField] Bot bot;
    [SerializeField] Vector3 offset;
    [SerializeField] Stage stage;
    private void Start()
    {
        offset = new Vector3(0, 3, 3);
    }
    void Update()
    {
        if (bot != null)
        {
            if (bot.onBridge && bot.stage == this.stage)
            {
                transform.position = bot.transform.position + offset;
            }
        }
        
    }
}
