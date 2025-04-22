using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DustJumperStats : EnemyStats
{
    [SerializeField] public GameObject dustJumperGO;

    [Header("Special Attack info")]
    public FloatStat SpecialAttackCooldown;
    public FloatStat lastTimeSpecialAttacked;
    public FloatStat SpecialAttackLastTime;
    public FloatStat SpecialAttackDistance;

    [SerializeField] public GameObject skillObject;
    [SerializeField] public GameObject skillObjectSecond;
}
