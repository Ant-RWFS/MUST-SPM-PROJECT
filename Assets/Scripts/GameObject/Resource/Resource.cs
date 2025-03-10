using UnityEngine;

public class Resource : MonoBehaviour
{
    #region Components
    public ResourceStats stats { get; private set; }
    public SpriteRenderer sr { get; private set; }
    public Animator anim { get; private set; }
    #endregion
    public int Id;
    public int InitId;

    protected virtual void Awake()
    {
        sr = GetComponentInChildren<SpriteRenderer>();
        anim = GetComponentInChildren<Animator>();
        stats = GetComponent<ResourceStats>();
    }
    protected virtual void Start()
    {
        anim.SetInteger("Num", InitId);
    }

    protected virtual void Update()
    {

    }

    protected virtual void OnDestroy()
    {
    }
}
