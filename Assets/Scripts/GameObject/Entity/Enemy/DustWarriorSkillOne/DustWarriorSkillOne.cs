using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DustWarriorSkillOne : Enemy
{
    public GameObject dustWarriorSkillGO;
    public DustWarrior dustWarrior;
    public DustWarriorSkillOneReleaseState  releaseState { get; private set; }
    protected override void Awake()
    {
        base.Awake();
        stateMachine = new EntityStateMachine();
        //dustWarrior = GetComponentInParent<DustWarrior>();
        releaseState = new DustWarriorSkillOneReleaseState(this,stateMachine,"Release",this);
        
        
        
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
