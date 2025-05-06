using UnityEngine;

public interface EnemyInterface
{
    void Damage(int _damage);
    void SetKnockBack(float _x, float _y);
    EnemyStats statsI { get; }
    GameObject selfI { get; }
    Transform transformI { get; }
}

public class Enemy<SpecificEnemyStats> : Entity<SpecificEnemyStats>, EnemyInterface where SpecificEnemyStats : EnemyStats
{
    public GameObject self;
    public GameObject selfI => this.self ;
    public EnemyStats statsI => base.stats;
    public Transform transformI => this.transform;   
    public Collider2D collider2d { get; private set; }
    public Transform entityTransform { get; private set; }

    public EnemyStateMachine stateMachine;

    protected override void Awake()
    {
        base.Awake();
        collider2d = GetComponent<Collider2D>();
    }

    protected override void Start()
    {
        base.Start();
        entityTransform = collider2d.transform;
    }

    protected override void Update()
    {
        base.Update();
        stateMachine.currentState.Update();

        if (Mathf.Abs(Vector3.Distance(transform.position, PlayerManager.instance.playerTransform.position)) >= MapGenerator.instance.radius - 2.5f)
            DeactivateEnemy();
        else 
            ActivateEnemy();
    }
   
    public virtual Collider2D IsPlayerDetected() => Physics2D.OverlapCircle(new Vector2(stats.collisionCheck.position.x, stats.collisionCheck.position.y), stats.playerCheckDistance.GetValue(), stats.whatIsPlayer);
    public virtual void AnimationFinishTrigger() => stateMachine.currentState.AnimationFinishTrigger();
    public void SetEnemyMoveVelocity(float _x, float _y)
    {
        if (stats.isKnocked)
            return;

        Vector3 vector = new Vector3(_x, _y);

        rb.velocity = vector * stats.moveSpeed.GetValue();
        stats.rollSpeed.SetValue(stats.moveSpeed.GetValue());
    }
    public void SetEnemyMoveVelocity(float _x, float _y, float _chaseSpeed)
    {
        if (stats.isKnocked)
            return;

        Vector3 vector = new Vector3(_x, _y);

        rb.velocity = vector * _chaseSpeed * stats.moveSpeed.GetValue();
        stats.rollSpeed.SetValue(stats.moveSpeed.GetValue());
    }
    public void SetEnemyMoveVelocity(Vector2 _vector)
    {
        if (stats.isKnocked)
            return;

        rb.velocity = _vector * stats.chaseSpeed.GetValue() * stats.moveSpeed.GetValue();
        stats.rollSpeed.SetValue(stats.moveSpeed.GetValue());
    }
    public void SetEnemyMoveVelocity(Vector2 _vector, float _time)
    {
        if (stats.isKnocked)
            return;

        rb.velocity = _vector * _time * stats.moveSpeed.GetValue();
        stats.rollSpeed.SetValue(stats.moveSpeed.GetValue());
    }

    public void EnemyDie() => Destroy(self.gameObject);

    private void ActivateEnemy() 
    {
        sr.enabled = true;
        anim.enabled = true;
        collider2d.enabled = true;
    }

    private void DeactivateEnemy() 
    {
        sr.enabled = false;
        anim.enabled = false;
        collider2d.enabled = false;
    }
}
