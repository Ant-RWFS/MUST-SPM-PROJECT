using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DustWarriorSkillState : EnemyState
{
    protected DustWarrior enemy;
    public DustWarriorSkillState(Enemy _entity, EntityStateMachine _stateMachine, string _animBoolName, DustWarrior _enemy) : base(_entity, _stateMachine, _animBoolName)
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
        enemy.lastTimeSpecialAttacked = Time.time;
    }

    public override void Update()
    {
        base.Update();
        enemy.SetEnemyMoveVelocity(enemy.previousVelocity,0.2f);

        if (triggerCalled)
            stateMachine.ChangeState(enemy.battleState);
        if (enemy.stats.currentHealth <= 0)
        {
            stateMachine.ChangeState(enemy.deathState);
        }
    }
}
