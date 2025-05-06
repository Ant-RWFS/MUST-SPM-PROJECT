using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct DroppedItem 
{
    public GameObject resourcePrefab;
    public int number;
}
public class Resource : MonoBehaviour
{
    [SerializeField] protected GameObject self;
    [SerializeField] public int maxAnimStage;

    #region Time Info
    [Header("Time Info")]
    [SerializeField] private float destroyTime = 5f;
    private float destroyTimer;

    [SerializeField] private float reactivateTime = 5f;
    private float reactivateTimer;

    [SerializeField] private float growTime = 5f;
    private float growTimer;
    #endregion

    [SerializeField] public List<DroppedItem> items;

    #region Components
    public ResourceStats stats { get; private set; }
    public SpriteRenderer sr { get; private set; }
    public Collider2D coll { get; private set; }
    public Animator anim { get; private set; }
    #endregion

    #region Init Info
    [HideInInspector] public int x;
    [HideInInspector] public int y;
    [HideInInspector] public int initStage;
    //[HideInInspector] public int currentStage;
    public int currentStage;
    #endregion

    private bool isDropped = false;
    private bool isReactive = false;
    private bool isRespawning = false;
    //private float activateTime = 5f;
    //private float activateTimer;

    //currentId用来记录资源目前状态来适配动画 玩家远离时资源sprite被禁用
    //玩家远离时如果InitId == CurrentId计时器开始计时 到时间则销毁gameObject释放资源
    //如果不等则保留该物体的运行 只判断玩家远近来禁用sprite或启用sprite渲染

    //注意：设置animator动画时根据资源的破损情况排序 破损越多（或体积越小 e.g.Stone） Stage越小
    protected virtual void Awake()
    {
        sr = GetComponentInChildren<SpriteRenderer>();
        anim = GetComponentInChildren<Animator>();
        stats = GetComponent<ResourceStats>();
        coll = GetComponentInChildren<Collider2D>();
    }
    protected virtual void Start()
    {
        anim.SetInteger("Stage", initStage);
        currentStage = initStage;
        stats.hp.SetValue(10 + currentStage * 10);
        reactivateTimer = 0;
        growTimer = 0;
    }

    protected virtual void Update()
    {
        DynamicResourceState();
        ResourceRespawn();

        if (Mathf.Abs(Vector3.Distance(transform.position, PlayerManager.instance.playerTransform.position)) >= MapGenerator.instance.radius - 3.5f)
        {
            DeactivateResource();
            CalculateDestroyTime();
        }
        else
        {
            ResourceInVisualRange();
            ResetDestroyTime();
        }
    }

    protected virtual void OnDestroy()
    {

    }

    private void DeactivateResource()
    {
        sr.enabled = false;
        anim.enabled = false;
        coll.enabled = false;
    }

    private void ActivateResource()
    {
        sr.enabled = true;
        anim.enabled = true;
        coll.enabled = true;
        anim.SetInteger("Stage", currentStage);
    }

    private void CalculateDestroyTime()
    {
        if (currentStage == initStage)
            destroyTimer += Time.deltaTime;

        if (destroyTimer >= destroyTime)
            Destroy(self);
    }

    private void ResetDestroyTime()
    {
        if (destroyTimer != 0)
            destroyTimer = 0;
    }

    private void ResourceInVisualRange()
    {
        if (stats.hp.GetValue() <= 0) 
        {
            DeactivateResource();
            ItemDrop();
        }
        else
            ActivateResource();
    }
    private void ItemDrop()
    {
        if (!isDropped)
        {
            foreach (var item in items)
            {
                for (int i = 0; i < item.number; i++)
                {
                    GameObject droppedItem = Instantiate(item.resourcePrefab, transform.position, Quaternion.identity, ItemManager.instance.itemTransform);
                }
            }

            isDropped = true;
        }
    }
    private void ResourceRespawn() 
    {
        if (stats.hp.GetValue() <= 0) 
        {
            reactivateTimer += Time.deltaTime;

            if (reactivateTimer >= reactivateTime) 
            {
                isReactive = true;
                reactivateTimer = 0;
            }
        }

        if (isReactive) 
        {
            stats.hp.SetValue(10);
            currentStage = 0;
            reactivateTimer = 0;
            isDropped = false;
            isReactive = false;
            isRespawning = true;
        }

        if (isRespawning) 
        {
            if (currentStage < initStage) 
            { 
                growTimer += Time.deltaTime;

                if (growTimer >= growTime)
                {
                    stats.hp.SetValue(stats.hp.GetValue() + 10);
                    growTimer = 0;
                }
            }
            else
                isRespawning = false;
        }
    }
    private void DynamicResourceState() 
    {
        if (stats.hp.GetValue() > 10)
            currentStage = (int)(stats.hp.GetValue() / 10 - 1);
        else
            currentStage = (int)(stats.hp.GetValue() / 10);
    }
    
    public void Damage(int _damage) => stats.hp.SetValue(stats.hp.GetValue() - (int)(_damage * 0.4f));
}
