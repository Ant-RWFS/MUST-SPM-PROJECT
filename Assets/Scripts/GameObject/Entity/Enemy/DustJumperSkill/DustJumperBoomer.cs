using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DustJumperBoomer : EnemySkill<DustJumperBoomerStats>
{
    public GameObject dustJumperSkillGO;
    public DustJumperBoomerFlyingState flyingState { get; private set; }
    public DustJumperBoomerFallingState fallingState { get; private set; }
    public DustJumperBoomerGroundState groundState { get; private set; }
    public DustJumperBoomerBombState bombState { get; private set; }

    protected override void Awake()
    {
        base.Awake();
        stateMachine = new EnemySkillStateMachine();
        flyingState = new DustJumperBoomerFlyingState(this,stateMachine,"Flying",this);
        fallingState = new DustJumperBoomerFallingState(this, stateMachine, "Falling", this);
        groundState = new DustJumperBoomerGroundState(this, stateMachine,"Ground",this);
        bombState = new DustJumperBoomerBombState(this, stateMachine, "Bomb", this);
    }

    protected override void Start()
    {
        base.Start();
        stateMachine.Initialize(flyingState);
    }

    protected override void Update()
    {
        base.Update();
    }
}
