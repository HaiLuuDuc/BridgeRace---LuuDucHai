using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : IState
{
    public void OnEnter(Bot bot) { 
        bot.DeactiveNavMeshAgent();
    }
    public void OnExecute(Bot bot)
    {
        bot.ChangeAnim(CachedString.IDLE);
    }

    public void OnExit(Bot bot) { }

}
