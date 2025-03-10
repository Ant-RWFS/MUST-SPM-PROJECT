using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public struct SpawnedItem
{
    public GameObject prefab;
    public float x;
    public float y;
    public int maxID;
}
public class ItemGenerator : MonoBehaviour
{
    public List<SpawnedItem> items;
    public List<SpawnedItem> spawnedItems;

    private void Awake()
    {
    }

    private void Start()
    {
      
    }

    private void Update()
    {

    }
    public void CleanSpanwedItems() => spawnedItems.Clear();
    public void UpdateSpawnedItems(int _index, float _x, float _y) => spawnedItems.Add(new SpawnedItem { prefab = items[_index].prefab, x = _x, y = _y });
    public GameObject IntantiateSpanwedItems(int _index, float _x, float _y) => Instantiate(spawnedItems[_index].prefab,new Vector3(_x, _y), Quaternion.identity, ItemManager.instance.itemTransform);
}
