using UnityEngine;

public class EntityStats : MonoBehaviour
{
    #region Info

    [Header("Stats Info")]
    public Stat maxHealth;
    public Stat currentHealth;
    public Stat damage;

    [Header("Velocity Info")]
    public Stat rollSpeed;
    public Stat moveSpeed;
    public Stat runSpeed;
    public Vector2 currentVector;

    [Header("Knock Info")]
    public bool isKnocked;
    public float knockDuration;
    public Vector3 knockVector;

    [Header("Survive Info")]
    public bool isDead;

    [Header("Other Info")]
    public bool isBusy;
    public bool isInvisible;

    [Header("Collision info")]
    [SerializeField] public Transform collisionCheck;
    #endregion

    protected virtual void Awake()
    {
        isDead = false;
    }

    protected virtual void Start()
    {
        InitCurrentHealthValue();
    }

    protected virtual void Update()
    {
        EntityDie();
    }

    private void EntityDie()
    {
        if (currentHealth.GetValue() < 0)
            isDead = true;
    }
    public int GetMaxHealthValue() => maxHealth.GetValue();
    public void InitCurrentHealthValue() => currentHealth.SetValue(GetMaxHealthValue());
}
