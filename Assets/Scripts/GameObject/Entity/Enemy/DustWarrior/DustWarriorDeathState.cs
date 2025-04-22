using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DustWarriorDeathState : DustWarriorState
{
   

    public DustWarriorDeathState(Enemy<DustWarriorStats> _entity, EnemyStateMachine _stateMachine, string _animBoolName, DustWarrior _enemy) : base(_entity, _stateMachine, _animBoolName, _enemy)
    {
    }

    public override void Enter()
    {
        base.Enter();
        enemy.LockRB();
    }

    public override void Exit()
    {
        base.Exit();
        enemy.UnlockRB();
        enemy.EnemyDie();
    }

    public override void Update()
    {
        base.Update();
        Flip(enemy);
        
    }
}
