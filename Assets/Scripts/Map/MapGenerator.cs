using System;
using UnityEngine;
using UnityEngine.Tilemaps;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Collections.Concurrent;
using System.Linq;
using Unity.Mathematics;

public struct TileData
{
    public int x;
    public int y;
    public float offsetX;
    public float offsetY;
    public bool tileType;// False: Land; True: Water
    public int resourceType;
    public int resourceInitID;
    public bool resourceInstantiated;

    public bool IsSameLocation(int _x, int _y)
    {
        if (x == _x && y == _y)
            return true;
        else
            return false;
    }
}
public class MapGenerator : MonoBehaviour
{
    [Header("Tilemap")]
    public Tilemap landTilemap;
    public Tilemap waterTilemap;

    public TileBase landTile;
    public TileBase waterTile;

    public int radius;

    public int seed;
    public bool randomSeed;

    [Range(0, 1f)]
    public float lacunarity;

    [Range(0, 1f)]
    public float waterRatio;

    [Range(0, 1f)]
    public float resourceDensity;

    [Header("Detector")]
    [SerializeField] private Transform mapDetector;

    private Transform playerTransform;
    private ItemGenerator itemGenerator;

    private List<TileData> mapData;
    private float randomOffset;

    private bool isPlayerDetected;

    private void Awake()
    {
        itemGenerator = GetComponent<ItemGenerator>();
    }
    private void Start()
    {
        InitMap();

    }

    private void Update()
    {
        UpdateMap();
    }

    #region Initialization
    private void InitMap()
    {
        InitPlayerTransform();

        InitMapData();

        UpdateTilemapData();
    }

    private void InitPlayerTransform() => playerTransform = PlayerManager.instance.playerTransform;

    private void InitMapData()
    {
        if (!randomSeed)
            seed = Time.time.GetHashCode();

        UnityEngine.Random.InitState(seed);

        mapData = new List<TileData>();

        isPlayerDetected = false;

        randomOffset = UnityEngine.Random.Range(-100000, 100000);
    }

    private void CleanTilemap()// High performance cost before set tiles
    {
        landTilemap.ClearAllTiles();
        waterTilemap.ClearAllTiles();
    }
    #endregion

    #region Update
    private void UpdateMap()
    {
        Collider2D[] detector = Physics2D.OverlapBoxAll(mapDetector.position, new Vector2(.25f, .25f), 0);

        isPlayerDetected = false;

        foreach (var hit in detector)
        {
            if (hit.GetComponent<Player>())
                isPlayerDetected = true;
        }

        if (!isPlayerDetected)
        {
            itemGenerator.CleanSpanwedItems();
            UpdateTilemapData();

            mapDetector.position = new Vector3Int((int)playerTransform.position.x, (int)playerTransform.position.y);
        }
    }

    #region Hash
    private uint MurmurHash(int x, int y, int seed)
    {
        uint hash = (uint)seed;
        uint key = (uint)(x ^ y);

        hash ^= key;
        hash ^= hash >> 14;
        hash ^= hash << 7;
        hash ^= hash >> 19;

        return hash;
    }

    private float HashToOffset(int x, int y, int seed, float range)
    {
        uint hashValue = MurmurHash(x, y, seed);
        return (hashValue % 1000) / 1000.0f * 2 * range - range;
    }
    #endregion


