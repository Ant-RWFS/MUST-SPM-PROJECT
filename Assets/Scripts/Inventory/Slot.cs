using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Slot : MonoBehaviour
{
    public Inventory inventory;
    public Item slotItem;
    public Image slotImage;
    public TextMeshProUGUI slotNum;

    public Slider slider; // Slider 组件
    public Image fillImage; // 填充区域的 Image 组件

    
    public int index;
    private Color greenColor = Color.green; // 0.8 以上
    private Color yellowColor = Color.yellow; // 0.4 到 0.8
    private Color redColor = Color.red; // 0 到 0.4

    void Start()
    {
        if (slotItem.itemType!=ItemType.Weapon)
        {
            slider.GameObject().SetActive(false);
        }
        Update();

    }

    private void Update()
    {
        if (inventory.durabilityDict.ContainsKey(index))
        {
            slider.value = inventory.durabilityDict[index]*0.01f;
            // Debug.Log(inventory.durabilityDict[index]);
            UpdateColor(slider.value);
        }
    }

    void UpdateColor(float value)
    {
        if (value >= 0.8f)
        {
            fillImage.color = greenColor; // 绿色
        }
        else if (value >= 0.4f)
        {
            fillImage.color = yellowColor; // 黄色
        }
        else
        {
            fillImage.color = redColor; // 红色
        }
    }


    public void ItemOnClicked()
    {
        InventoryManager.UpdateItemInfo(slotItem.itemInfo);
    }
}