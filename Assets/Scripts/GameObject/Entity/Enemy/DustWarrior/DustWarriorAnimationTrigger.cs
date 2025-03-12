using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DustWarriorAnimationTrigger : MonoBehaviour
{
    private DustWarrior enemy => GetComponentInParent<DustWarrior>();

    private void AnimationTrigger()
    {
        enemy.AnimationFinishTrigger();
    }

    private void DamageTrigger()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(enemy.damageCheck.position,enemy.damageDistance,enemy.whatIsPlayer);

        foreach(var hit in colliders)
        {
            if(hit.GetComponentInParent<Player>()!= null)
            {
                hit.GetComponentInParent<Player>().Damage(enemy.damageNumber);
                
            }
            else { Debug.Log("no"); }
        }
    }

    private void SpinDamageTrigger()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(enemy.damageCheck.position, enemy.damageDistance, enemy.whatIsPlayer);

        foreach (var hit in colliders)
        {
            if (hit.GetComponentInParent<Player>() != null)
            {
                hit.GetComponentInParent<Player>().Damage(enemy.spinDamageNumber);

            }
            else { Debug.Log("no"); }
        }
    }

    private void SkillTrigger()
    {
        GameObject skillObject = Instantiate(enemy.skillObject, enemy.transform.position, Quaternion.Euler(0.0f, 0.0f, 0.0f), enemy.dustWarriorGO.transform);
    }

    private void DeathTrigger() { Destroy(enemy.dustWarriorGO); }
}
