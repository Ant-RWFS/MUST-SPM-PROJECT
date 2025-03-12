using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DustWarriorSpinAttackState : EntityState
{
    protected DustWarrior enemy;
    public DustWarriorSpinAttackState(Enemy _entity, EntityStateMachine _stateMachine, string _animBoolName, DustWarrior enemy) : base(_entity, _stateMachine, _animBoolName)
    {
        this.enemy = enemy;
    }

    public override void Enter()
    {
        base.Enter();
        
        
    }

    public override void Exit()
    {
        base.Exit();
        enemy.lastTimeSpecialAttacked = Time.time;
    }

    public override void Update()
    {
        base.Update();
        enemy.SetEnemyMoveVelocity(enemy.previousVelocity,5f);

        if (triggerCalled)
            stateMachine.ChangeState(enemy.battleState);

        if (enemy.stats.currentHealth <= 0)
        {
            stateMachine.ChangeState(enemy.deathState);
        }

    }
}
