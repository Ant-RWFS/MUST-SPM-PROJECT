using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DustWarriorSpinAttackState : DustWarriorState
{
    

    public DustWarriorSpinAttackState(Enemy<DustWarriorStats> _entity, EnemyStateMachine _stateMachine, string _animBoolName, DustWarrior _enemy) : base(_entity, _stateMachine, _animBoolName, _enemy)
    {
    }

    public override void Enter()
    {
        base.Enter();
  
    }

    public override void Exit()
    {
        base.Exit();
        enemy.stats.lastTimeSpecialAttacked.SetValue(Time.time);
    }

    public override void Update()
    {
        base.Update();
        enemy.SetEnemyMoveVelocity(enemy.stats.previousVelocity,5f);

        if (triggerCalled)
            stateMachine.ChangeState(enemy.battleState);

        if (enemy.stats.currentHealth.GetValue() <= 0)
        {
            stateMachine.ChangeState(enemy.deathState);
        }
        

    }
}
