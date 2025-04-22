using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DustWarriorStats : EnemyStats
{
    [Header("Special Attack info")]
    public Stat spinDamageNumber;
    public FloatStat SpecialAttackCooldown;
    public FloatStat lastTimeSpecialAttacked;

    [SerializeField] public GameObject skillObject;
    [SerializeField] public GameObject dustWarriorGO;
}
