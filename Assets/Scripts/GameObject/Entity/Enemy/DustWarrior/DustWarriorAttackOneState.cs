using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DustWarriorAttackOneState : EntityState
{
    protected DustWarrior enemy;
    public DustWarriorAttackOneState(Enemy _entity, EntityStateMachine _stateMachine, string _animBoolName, DustWarrior enemy) : base(_entity, _stateMachine, _animBoolName)
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
        enemy.lastTimeAttacked =Time.time;
    }

    public override void Update()
    {
        base.Update();
        enemy.SetEnemyMoveVelocity(enemy.previousVelocity);
        if (triggerCalled) {
            
            stateMachine.ChangeState(enemy.battleState);
        }
        if (enemy.stats.currentHealth <= 0)
        {
            stateMachine.ChangeState(enemy.deathState);
        }

    }
}
