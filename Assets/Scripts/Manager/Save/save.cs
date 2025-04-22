using System;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Linq;

public class save : MonoBehaviour
{
    public Inventory inventory;     // 引用 Inventory
    public Player player;           // 引用 Player（提供位置）
    public PlayerStats playerStats; // 引用 PlayerStats（提供血量）
    public MapGenerator mapGenerator;
    public Motor motor;

    // 保存路径
    private string SavePath => $"{Application.persistentDataPath}/save.json";

    private void Awake()
    {
        // 确保单例（可选）
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        LoadAllData(); // 启动时加载所有数据
    }

    private void OnEnable()
    {
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.playModeStateChanged += OnPlayModeStateChanged;
        #else
        Application.quitting += SaveOnQuit;
        #endif
    }

    private void OnDisable()
    {
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.playModeStateChanged -= OnPlayModeStateChanged;
        #else
        Application.quitting -= SaveOnQuit;
        #endif
    }

    #if UNITY_EDITOR
    private void OnPlayModeStateChanged(UnityEditor.PlayModeStateChange state)
    {
        if (state == UnityEditor.PlayModeStateChange.ExitingPlayMode)
        {
            SaveAllData();
            Debug.Log("编辑器退出播放模式，存档数据已保存");
        }
    }
    #endif

    private void SaveOnQuit()
    {
        SaveAllData();
        Debug.Log("游戏退出，存档数据已保存");
    }

    // 保存所有数据（库存、玩家位置、玩家血量）
    public void SaveAllData()
    {
        try
        {
            // 检查引用
            if (inventory == null || player == null || playerStats == null)
            {
                Debug.LogError("SaveManager: inventory, player 或 playerStats 未分配");
                return;
            }

            // 收集库存数据
            var inventoryData = new InventorySaveData
            {
                durabilityEntries = inventory.durabilityDict.Select(kvp => new DurabilityEntry
                {
                    slotIndex = kvp.Key,
                    durability = kvp.Value
                }).ToList(),
                items = inventory.itemList
            };

            // 收集玩家数据（位置和血量）
            var playerData = new PlayerSaveData
            {
                health = playerStats.currentHealth,
                position = new Vector3Serializable(player.transform.position)
            };
            
            //收集摩托数据
            var motorData = new MotorSaveData
            {
                position = new Vector3Serializable(motor.transform.position)
            };
            
            //收集地图数据
            var mapData = new MapSaveData
            {
                seed = mapGenerator.seed,
                waterRatio = mapGenerator.map.ratio,
                lacunarity = mapGenerator.map.lacunarity,
                resourceDensity = mapGenerator.map.density
            };

            // 合并到全局存档
            var saveData = new GameSaveData
            {
                inventory = inventoryData,
                player = playerData,
                motor = motorData,
                map = mapData
            };

            // 转换为 JSON
            string json = JsonUtility.ToJson(saveData, true);
            if (string.IsNullOrEmpty(json) || json == "{}")
            {
                Debug.LogWarning("生成的 JSON 数据为空，检查 GameSaveData 结构");
                return;
            }

            // 确保目录存在
            string directory = Path.GetDirectoryName(SavePath);
            if (!Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
                Debug.Log($"创建保存目录: {directory}");
            }

            // 写入文件
            File.WriteAllText(SavePath, json);
            Debug.Log($"存档数据已保存到: {SavePath}\nJSON 内容:\n{json}");
        }
        catch (Exception ex)
        {
            Debug.LogError($"保存存档数据失败: {ex.Message}");
        }
    }

