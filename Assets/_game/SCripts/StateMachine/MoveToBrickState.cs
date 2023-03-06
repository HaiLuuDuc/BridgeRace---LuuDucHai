using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveToBrickState : IState
{
    public void OnEnter(Bot bot) { 

    }
    public void OnExecute(Bot bot) {
        if (bot.isFalling) {
            bot.rb.velocity = Vector3.zero;
            bot.navMeshAgent.isStopped= true;
            return;
        } 
        if (!bot.isFalling)
        {
            bot.navMeshAgent.isStopped = false;
            bot.MoveToNearestBrick();
            if (bot.baloBrickObjectList.Count >= bot.maxBrickCount)
            {
                bot.ChangeState(new MoveToEndState());
            }
            bot.ChangeAnim(CachedString.RUN);

        }

    }

    public void OnExit(Bot bot) { }

}
