using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DustJumperJumpDownState : DustJumperState
{
    public DustJumperJumpDownState(Enemy<DustJumperStats> _entity, EnemyStateMachine _stateMachine, string _animBoolName, DustJumper _enemy) : base(_entity, _stateMachine, _animBoolName, _enemy)
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
        FlipByPosition(enemy);
        enemy.SetEnemyMoveVelocity(0, 0);
        if (triggerCalled)
        {
            enemy.stateMachine.ChangeState(enemy.battleState);
        }
    }
}
