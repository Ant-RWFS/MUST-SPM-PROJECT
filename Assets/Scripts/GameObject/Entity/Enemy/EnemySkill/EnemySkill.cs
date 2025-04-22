using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySkill<SpecificEnemyStats> : Entity<SpecificEnemyStats> where SpecificEnemyStats : EnemySkillStats
{

    public Collider2D collider2d { get; private set; }

    public Transform entityTransform { get; private set; }

    //[Header("PlayerCheck info")]
    //[SerializeField] protected float playerCheckDistance = 1;
    //[SerializeField] public LayerMask whatIsPlayer;

    //[Header("Move info")]
    //public float moveTime;
    //public float idleTime;
    //public float chaseTime;
    //[SerializeField] public Vector2 previousVelocity;
    //[Header("Attack info")]
    //public float chaseSpeed;
    //public float attackDistance;
    //public float attackCooldown;
    //public float lastTimeAttacked;
    //public Transform damageCheck;
    //public float damageDistance;
    //public int damageNumber;

    public EnemySkillStateMachine stateMachine;
    protected override void Awake()
    {

        base.Awake();
        collider2d = GetComponent<Collider2D>();

    }

    protected override void Start()
    {

        base.Start();
        entityTransform = collider2d.transform;
    }

    protected override void Update()
    {
        base.Update();
        stateMachine.currentState.Update();


    }

    public virtual Collider2D IsPlayerDetected() => Physics2D.OverlapCircle(new Vector2(collisionCheck.position.x, collisionCheck.position.y), stats.playerCheckDistance.GetValue(), stats.whatIsPlayer);
    public virtual void AnimationFinishTrigger() => stateMachine.currentState.AnimationFinishTrigger();

    //protected virtual void OnDrawGizmos()
    //{

    //    Gizmos.DrawWireSphere(collisionCheck.position, playerCheckDistance);
    //    Gizmos.DrawWireSphere(collisionCheck.position, attackDistance);
    //    Gizmos.DrawWireSphere(collisionCheck.position, damageDistance);
    //}
    public void SetEnemyMoveVelocity(float _x, float _y)
    {
        if (stats.isKnocked)
            return;

        Vector3 vector = new Vector3(_x, _y);

        rb.velocity = vector * stats.moveSpeed.GetValue();
        stats.rollSpeed.SetValue(stats.moveSpeed.GetValue());
    }
    public void SetEnemyMoveVelocity(float _x, float _y, float _chaseSpeed)
    {
        if (stats.isKnocked)
            return;

        Vector3 vector = new Vector3(_x, _y);

        rb.velocity = vector * _chaseSpeed * stats.moveSpeed.GetValue();
        stats.rollSpeed.SetValue(stats.moveSpeed.GetValue());
    }
    public void SetEnemyMoveVelocity(Vector2 _vector)
    {
        if (stats.isKnocked)
            return;

        rb.velocity = _vector * stats.chaseSpeed.GetValue() * stats.moveSpeed.GetValue();
        stats.rollSpeed.SetValue(stats.moveSpeed.GetValue());
    }
    public void SetEnemyMoveVelocity(Vector2 _vector, float _time)
    {
        if (stats.isKnocked)
            return;

        rb.velocity = _vector * _time * stats.moveSpeed.GetValue();
        stats.rollSpeed.SetValue(stats.moveSpeed.GetValue());
    }
}
