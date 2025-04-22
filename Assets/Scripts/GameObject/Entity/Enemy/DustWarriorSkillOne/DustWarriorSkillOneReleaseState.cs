using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DustWarriorSkillOneReleaseState : EnemySkillState<DustWarriorSkillOneStats>
{
    protected DustWarriorSkillOne enemy;


    public DustWarriorSkillOneReleaseState(EnemySkill<DustWarriorSkillOneStats> _entity, EnemySkillStateMachine _stateMachine, string _animBoolName, DustWarriorSkillOne _enemy) : base(_enemy,_stateMachine, _animBoolName)
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
    }
    public override void Update()
    {
        base.Update();
        FlipByPosition(enemy);


       
    }





}
