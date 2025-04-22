using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DustJumperJumpingState : DustJumperState
{
    
    private Vector3 moveDir;
    public Transform playerTransform = null;

    public DustJumperJumpingState(Enemy<DustJumperStats> _entity, EnemyStateMachine _stateMachine, string _animBoolName, DustJumper _enemy) : base(_entity, _stateMachine, _animBoolName, _enemy)
    {
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider != null)
        {
            playerTransform = collider.gameObject.transform;
        }
        else { playerTransform = null; }
    }

    public override void Enter()
    {
        base.Enter();
        stateTimer = enemy.stats.SpecialAttackLastTime.GetValue();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();
       

        OnTriggerEnter2D(enemy.IsPlayerDetected());
        if (playerTransform != null)
        {
            moveDir = (playerTransform.position - enemy.transform.position).normalized;
            enemy.SetEnemyMoveVelocity(moveDir.x, moveDir.y, enemy.stats.chaseSpeed.GetValue());
            enemy.stats.previousVelocity = (enemy.rb.velocity);

        }
        else
        {
            stateMachine.ChangeState(enemy.jumpDownState);
        }
        if(playerTransform != null)
        {
            if (enemy.IsPlayerDetected())
            {
                float distance;
                distance = Physics2D.Distance(enemy.collider2d, enemy.IsPlayerDetected()).distance;
                if (distance < enemy.stats.SpecialAttackDistance.GetValue())
                {
                    stateMachine.ChangeState(enemy.jumpDownState);
                }
            }
        }
        if (stateTimer < 0)
        {
            stateMachine.ChangeState(enemy.jumpDownState);
        }
    }
}
