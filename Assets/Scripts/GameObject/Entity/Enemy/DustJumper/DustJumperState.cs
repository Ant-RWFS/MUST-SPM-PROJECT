using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DustJumperState : EnemyState<DustJumperStats>
{
    protected DustJumper enemy;

    public DustJumperState(Enemy<DustJumperStats> _entity, EnemyStateMachine _stateMachine, string _animBoolName, DustJumper _enemy) : base(_entity, _stateMachine, _animBoolName)
    {
        this.enemy = _enemy;
    }

    public override void Update()
    {
        base.Update();
        if (enemy.stats.currentHealth.GetValue() <= 0)
        {
            stateMachine.ChangeState(enemy.deathState);
        }
    }
}
