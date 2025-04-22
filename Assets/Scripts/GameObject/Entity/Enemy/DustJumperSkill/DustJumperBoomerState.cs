using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DustJumperBoomerState : EnemySkillState<DustJumperBoomerStats>
{
    protected DustJumperBoomer enemy;

    public DustJumperBoomerState(EnemySkill<DustJumperBoomerStats> _entity, EnemySkillStateMachine _stateMachine, string _animBoolName, DustJumperBoomer _enemy) : base(_entity, _stateMachine, _animBoolName)
    {
        this.enemy = _enemy;    
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
        if (enemy.IsPlayerDetected())
        { enemy.stateMachine.ChangeState(enemy.bombState);

            
        }
    }
}
