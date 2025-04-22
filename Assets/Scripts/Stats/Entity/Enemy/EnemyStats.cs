using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStats : EntityStats
{
    [Header("Move info")]
    public Stat moveTime;
    public Stat idleTime;
    public Stat chaseTime;
    [SerializeField] public Vector2 previousVelocity;
    [Header("Attack info")]
    public Stat chaseSpeed;
    public FloatStat attackDistance;
    public Stat attackCooldown;
    public FloatStat lastTimeAttacked;
    public Transform damageCheck;
    public FloatStat damageDistance;
    public Stat damageNumber;

    [Header("PlayerCheck info")]
    public FloatStat playerCheckDistance;
    public LayerMask whatIsPlayer;
}
