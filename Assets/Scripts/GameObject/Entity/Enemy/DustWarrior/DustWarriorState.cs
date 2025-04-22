using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DustWarriorState : EnemyState<DustWarriorStats>
{
    protected DustWarrior enemy;
    public DustWarriorState(Enemy<DustWarriorStats> _entity, EnemyStateMachine _stateMachine, string _animBoolName, DustWarrior _enemy) : base(_entity, _stateMachine, _animBoolName)
    {
        this.enemy = _enemy;
    }
}
