using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DustJumperAttackState : DustJumperState
{
   

    public DustJumperAttackState(Enemy<DustJumperStats> _entity, EnemyStateMachine _stateMachine, string _animBoolName, DustJumper _enemy) : base(_entity, _stateMachine, _animBoolName, _enemy)
    {
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Exit()
    {
        base.Exit();
        enemy.stats.lastTimeAttacked.SetValue(Time.time);
    }

    public override void Update()
    {
        base.Update();
        if (triggerCalled)
        {

            stateMachine.ChangeState(enemy.battleState);
        }
        if (enemy.stats.currentHealth.GetValue() <= 0)
        {
            stateMachine.ChangeState(enemy.deathState);
        }
    }
}
