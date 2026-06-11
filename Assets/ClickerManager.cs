using UnityEngine;
using System.Collections;

public class ClickerManager : MonoBehaviour
{
    public UIManager uiManager;
    public GameObject upgradesPanel;
    public GameObject researchPanel;
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
        if (researchManager != null)
        {
            researchManager.OnResearchChanged += UpdateResearchUI;
            UpdateResearchUI();
        }
        else
        {
            Debug.LogError("ResearchManager jest nieprzypisany w ClickerManager!");
        }
    }

    public void AddPoint()
    {
        gameData.Score += gameData.PointsPerClick;
        uiManager.UpdateScore(gameData.Score);
        gameData.Save();
        Debug.Log($"[ClickerManager] Dodano punkt! Aktualny wynik: {gameData.Score}");
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

    public void ToggleResearchView(bool show)
    {
        researchPanel.SetActive(show);
        mainClickButton.interactable = !show;
    }

    private void UpdateResearchUI()
    {
        uiManager.UpdateResearchUI(researchManager);
    }
}
