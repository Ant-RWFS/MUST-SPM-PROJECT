using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DustWarriorMoveState : DustWarriorChillState
{

    float randomX;
    float randomY;

    public DustWarriorMoveState(Enemy<DustWarriorStats> _entity, EnemyStateMachine _stateMachine, string _animBoolName, DustWarrior _enemy) : base(_entity, _stateMachine, _animBoolName, _enemy)
    {
    }

    //int moveDir;


    public override void AnimationFinishTrigger()
    {
        base.AnimationFinishTrigger();
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
