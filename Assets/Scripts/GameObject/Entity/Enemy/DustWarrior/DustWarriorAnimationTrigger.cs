using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DustWarriorAnimationTrigger : MonoBehaviour
{
    public List<GameObject> droppedItems;
    private DustWarrior enemy => GetComponentInParent<DustWarrior>();

    private void AnimationTrigger()
    {
        enemy.AnimationFinishTrigger();
    }

    private void DamageTrigger()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(enemy.stats.damageCheck.position,enemy.stats.damageDistance.GetValue(),enemy.stats.whatIsPlayer);

        foreach(var hit in colliders)
        {
            if(hit.GetComponentInParent<Player>()!= null)
            {
                if (!PlayerManager.instance.player.stats.isInvisible)
                {
                    hit.GetComponentInParent<Player>().Damage(enemy.stats.damageNumber.GetValue());
                }
            }
            else { Debug.Log("no"); }
        }
    }

    private void SpinDamageTrigger()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(enemy.stats.damageCheck.position, enemy.stats.damageDistance.GetValue(), enemy.stats.whatIsPlayer);

        foreach (var hit in colliders)
        {
            if (hit.GetComponentInParent<Player>() != null)
            {
                if (!PlayerManager.instance.player.stats.isInvisible)
                {
                    hit.GetComponentInParent<Player>().Damage(enemy.stats.spinDamageNumber.GetValue());
                }
            }
            else { Debug.Log("no"); }
        }
    }

    private void SkillTrigger()
    {

        Vector3 enemyVelocity = enemy.rb.velocity;
        GameObject skillObject = Instantiate(enemy.stats.skillObject, enemy.transform.position, Quaternion.identity, enemy.stats.dustWarriorGO.transform);

    }

    private void DeathTrigger() { Destroy(enemy.stats.dustWarriorGO); }

    private void ItemDropTrigger() 
    {
        if (droppedItems.Count > 0)
        {
            GameObject droppedItem = Instantiate(droppedItems[Random.Range(0, droppedItems.Count)], enemy.transform.position, Quaternion.identity, ItemManager.instance.itemTransform);
        }
    }
}
