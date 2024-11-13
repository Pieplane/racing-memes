using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CarUpgradeData", menuName = "CarUpgrade")]
public class CarUpgrade : ScriptableObject
{
    public List<UpgradeLevel> upgradeLevels;

    public UpgradeLevel GetUpgradeForLevel(int level)
    {
        return upgradeLevels.Find(upgrade => upgrade.level == level);
    }
}
