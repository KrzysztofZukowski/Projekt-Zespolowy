using System.Collections;
using UnityEngine;

public class AutoClicker
{
    public int Level { get; private set; }
    public float Interval { get; private set; }
    public int UpgradeCost { get; private set; }

    public AutoClicker(int level, float interval, int upgradeCost)
    {
        Level = level;
        Interval = interval;
        UpgradeCost = upgradeCost;
    }

    public void Upgrade()
    {
        Level++;
        Interval = Mathf.Max(0.1f, Interval * 0.85f);
        UpgradeCost = (int)(UpgradeCost * 1.7f);
    }
}
