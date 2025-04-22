using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStateMachine 
{
    public IEnemyState<EnemyStats> currentState { get; private set; }

    public void Initialize(IEnemyState<EnemyStats> _startState)
    {
        currentState = _startState;
        currentState.Enter();
    }

    public void ChangeState(IEnemyState<EnemyStats> _newState)
    {
        currentState.Exit();
        currentState = _newState;
        currentState.Enter();
    }
}
