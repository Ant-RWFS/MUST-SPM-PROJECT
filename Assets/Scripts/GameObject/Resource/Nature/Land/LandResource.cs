using UnityEngine;

public class LandResource : Resource
{
    private bool edgeResource;

    protected override void Awake()
    {
        base.Awake();

        edgeResource = false;
    }

    protected override void Start()
    {
        base.Start();
    }

    protected override void OnDestroy()
    {
        base.OnDestroy();

        if (edgeResource)
            MapGenerator.instance.ItemAdjust(x, y);
        else
            MapGenerator.instance.ItemRegenerate(x, y);
    }

    protected override void Update()
    {
        base.Update();

        if (Physics2D.OverlapCircle(transform.position, .5f, LayerMask.GetMask("Water")))
        {
            edgeResource = true;
            Destroy(self);
        }
    }
}
