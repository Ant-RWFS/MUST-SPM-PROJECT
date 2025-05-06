using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Guide : NPC<GuideStats>
{
    public override GuideStats stats { get => base.stats; set => base.stats = value; }

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
    }

    protected override void OnDestroy()
    {
        base.OnDestroy();
    }

    public override void Die()
    {
        base.Die();
    }
}
