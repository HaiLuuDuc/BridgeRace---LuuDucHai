using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolState : IState
{
    public void OnEnter(Bot bot) { }
    public void OnExecute(Bot bot)
    {

        if (bot.baloBrickObjectList.Count >= bot.maxBrickCount)
        {
            bot.ChangeAnim("run");
            bot.navMeshAgent.SetDestination(bot.endSpot);
        }
        else
        {
            bot.ChangeAnim("run");
            bot.MoveToNearestBrick();
        }

        /*if (!bot.onBridge && (bot.baloBrickObjectList.Count >= bot.maxBrickCount))
        {
            bot.ChangeAnim("run");
            bot.MoveToBeginSpot();
            goto label;
        }
        if (bot.onBridge)
        {
            Debug.Log("change state @@");
            bot.ChangeState(new GoBridgeState());
            goto label;

        }
        if (!bot.onBridge && !bot.isFalling)
        {
            bot.ChangeAnim("run");
            bot.MoveToNearestBrick();
        }
    label:;*/
    }
    public void OnExit(Bot bot) { }
}
