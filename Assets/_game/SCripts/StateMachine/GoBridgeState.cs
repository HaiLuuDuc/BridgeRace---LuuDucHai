using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class GoBridgeState : IState
{
    public void OnEnter(Bot bot) { }
    public void OnExecute(Bot bot)
    {
        if (bot.onBridge)
        {
            if (bot.baloBrickObjectList.Count > 0)
            {
                bot.rb.velocity = bot.direction * bot.speed / 1.5f;
                bot.transform.rotation = Quaternion.LookRotation(new Vector3(bot.direction.x, 0, bot.direction.z));
                bot.ChangeAnim("run");
            }
            else
            {
                bot.ChangeAnim("run");
                bot.rb.velocity = Vector3.back * bot.speed / 3;
                bot.transform.rotation = Quaternion.LookRotation(Vector3.back);

            }

        }
        if (!bot.onBridge)
        {
            bot.ChangeState(new PatrolState());
        }
    }
    public void OnExit(Bot bot) { }
}
