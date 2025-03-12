using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DustWarriorIdleState : DustWarriorChillState
{
    public DustWarriorIdleState(Enemy _entity, EntityStateMachine _stateMachine, string _animBoolName, DustWarrior entity) : base(_entity, _stateMachine, _animBoolName, entity)
    {
    }

    public override void AnimationFinishTrigger()
    {
        base.AnimationFinishTrigger();
    }

    public override void Enter()
    {
        base.Enter();
        stateTimer =enemy.idleTime;
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
