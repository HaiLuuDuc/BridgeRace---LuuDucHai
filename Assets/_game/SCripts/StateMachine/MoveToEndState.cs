using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveToEndState : IState
{
    public void OnEnter(Bot bot) { 
        bot.ChangeAnim(CachedString.RUN);

    }
    public void OnExecute(Bot bot)
    {
        if (bot.elevator != null)
        {
            if (bot.elevator.isMoving)
            {
                bot.DeactiveNavMeshAgent();
                bot.ChangeAnim(CachedString.IDLE);
            }
            else
            {
                bot.ActiveNavMeshAgent();
                bot.ChangeAnim(CachedString.RUN);
                bot.navMeshAgent.SetDestination(bot.endSpot.transform.position);
            }
        }
        if(bot.navMeshAgent!=null)
        {
            bot.direction = bot.navMeshAgent.velocity;
            bot.transform.rotation = Quaternion.LookRotation(new Vector3(bot.direction.x, 0, bot.direction.z));
            bot.navMeshAgent.SetDestination(bot.endSpot.transform.position);
            if (bot.baloBrickObjectList.Count == 0 && !bot.isFalling)
            {
                bot.navMeshAgent.velocity = Vector3.zero;
                bot.navMeshAgent.isStopped = true;
                bot.ChangeState(new MoveToBrickState());
            }
        }
            
  
        
    }

    public void OnExit(Bot bot) { }

}
