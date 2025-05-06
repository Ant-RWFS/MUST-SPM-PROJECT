using Flower;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public interface NPCInterface
{
    void Damage(int _damage);
    void SetKnockBack(float _x, float _y);
    NPCStats statsI { get; }
    Transform transformI { get; }
}

public class NPC<Stats> : Entity<Stats>, NPCInterface where Stats : NPCStats
{
    public override Stats stats { get => base.stats; set => base.stats = value; }
    public NPCStats statsI => this.stats;
    public Transform transformI => this.transform;

    protected override void Awake()
    {
        base.Awake();
    }
    protected override void Start()
    {
        base.Start();
    }
    protected override void Update()
    {
        base.Update();

        if (Mathf.Abs(Vector3.Distance(transform.position, PlayerManager.instance.playerTransform.position)) >= MapGenerator.instance.radius - 3.5f)
            DeactivateNPC();
        else
            ActivateNPC();
    }
    protected override void OnDestroy()
    {
        base.OnDestroy();
    }
    public override void Die()
    {
        base.Die();
    }

    private void DeactivateNPC()
    {
        sr.enabled = false;
        anim.enabled = false;
    }

    private void ActivateNPC()
    {
        sr.enabled = true;
        anim.enabled = true;
    }

}
