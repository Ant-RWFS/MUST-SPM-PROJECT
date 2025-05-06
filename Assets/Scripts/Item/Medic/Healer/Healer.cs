using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Healer", menuName = "Inventory/Medic/Healer")]
public class Healer : Item
{
    public int heal;
    public List<int> test;
    public override void Use()
    {
        PlayerStats stats = PlayerManager.instance.player.stats;

        if (stats.currentHealth.GetValue() < stats.GetMaxHealthValue())
        {
            if (stats.currentHealth.GetValue() + heal <= stats.GetMaxHealthValue())
                stats.currentHealth.SetValue(stats.currentHealth.GetValue() + heal);
            else
                stats.currentHealth.SetValue(stats.GetMaxHealthValue());
        }
    }
}
