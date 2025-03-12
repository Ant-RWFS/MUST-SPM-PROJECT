using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;


public class DustWarriorBattleState : EnemyState
{
    protected DustWarrior enemy;
    public Transform playerTransform = null;
    
    // This playerTransform is the playerTransform that will be captured by enemy, used as the target of chasing, which means it needs work when enemy capture a player
    // The playerTransform from PlayerManager are used when animator of enemy play animation, which mean it needs work whether enemy can capture a player.
    private Vector3 moveDir;
    private Vector3 enemyAnchor;

    public DustWarriorBattleState(Enemy _entity, EntityStateMachine _stateMachine, string _animBoolName, DustWarrior _enemy) : base(_entity, _stateMachine, _animBoolName)
    {
        this.enemy = _enemy;
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
    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider != null)
        {
            if (collider.gameObject.CompareTag("Player"))
            {

                playerTransform = collider.gameObject.transform;
                
            }
        }
        else { playerTransform = null; }
    }

    #region CanAttack
    private bool CanAttack()
    {
        if (Time.time >= enemy.lastTimeAttacked + enemy.attackCooldown)
        {
            enemy.lastTimeAttacked = Time.time;
            return true;

        }
        else
        {
            return false;
        }
    }
    private bool CanSpecialAttack()
    {
        if (Time.time >= enemy.lastTimeSpecialAttacked + enemy.SpecialAttackCooldown)
        {
            enemy.lastTimeSpecialAttacked = Time.time;
            return true;

        }
        else
        {
            return false;
        }
    }
    #endregion

    public override void Update()
    {
        base.Update();

       

        #region FindingAndChasing

        OnTriggerEnter2D(enemy.IsPlayerDetected());
        if (playerTransform != null)
        {
            moveDir = playerTransform.position - enemy.transform.position;
            enemy.SetEnemyMoveVelocity(moveDir.x, moveDir.y,enemy.chaseSpeed);
            enemy.previousVelocity=(enemy.rb.velocity);
           
            
        }
        else {
            stateMachine.ChangeState(enemy.idleState);
        }


        #endregion

        Flip(enemy);
        //Debug.Log(PlayerManager.instance.playerTransform.right);

        #region StartAttack
        int randomA = Random.Range(-2, 3);
        
        if (enemy.IsPlayerDetected())
        {
            float distance;
            distance = Physics2D.Distance(enemy.collider2d, enemy.IsPlayerDetected()).distance;
            if (distance < enemy.attackDistance) {

                if (CanSpecialAttack())
                {
                    if (randomA > 0) { stateMachine.ChangeState(enemy.spinAttackState); }
                    else
                    {
                    stateMachine.ChangeState(enemy.skillState);
                        
                    }


                }else if (CanAttack()) {
                    stateMachine.ChangeState(enemy.attackState);
                }
            }
            

        }

        #endregion

        if (enemy.stats.currentHealth <= 0)
        {
            stateMachine.ChangeState(enemy.deathState);
        }
    }
}
