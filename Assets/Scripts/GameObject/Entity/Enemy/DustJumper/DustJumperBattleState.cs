using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;

public class DustJumperBattleState : DustJumperState
{
   
    public Transform playerTransform = null;
    private Vector3 moveDir;
    private Vector3 enemyAnchor;

    public DustJumperBattleState(Enemy<DustJumperStats> _entity, EnemyStateMachine _stateMachine, string _animBoolName, DustJumper _enemy) : base(_entity, _stateMachine, _animBoolName, _enemy)
    {
        this.enemy = _enemy;
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
    }

    public override void Exit()
    {
        enemy.SetMoveVelocity(0, 0);
        base.Exit();
    }

    private bool CanAttack()
    {
        if (Time.time >= enemy.stats.lastTimeAttacked.GetValue() + enemy.stats.attackCooldown.GetValue())
        {
            enemy.stats.lastTimeAttacked.SetValue(Time.time);
            return true;

        }
        else
        {
            return false;
        }
    }
    private bool CanSpecialAttack()
    {
        if (Time.time >= enemy.stats.lastTimeSpecialAttacked.GetValue() + enemy.stats.SpecialAttackCooldown.GetValue())
        {
            enemy.stats.lastTimeSpecialAttacked.SetValue(Time.time);
            return true;

        }
        else
        {
            return false;
        }
    }
    public override void Update()
    {
        base.Update();

        OnTriggerEnter2D(enemy.IsPlayerDetected());
        if (playerTransform != null)
        {
            moveDir = playerTransform.position - enemy.transform.position;
            enemy.SetEnemyMoveVelocity(0, 0);
           
        }
        else
        {
            stateMachine.ChangeState(enemy.idleState);
        }

        FlipByPosition(enemy);

        if (enemy.IsPlayerDetected())
        {
            float distance;
            distance = Physics2D.Distance(enemy.collider2d, enemy.IsPlayerDetected()).distance;
            if (distance < enemy.stats.attackDistance.GetValue())
            {

                if (CanSpecialAttack())
                {

                    stateMachine.ChangeState(enemy.upState);




                }
                else if (CanAttack())
                {
                    stateMachine.ChangeState(enemy.attackState);
                }
            }


        }
    }
}
