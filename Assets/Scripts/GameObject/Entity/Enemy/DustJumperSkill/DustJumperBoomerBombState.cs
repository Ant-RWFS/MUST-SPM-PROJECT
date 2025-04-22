using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DustJumperBoomerBombState : EnemySkillState<DustJumperBoomerStats>
{
    protected DustJumperBoomer enemy;

    public DustJumperBoomerBombState(EnemySkill<DustJumperBoomerStats> _entity, EnemySkillStateMachine _stateMachine, string _animBoolName,DustJumperBoomer _enemy) : base(_entity, _stateMachine, _animBoolName)
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
        enemy.SetEnemyMoveVelocity(0, 0);
        base.Update();
        
    }
}