    private void UpdateTilemapData()
    {
        List<TileData> updatedMapData = new List<TileData>();

        int playerX = (int)playerTransform.position.x;
        int playerY = (int)playerTransform.position.y;

        for (int x = -radius; x <= radius; x++)
        {
            for (int y = -radius; y <= radius; y++)
            {
                if ((x * x + y * y) <= (radius * radius))
                {
                    int resourceType, resourceInitID;
                    bool tileType, resourceInstantiated;
                    float offsetX, offsetY, mapNoiseValue, resourceNoise, resourceTypeNoise;

                    mapNoiseValue = Mathf.PerlinNoise((playerX + x) * lacunarity + randomOffset, (playerY + y) * lacunarity + randomOffset);
                    tileType = mapNoiseValue < waterRatio;

                    resourceNoise = Mathf.PerlinNoise((playerX + x) * lacunarity + randomOffset + 10000, (playerY + y) * lacunarity + randomOffset + 10000);

                    if (!tileType && resourceNoise < resourceDensity)
                    {
                        resourceTypeNoise = Mathf.PerlinNoise((playerX + x) * lacunarity + randomOffset + 20000, (playerY + y) * lacunarity + randomOffset + 20000);

                        offsetX = HashToOffset(playerX + x, playerY + y, seed, 0.4f);
                        offsetY = HashToOffset(playerX + x, playerY + y, seed, 0.4f);

                        if (resourceTypeNoise < 0.5f)
                            resourceType = 0;
                        else
                            resourceType = 1;

                        resourceInitID = (int)(MurmurHash(playerX + x, playerY + y, seed + resourceType) % itemGenerator.items[resourceType].maxID);

                        resourceInstantiated = IsItemInstantiated(playerX + x, playerY + y);

                        itemGenerator.UpdateSpawnedItems(resourceType, playerX + x + offsetX, playerY + y + offsetY);
                    }
                    else
                    {
                        offsetX = 0;
                        offsetY = 0;
                        resourceType = -1;
                        resourceInitID = 0;
                        resourceInstantiated = false;
                    }

                    updatedMapData.Add(new TileData
                    {
                        x = playerX + x,
                        y = playerY + y,
                        offsetX = offsetX,
                        offsetY = offsetY,
                        tileType = tileType,
                        resourceType = resourceType,
                        resourceInstantiated = resourceInstantiated,
                        resourceInitID = resourceInitID
                    });
                }
            }
        }

        UpdateItemData();
        UpdateMapData(updatedMapData);

        foreach (var tileData in updatedMapData)
        {
            if (!tileData.tileType)
                landTilemap.SetTile(new Vector3Int(tileData.x, tileData.y), landTile);
            else
                waterTilemap.SetTile(new Vector3Int(tileData.x, tileData.y), waterTile);
        }
    }

    private void UpdateMapData(List<TileData> _addList)
    {
        ConcurrentDictionary<(int, int), TileData> mapDataDict = new ConcurrentDictionary<(int, int), TileData>();

        foreach (var tileData in mapData)
            mapDataDict[(tileData.x, tileData.y)] = tileData;

        Parallel.ForEach(_addList, tileData => { mapDataDict.TryRemove((tileData.x, tileData.y), out _); });

        Parallel.ForEach(_addList, tileData => { mapDataDict[(tileData.x, tileData.y)] = tileData; });

        mapData = new List<TileData>(mapDataDict.Values);
    }
    private void UpdateItemData()
    {
        List<TileData> updatedMapData = new List<TileData>();

        foreach (var tileData in mapData)
        {
            if (tileData.resourceType != -1 && tileData.resourceInstantiated && IsNeighbourLand(tileData.x, tileData.y))
            {
                GameObject item = itemGenerator.IntantiateSpanwedItems(tileData.resourceType, tileData.x + tileData.offsetX, tileData.y + tileData.offsetY);
                item.GetComponent<Resource>().InitId = tileData.resourceInitID;
            }

            updatedMapData.Add(new TileData { x = tileData.x, y = tileData.y, resourceType = tileData.resourceType, resourceInstantiated = false, resourceInitID = tileData.resourceInitID });
        }

        mapData = updatedMapData;
    }
    private bool IsItemInstantiated(int _x, int _y)
    {
        foreach (var tileData in mapData)
            if (tileData.IsSameLocation(_x, _y))
                return false;

        return true;
    }

    private bool IsNeighbourLand(int _x, int _y)
    {
        int[] offsetX = { -1, 1, 0, 0, -1, -1, 1, 1 };
        int[] offsetY = { 0, 0, -1, 1, -1, 1, -1, 1 };

        ConcurrentBag<bool> results = new ConcurrentBag<bool>();

        Parallel.ForEach(offsetX.Zip(offsetY, (x, y) => (x, y)), (offset) =>
        {
            int nx = _x + offset.Item1;
            int ny = _y + offset.Item2;

            bool isNeighbourLand = true;

            foreach (var tileData in mapData)
            {
                if (tileData.IsSameLocation(nx, ny))
                {
                    isNeighbourLand = !tileData.tileType;
                    break;
                }
            }

            results.Add(isNeighbourLand);
        });

        return results.All(result => result);
    }
    #endregion
}