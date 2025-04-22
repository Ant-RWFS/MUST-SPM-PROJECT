using UnityEngine;

public class Stone : LandResource
{
    struct DynamicCircleColliderInfo
    {
        public float offsetY;
        public float radius;
    }

    [Header("Dynamic Hitbox Info")]
    public Vector2 normalOffset;
    public float normalRadius;
    [Space]
    public Vector2 smallOffset;
    public float smallRadius;

    private CircleCollider2D circleColl;

    protected override void Awake()
    {
        base.Awake();
        circleColl = GetComponentInChildren<CircleCollider2D>();
    }

    protected override void Start()
    {
        base.Start();
    }

    protected override void Update()
    {
        base.Update();
        DynamicHitbox();
    }

    protected override void OnDestroy()
    {
        base.OnDestroy();
    }

    private void DynamicHitbox()
    {
        if (anim.GetInteger("Stage") <= 12)
        {
            circleColl.offset = normalOffset;
            circleColl.radius = normalRadius;
        }
        else
        {
            circleColl.offset = smallOffset;
            circleColl.radius = smallRadius;
        }
    }
}
