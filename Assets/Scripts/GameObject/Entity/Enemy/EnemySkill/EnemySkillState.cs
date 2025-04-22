using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IEnemySkillState<out T> where T : EnemySkillStats
{
    void Enter();
    void Update();
    void Exit();
    void AnimationFinishTrigger();
}
public class EnemySkillState<SpecificEnemyStats> :IEnemySkillState<SpecificEnemyStats> where SpecificEnemyStats : EnemySkillStats
{
    protected EnemySkillStateMachine stateMachine;
    protected EnemySkill<SpecificEnemyStats> entity;


    protected Rigidbody2D rb;
    private string animBoolName;
    protected float stateTimer;
    protected bool triggerCalled;



    protected float Xdir;

    public EnemySkillState(EnemySkill<SpecificEnemyStats> _entity,EnemySkillStateMachine _stateMachine, string _animBoolName)
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
    protected void Flip(EnemySkill<SpecificEnemyStats> enemy)
    {


        if (PlayerManager.instance.playerTransform.right.x > 0)
        {
            Xdir = rb.velocity.x;

        } // when playerTransform.right== (0.00,1.00,0.00), x do not equal 0 but smaller that 0 in the "if" check, so here I use y to check at the same time.
        else if (PlayerManager.instance.playerTransform.right.x < 0 && PlayerManager.instance.playerTransform.right.y < 0.9 && PlayerManager.instance.playerTransform.right.y > -0.9)
        {
            Xdir = -rb.velocity.x;

        }
        else if (PlayerManager.instance.playerTransform.right.y > 0)
        {
            Xdir = rb.velocity.y;

        }
        else if (PlayerManager.instance.playerTransform.right.y < 0)
        {
            Xdir = -rb.velocity.y;

        }
        enemy.anim.SetFloat("Xdir", Xdir);
        //Debug.Log("Xdir"+Xdir);

    }
    protected void FlipByPosition(EnemySkill<SpecificEnemyStats> enemy)
    {


        if (PlayerManager.instance.playerTransform.right.x > 0)
        {
            Xdir = PlayerManager.instance.playerTransform.position.x - rb.position.x;

        }
        else if (PlayerManager.instance.playerTransform.right.x < 0 && PlayerManager.instance.playerTransform.right.y < 0.9 && PlayerManager.instance.playerTransform.right.y > -0.9)
        {

            Xdir = rb.position.x - PlayerManager.instance.playerTransform.position.x;

        }
        else if (PlayerManager.instance.playerTransform.right.y > 0)
        {
            Xdir = PlayerManager.instance.playerTransform.position.y - rb.position.y;

        }
        else if (PlayerManager.instance.playerTransform.right.y < 0)
        {

            Xdir = rb.position.y - PlayerManager.instance.playerTransform.position.y;

        }
        enemy.anim.SetFloat("Xdir", Xdir);

    }
}
