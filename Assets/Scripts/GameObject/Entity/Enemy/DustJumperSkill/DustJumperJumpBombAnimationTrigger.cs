using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DustJumperJumpBombAnimationTrigger : MonoBehaviour
{
    private DustJumperJumpBomb enemy => GetComponentInParent<DustJumperJumpBomb>();
    private void AnimationDestroyTrigger()
    {
        enemy.AnimationFinishTrigger();
        Destroy(enemy.dustJumperSkillGO);
    }
    private void DamageTrigger()
    {
        CameraShake.Instance.CameraShakeEffect();
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
