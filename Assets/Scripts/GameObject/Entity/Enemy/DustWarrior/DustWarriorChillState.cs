using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DustWarriorChillState : DustWarriorState
{

    

    public DustWarriorChillState(Enemy<DustWarriorStats> _entity, EnemyStateMachine _stateMachine, string _animBoolName, DustWarrior _enemy) : base(_entity, _stateMachine, _animBoolName, _enemy)
    {
    }

    public override void AnimationFinishTrigger()
    {
        base.AnimationFinishTrigger();
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Exit()
    {
        enemy.SetMoveVelocity(0, 0);
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

        if (enemy.stats.currentHealth.GetValue() <= 0)
        {
            stateMachine.ChangeState(enemy.deathState);
        }
        
    }
}
