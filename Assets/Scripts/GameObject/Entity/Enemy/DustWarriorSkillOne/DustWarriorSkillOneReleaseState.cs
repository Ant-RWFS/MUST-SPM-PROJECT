using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DustWarriorSkillOneReleaseState : EnemyState
{
    protected DustWarriorSkillOne enemy;


    public DustWarriorSkillOneReleaseState(Enemy _entity, EntityStateMachine _stateMachine, string _animBoolName, DustWarriorSkillOne _enemy) : base(_entity, _stateMachine, _animBoolName)
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
        //SkillFlip(enemy);


        if (triggerCalled)
        {
            enemy.DestroyEntity(enemy);

        }
    }
    //protected void SkillFlip(DustWarriorSkillOne enemy)
    //{


    //    if (PlayerManager.instance.playerTransform.right.x > 0)
    //    {
    //        Xdir = enemy.dustWarrior.rb.velocity.x;

    //    } // when playerTransform.right== (0.00,1.00,0.00), x do not equal 0 but smaller that 0 in the "if" check, so here I use y to check at the same time.
    //    else if (PlayerManager.instance.playerTransform.right.x < 0 && PlayerManager.instance.playerTransform.right.y < 0.9 && PlayerManager.instance.playerTransform.right.y > -0.9)
    //    {
    //        Xdir = -enemy.dustWarrior.rb.velocity.x;

    //    }
    //    else if (PlayerManager.instance.playerTransform.right.y > 0)
    //    {
    //        Xdir = enemy.dustWarrior.rb.velocity.y;

    //    }
    //    else if (PlayerManager.instance.playerTransform.right.y < 0)
    //    {
    //        Xdir = -enemy.dustWarrior.rb.velocity.y;

    //    }


    //}





}
