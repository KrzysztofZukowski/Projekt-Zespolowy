using UnityEngine;

public class GameData
{
    public int Score { get; set; }
    public int PointsPerClick { get; set; } = 1;
    public AutoClicker AutoClicker { get; set; } = new AutoClicker(0, 5f, 50);

    public void Save()
    {
        PlayerPrefs.SetInt("Score", Score);
        PlayerPrefs.SetInt("PointsPerClick", PointsPerClick);
        if (AutoClicker != null)
        {
            PlayerPrefs.SetInt("AutoClickLevel", AutoClicker.Level);
            PlayerPrefs.SetInt("AutoClickCost", AutoClicker.UpgradeCost);
            PlayerPrefs.SetFloat("AutoClickInterval", AutoClicker.Interval);
        }
        PlayerPrefs.Save();
    }

    public void Load()
    {
        Score = PlayerPrefs.GetInt("Score", 0);
        PointsPerClick = PlayerPrefs.GetInt("PointsPerClick", 1);
        if (PointsPerClick <= 0) PointsPerClick = 1; // Zabezpieczenie przed błędem z zapisem 0

        int level = PlayerPrefs.GetInt("AutoClickLevel", 0);
        int cost = PlayerPrefs.GetInt("AutoClickCost", 50);
        float interval = PlayerPrefs.GetFloat("AutoClickInterval", 5.0f);
        AutoClicker = new AutoClicker(level, interval, cost);
        
        Debug.Log($"[GameData] Wczytano grę! Punkty: {Score}, Za klik: {PointsPerClick}");
    }
}
