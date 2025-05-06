using UnityEngine;

[CreateAssetMenu(fileName = "Gun", menuName = "Inventory/Weapon/Gun")]
public class Gun : Item
{
    public RuntimeAnimatorController ac;
    public float firingRatePerSec;
    public GameObject bullet;
    public bool isEquipped = false;
    
    
    public override void Use()
    {
        isEquipped = !isEquipped;
    }
}