using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DustWarriorSkillOneAnimationTrigger : MonoBehaviour
{
    private DustWarriorSkillOne enemy => GetComponentInParent<DustWarriorSkillOne>();

    private void AnimationTrigger()
    {
        enemy.AnimationFinishTrigger();
        Destroy(enemy.stats.dustWarriorSkillGO);
    }
    private void DamageTrigger()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(enemy.stats.damageCheck.position, enemy.stats.damageDistance.GetValue(), enemy.stats.whatIsPlayer);

        foreach (var hit in colliders)
        {
            if (hit.GetComponentInParent<Player>() != null)
            {
                if (!PlayerManager.instance.player.stats.isInvisible)
                {
                    hit.GetComponentInParent<Player>().Damage(enemy.stats.damageNumber.GetValue());
                }
               
            }
            
        }
    }


}
