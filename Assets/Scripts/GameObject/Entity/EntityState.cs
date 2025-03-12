using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityState 
{

    protected EntityStateMachine stateMachine;
    protected Enemy entity;


    protected Rigidbody2D rb;
    private string animBoolName;
    protected float stateTimer;
    protected bool triggerCalled;

    
    protected float Xdir;
   
    public EntityState(Enemy _entity, EntityStateMachine _stateMachine,string _animBoolName)
    {

        this.entity = _entity;
        this.stateMachine = _stateMachine;
        this.animBoolName = _animBoolName;
    }


    public virtual void Enter()
    {
        triggerCalled = false;
        rb = entity.rb;
        entity.anim.SetBool(animBoolName, true);

    }

    public virtual void Update()
    {
        stateTimer -= Time.deltaTime;

    }
    public virtual void Exit()
    {
        entity.anim.SetBool(animBoolName, false);
    }

    public virtual void AnimationFinishTrigger()
    {
        triggerCalled = true;
    }

}
