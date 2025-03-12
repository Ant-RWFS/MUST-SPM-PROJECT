using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DustWarriorSkillOneAnimationTrigger : MonoBehaviour
{
    private DustWarriorSkillOne enemy => GetComponentInParent<DustWarriorSkillOne>();

    private void AnimationTrigger()
    {
        enemy.AnimationFinishTrigger();
        Destroy(enemy.dustWarriorSkillGO);
    }
    private void DamageTrigger()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(enemy.damageCheck.position, enemy.damageDistance, enemy.whatIsPlayer);

        foreach (var hit in colliders)
        {
            if (hit.GetComponentInParent<Player>() != null)
            {
                hit.GetComponentInParent<Player>().Damage(enemy.damageNumber);

            }
            
        }
    }


}
