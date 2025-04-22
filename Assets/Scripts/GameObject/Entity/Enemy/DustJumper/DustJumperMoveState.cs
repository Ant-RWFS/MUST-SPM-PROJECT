using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DustJumperMoveState : DustJumperChillState
{
    float randomX;
    float randomY;
    public DustJumperMoveState(Enemy<DustJumperStats> _entity, EnemyStateMachine _stateMachine, string _animBoolName, DustJumper _enemy) : base(_entity, _stateMachine, _animBoolName, _enemy)
    {
    }

    public override void Enter()
    {
        base.Enter();
        stateTimer = enemy.stats.moveTime.GetValue();
        randomX = Random.Range(-3, 3);
        randomY = Random.Range(-3, 3);
        if (randomX == 0 && randomY == 0)
        {
            randomX = 1;
        }
    }

    public override void Exit()
    {
        enemy.SetEnemyMoveVelocity(0, 0);
        base.Exit();
    }

    public override void Update()
    {
        base.Update();
        enemy.SetEnemyMoveVelocity(randomX, randomY);
        if (stateTimer < 0)
        {

            stateMachine.ChangeState(enemy.idleState);
        }
    }
}
