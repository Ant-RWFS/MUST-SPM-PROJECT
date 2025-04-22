using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DustJumperAnimationTrigger : MonoBehaviour
{
    private DustJumper enemy => GetComponentInParent<DustJumper>();

    private void AnimationTrigger()
    {
        enemy.AnimationFinishTrigger();
    }
    private void DeathTrigger() { Destroy(enemy.stats.dustJumperGO); }

    private void AttackTrigger()
    {
        GameObject skillObject=Instantiate(enemy.stats.skillObject, enemy.transform.position, Quaternion.identity, ItemManager.instance.itemTransform);

        Rigidbody2D skillRb = skillObject.GetComponentInChildren<Rigidbody2D>();
        Vector3 velocity;
        velocity = (PlayerManager.instance.playerTransform.position - enemy.transform.position).normalized;
        skillRb.velocity = velocity*4;
    }

    private void SpecialAttackTrigger()
    {
        Instantiate(enemy.stats.skillObjectSecond, enemy.transform.position, Quaternion.identity, ItemManager.instance.itemTransform);
    }
    private void CameraShakeTrigger()
    {
        CameraShake.Instance.CameraShakeEffect();
    }

    private void ColliderHideTrigger()
    {
        CircleCollider2D playerCollider = PlayerManager.instance.player.coll;
        
        Physics2D.IgnoreCollision(enemy.collider2d, playerCollider,true);
    }

    private void ColliderDisplayTrigger()
    {
        CircleCollider2D playerCollider = PlayerManager.instance.player.coll;

        Physics2D.IgnoreCollision(enemy.collider2d, playerCollider,false);
    }

}
