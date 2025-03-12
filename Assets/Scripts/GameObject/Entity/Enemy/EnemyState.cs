using System.Collections;
using System.Collections.Generic;
using UnityEditor.Tilemaps;
using UnityEngine;

public class EnemyState : EntityState
{
    public EnemyState(Enemy _entity, EntityStateMachine _stateMachine, string _animBoolName) : base(_entity, _stateMachine, _animBoolName)
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

    protected void Flip(Enemy enemy) {
       

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
}
