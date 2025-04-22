using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DustJumperIdleState : DustJumperChillState
{
    public DustJumperIdleState(Enemy<DustJumperStats> _entity, EnemyStateMachine _stateMachine, string _animBoolName, DustJumper _enemy) : base(_entity, _stateMachine, _animBoolName, _enemy)
    {
    }

    public override void Enter()
    {
        base.Enter();
        stateTimer = enemy.stats.idleTime.GetValue();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();
        if (stateTimer < 0)
        {
            stateMachine.ChangeState(enemy.moveState);
        }
    }
}
