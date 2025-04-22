using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySkillStateMachine
{
    public IEnemySkillState<EnemySkillStats> currentState { get; private set; }

    public void Initialize(IEnemySkillState<EnemySkillStats> _startState)
    {
        currentState = _startState;
        currentState.Enter();
    }

    public void ChangeState(IEnemySkillState<EnemySkillStats> _newState)
    {
        currentState.Exit();
        currentState = _newState;
        currentState.Enter();
    }
}
