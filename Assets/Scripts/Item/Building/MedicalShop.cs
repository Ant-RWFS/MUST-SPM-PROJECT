using UnityEngine;

[CreateAssetMenu(fileName = "MedicalShop", menuName = "Inventory/Building/MedicalShop")]
public class MedicalShop : Item
{
    public override void Use()
    {
        Debug.Log("you can buy drug here");
    }
}