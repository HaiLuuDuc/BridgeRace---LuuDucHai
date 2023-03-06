using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine;

public class WinState : IState
{
    public void OnEnter(Bot bot) {
        bot.ChangeAnim(CachedString.WIN);
        bot.DeactiveNavMeshAgent();

    }
    public void OnExecute(Bot bot)
    {

    }
    public void OnExit(Bot bot) { }
}
