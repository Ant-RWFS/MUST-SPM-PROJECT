using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    private bool edgeItem;
    [SerializeField] public GameObject self;
    [SerializeField] private List<GameObject> enemies;

    [SerializeField] private float regenerateTime;
    private float regenerateTimer;

    #region Init Info
    [HideInInspector] public int x;
    [HideInInspector] public int y;
    #endregion

    private float destroyTime = 30f;
    private float destroyTimer;

    protected virtual void Awake()
    {
        edgeItem = false;
        RandomGenerateEnemy();
    }

    protected virtual void Start()
    {

    }

    protected virtual void Update()
    {
        EnemyGenerateLogic();
        AvoidSpawnedInWater();
        ActivateEnemyLogic();

        if (GetComponentInChildren<EnemyInterface>() != null)
        {
            if (Mathf.Abs(Vector3.Distance(GetComponentInChildren<EnemyInterface>().transformI.position, PlayerManager.instance.playerTransform.position)) >= MapGenerator.instance.radius - 2.5f)
                CalculateDestroyTime();
            else
                ResetDestroyTime();
        }
        else
            ResetDestroyTime();


    }

    protected virtual void OnDestroy()
    {
        if (edgeItem)
            MapGenerator.instance.ItemAdjust(x, y);
        else
            MapGenerator.instance.ItemRegenerate(x, y);
    }

    private void RandomGenerateEnemy()
    {
        if (enemies != null)
        {
            GameObject selectedEnemy = Instantiate(enemies[Random.Range(0, enemies.Count)]);
            selectedEnemy.gameObject.transform.parent = this.transform;
        }
    }

    private void EnemyGenerateLogic()
    {
        if (GetComponentInChildren<EnemyInterface>() != null)
            regenerateTimer = 0;
        else
        {
            regenerateTimer += Time.deltaTime;

            if (regenerateTimer >= regenerateTime)
                RandomGenerateEnemy();
        }
    }

    private void AvoidSpawnedInWater()
    {
        if (Physics2D.OverlapCircle(transform.position, .5f, LayerMask.GetMask("Water")))
        {
            edgeItem = true;
            Destroy(self);
        }
    }

    private void CalculateDestroyTime()
    {
        if (GetComponentInChildren<EnemyInterface>()!=null)
        {
            EnemyInterface enemy = GetComponentInChildren<EnemyInterface>();
            if (enemy.statsI.currentHealth.GetValue() == enemy.statsI.maxHealth.GetValue())//no reaction with player
                destroyTimer += Time.deltaTime;

            if (destroyTimer >= destroyTime)
                Destroy(self);
        }
        else
            destroyTime = 0;
    }

    private void ResetDestroyTime()
    {
        if (destroyTimer != 0)
            destroyTimer = 0;
    }

    private void ActivateEnemyLogic() 
    {
        if (GetComponentInChildren<EnemyInterface>() != null)
        {
            if (NPCManager.instance.isPeace)
                GetComponentInChildren<EnemyInterface>().selfI.SetActive(false);
            else
                GetComponentInChildren<EnemyInterface>().selfI.SetActive(true);
        }
    }
}
