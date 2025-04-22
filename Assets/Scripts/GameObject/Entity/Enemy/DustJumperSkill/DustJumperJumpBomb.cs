using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DustJumperJumpBomb : EnemySkill<DustJumperJumpBombStats>
{
    public GameObject dustJumperSkillGO;
    public DustJumperJumpBombReleaseState releaseState;

    protected override void Awake()
    {
        base.Awake();
        stateMachine = new EnemySkillStateMachine();
        releaseState = new DustJumperJumpBombReleaseState(this, stateMachine, "Release");
    }

    protected override void Start()
    {
        base.Start();
        stateMachine.Initialize(releaseState);
    }

    protected override void Update()
    {
        base.Update();
        
    }
}
