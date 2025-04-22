using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DustWarrior : Enemy<DustWarriorStats>
{

    public DustWarriorIdleState idleState { get; private set; }
    public DustWarriorMoveState moveState { get; private set; }
    public DustWarriorBattleState battleState { get; private set; }
    public DustWarriorAttackOneState attackState { get; private set; }
    public DustWarriorSpinAttackState spinAttackState { get; private set; }
    public DustWarriorSkillState skillState { get; private set; }
    public DustWarriorDeathState deathState { get; private set; }
    protected override void Awake()
    {
        base.Awake();
        stateMachine = new EnemyStateMachine();
        idleState = new DustWarriorIdleState(this, stateMachine, "Idle", this);
        moveState = new DustWarriorMoveState(this, stateMachine, "Move", this);
        battleState = new DustWarriorBattleState(this, stateMachine, "Move", this);
        attackState = new DustWarriorAttackOneState(this, stateMachine, "Attack", this);
        spinAttackState = new DustWarriorSpinAttackState(this, stateMachine, "Spin", this);
        skillState = new DustWarriorSkillState(this, stateMachine, "Skill", this);
        deathState = new DustWarriorDeathState(this, stateMachine, "Death", this);
    }

    protected override void Start()
    {
        base.Start();

        stateMachine.Initialize(idleState);
    }

    protected override void Update()
    {
        base.Update();
    }
}
