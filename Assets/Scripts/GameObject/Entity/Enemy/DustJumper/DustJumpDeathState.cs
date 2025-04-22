using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DustJumpDeathState : DustJumperState
{
    

    public DustJumpDeathState(Enemy<DustJumperStats> _entity, EnemyStateMachine _stateMachine, string _animBoolName, DustJumper _enemy) : base(_entity, _stateMachine, _animBoolName, _enemy)
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
    }

    public override void Update()
    {
        base.Update();
        
    }
}
