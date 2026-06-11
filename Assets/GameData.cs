using UnityEngine;

public class GameData
{
    public int Score { get; set; }
    public int PointsPerClick { get; set; }
    public AutoClicker AutoClicker { get; set; }

    public void Save()
    {
        PlayerPrefs.SetInt("Score", Score);
        PlayerPrefs.SetInt("PointsPerClick", PointsPerClick);
        PlayerPrefs.SetInt("AutoClickLevel", AutoClicker.Level);
        PlayerPrefs.SetInt("AutoClickCost", AutoClicker.UpgradeCost);
        PlayerPrefs.SetFloat("AutoClickInterval", AutoClicker.Interval);
        PlayerPrefs.Save();
    }

    public void Load()
    {
        Score = PlayerPrefs.GetInt("Score", 0);
        PointsPerClick = PlayerPrefs.GetInt("PointsPerClick", 1);
        int level = PlayerPrefs.GetInt("AutoClickLevel", 0);
        int cost = PlayerPrefs.GetInt("AutoClickCost", 50);
        float interval = PlayerPrefs.GetFloat("AutoClickInterval", 5.0f);
        AutoClicker = new AutoClicker(level, interval, cost);
    }
}