    // 加载所有数据
    public void LoadAllData()
    {
        try
        {
            if (File.Exists(SavePath))
            {
                string json = File.ReadAllText(SavePath);
                if (string.IsNullOrEmpty(json))
                {
                    Debug.LogWarning("save.json 文件为空，无法加载数据");
                    return;
                }

                GameSaveData saveData = JsonUtility.FromJson<GameSaveData>(json);
                if (saveData == null)
                {
                    Debug.LogWarning("加载的 JSON 数据无效，初始化为空");
                    return;
                }

                // 加载库存数据
                if (saveData.inventory != null && inventory != null)
                {
                    inventory.durabilityDict.Clear(); // 清空现有数据
                    foreach (var entry in saveData.inventory.durabilityEntries)
                    {
                        inventory.durabilityDict[entry.slotIndex] = entry.durability;
                        Debug.Log($"加载耐久: slotIndex={entry.slotIndex}, durability={entry.durability}");
                    }
                    inventory.itemList.Clear();
                    inventory.itemList.AddRange(saveData.inventory.items);
                    // inventory.OnInventoryChanged?.Invoke();

                    string dictContent = inventory.durabilityDict.Count > 0
                        ? string.Join(", ", inventory.durabilityDict.Select(kvp => $"[{kvp.Key}]: {kvp.Value}"))
                        : "空";
                    Debug.Log($"库存数据已加载，durabilityDict: {dictContent}, itemList Count: {inventory.itemList.Count}");
                }
                else
                {
                    Debug.LogWarning("saveData.inventory 或 inventory 为 null");
                }

                // 加载玩家数据
                if (saveData.player != null)
                {
                    if (playerStats != null)
                    {
                        playerStats.currentHealth = saveData.player.health;
                    }
                    if (player != null)
                    {
                        player.transform.position = saveData.player.position.ToVector3();
                    }
                    Debug.Log($"玩家数据已加载，health: {playerStats?.currentHealth}, position: {player?.transform.position}");
                }
                //加载摩托数据
                if (saveData.motor != null)
                {
                    motor.transform.position = saveData.motor.position.ToVector3();
                    Debug.Log($"摩托数据已加载， position: {motor?.transform.position}");
                }
                // 加载地图数据
                if (saveData.map != null && mapGenerator != null)
                {
                    Debug.Log($"地图数据已加载，seed: {mapGenerator.seed}, waterRatio: {mapGenerator.map.ratio}, lacunarity: {mapGenerator.map.lacunarity}, resourceDensity: {mapGenerator.map.density}");
                    mapGenerator.seed = saveData.map.seed;
                    mapGenerator.map.ratio = saveData.map.waterRatio;
                    mapGenerator.map.lacunarity = saveData.map.lacunarity;
                    mapGenerator.map.density = saveData.map.resourceDensity;
                    mapGenerator.InitMap(); // 重新初始化地图
                }
                else
                {
                    Debug.Log("地图错误");
                }
            }
            else
            {
                Debug.Log("没有找到 save.json 文件，使用默认值");
                if (mapGenerator != null)
                {
                    mapGenerator.InitMap();
                }
            }
        }
        catch (Exception ex)
        {
            // Debug.LogError($"加载存档数据失败: {ex.Message}");
        }
    }

    // 序列化类
    [Serializable]
    private class GameSaveData
    {
        public InventorySaveData inventory;
        public PlayerSaveData player;
        public MotorSaveData motor;
        public MapSaveData map;
    }

    [Serializable]
    private class InventorySaveData
    {
        public List<DurabilityEntry> durabilityEntries = new List<DurabilityEntry>();
        public List<Item> items = new List<Item>();
        public int heldAmount;
    }

    [Serializable]
    private class DurabilityEntry
    {
        public int slotIndex;
        public int durability;
        
    }

    [Serializable]
    private class PlayerSaveData
    {
        public Stat health; // 与 PlayerStats.health 一致
        public Vector3Serializable position;
    }
    
    [Serializable]
    private class MotorSaveData
    {
        public Vector3Serializable position;
    }
    
    [Serializable]
    private class MapSaveData
    {
        public int seed;
        public float waterRatio;
        public float lacunarity;
        public float resourceDensity;
    }

    [Serializable]
    private class Vector3Serializable
    {
        public float x, y, z;

        public Vector3Serializable(Vector3 v)
        {
            x = v.x;
            y = v.y;
            z = v.z;
        }

        public Vector3 ToVector3() => new Vector3(x, y, z);
    }
}