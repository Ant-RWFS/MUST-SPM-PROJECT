using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DustWarriorDeathState : EnemyState
{
    protected DustWarrior enemy;
    public DustWarriorDeathState(Enemy _entity, EntityStateMachine _stateMachine, string _animBoolName, DustWarrior _enemy) : base(_entity, _stateMachine, _animBoolName)
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
        Flip(enemy);
        
    }
}
