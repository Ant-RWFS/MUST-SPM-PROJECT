using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DustJumperBoomerGroundState : DustJumperBoomerState
{
    public DustJumperBoomerGroundState(EnemySkill<DustJumperBoomerStats> _entity, EnemySkillStateMachine _stateMachine, string _animBoolName, DustJumperBoomer _enemy) : base(_entity, _stateMachine, _animBoolName, _enemy)
    {
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();
        enemy.SetEnemyMoveVelocity(0, 0);
        if (triggerCalled)
        {
            enemy.stateMachine.ChangeState(enemy.bombState);

        }

    }
}

