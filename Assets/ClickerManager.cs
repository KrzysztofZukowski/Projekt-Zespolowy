using UnityEngine;
using System.Collections;

public class ClickerManager : MonoBehaviour
{
    public UIManager uiManager;
    public GameObject upgradesPanel;
    public UnityEngine.UI.Button mainClickButton;
    public GameObject researchButton;
    public ResearchManager researchManager;

    public GameData gameData = new GameData();

    void Awake()
    {
        gameData.Load();
    }

    void Start()
    {
        uiManager.UpdateScore(gameData.Score);
        uiManager.UpdateAutoClicker(gameData.AutoClicker);
        StartCoroutine(AutoClickLoop());
        researchManager.OnResearchChanged += UpdateResearchUI;
        UpdateResearchUI();
    }

    public void AddPoint()
    {
        gameData.Score += gameData.PointsPerClick;
        uiManager.UpdateScore(gameData.Score);
        gameData.Save();
    }

    public void BuyAutoClicker()
    {
        if (gameData.Score >= gameData.AutoClicker.UpgradeCost)
        {
            gameData.Score -= gameData.AutoClicker.UpgradeCost;
            gameData.AutoClicker.Upgrade();
            uiManager.UpdateAutoClicker(gameData.AutoClicker);
            uiManager.UpdateScore(gameData.Score);
            gameData.Save();
        }
    }

    IEnumerator AutoClickLoop()
    {
        while (true)
        {
            yield return new WaitForSeconds(gameData.AutoClicker.Interval);
            if (gameData.AutoClicker.Level > 0)
            {
                AddPoint();
            }
        }
    }

    public void ToggleUpgradesView(bool show)
    {
        upgradesPanel.SetActive(show);
        mainClickButton.interactable = !show;
    }

    private void UpdateResearchUI()
    {
        uiManager.UpdateResearchUI(researchManager);
    }
}
