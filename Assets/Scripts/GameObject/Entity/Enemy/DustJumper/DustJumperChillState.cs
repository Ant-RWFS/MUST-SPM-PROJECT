using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DustJumperChillState : DustJumperState
{
    public DustJumperChillState(Enemy<DustJumperStats> _entity, EnemyStateMachine _stateMachine, string _animBoolName, DustJumper _enemy) : base(_entity, _stateMachine, _animBoolName, _enemy)
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
        {
            stateMachine.ChangeState(enemy.battleState);
        }
        Flip(enemy);
    }
}
