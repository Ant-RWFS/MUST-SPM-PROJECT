using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DustJumperJumpBombReleaseState : EnemySkillState<DustJumperJumpBombStats>
{
    
    public DustJumperJumpBombReleaseState(EnemySkill<DustJumperJumpBombStats> _entity, EnemySkillStateMachine _stateMachine, string _animBoolName) : base(_entity, _stateMachine, _animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();
        
    }
}
