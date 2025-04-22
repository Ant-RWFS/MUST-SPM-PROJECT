using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DustJumper : Enemy<DustJumperStats>
{
    public DustJumperIdleState idleState;
    public DustJumperMoveState moveState;
    public DustJumpDeathState deathState;
    public DustJumperBattleState battleState;
    public DustJumperAttackState attackState;
    public DustJumperJumpUpState upState;
    public DustJumperJumpingState jumpingState;
    public DustJumperJumpDownState jumpDownState;

    protected override void Awake()
    {
        base.Awake();
        stateMachine = new EnemyStateMachine();
        idleState = new DustJumperIdleState(this, stateMachine, "Idle", this);
        moveState = new DustJumperMoveState(this, stateMachine, "Move", this);
        deathState = new DustJumpDeathState(this, stateMachine, "Death", this);
        battleState = new DustJumperBattleState(this,stateMachine,"Battle",this);
        attackState = new DustJumperAttackState(this, stateMachine, "Attack", this);
        upState = new DustJumperJumpUpState(this, stateMachine, "JumpUp", this);
        jumpingState = new DustJumperJumpingState(this, stateMachine, "Jumping", this);
        jumpDownState = new DustJumperJumpDownState(this, stateMachine, "JumpDown", this);
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
