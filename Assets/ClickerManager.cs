using UnityEngine;
using TMPro;
using System.Collections;

public class ClickerManager : MonoBehaviour
{
    public int score = 0;
    public int pointsPerClick = 1;
    public TMP_Text scoreText;
    public GameObject upgradesPanel;
    public UnityEngine.UI.Button mainClickButton;

    [Header("AutoClicker Settings")]
    public float autoClickInterval = 10.0f; // Pocz¹tkowo klika co 5 sekund
    public int autoClickLevel = 0;
    public int autoClickUpgradeCost = 50;
    public TMP_Text autoClickText;

    void Start()
    {
        UpdateUI();
        StartCoroutine(AutoClickLoop());
    }

    public void AddPoint()
    {
        score += pointsPerClick;
        UpdateUI();
    }

    // Funkcja kupowania ulepszenia AutoClickera
    public void BuyAutoClicker()
    {
        if (score >= autoClickUpgradeCost)
        {
            score -= autoClickUpgradeCost;
            autoClickLevel++;

            // LOGIKA SZYBKOŒCI:
            // Skracamy czas oczekiwania o 15% przy ka¿dym poziomie.
            // Nigdy nie spadnie poni¿ej 0.1s (10 klikniêæ na sekundê).
            autoClickInterval = Mathf.Max(0.1f, autoClickInterval * 0.85f);

            autoClickUpgradeCost = (int)(autoClickUpgradeCost * 1.7f); // Koszt roœnie
            UpdateUI();
        }
    }

    IEnumerator AutoClickLoop()
    {
        while (true)
        {
            // Czeka tyle, ile wynosi aktualny interwa³
            yield return new WaitForSeconds(autoClickInterval);

            if (autoClickLevel > 0)
            {
                // Wywo³uje dok³adnie tê sam¹ funkcjê co gracz
                AddPoint();
            }
        }
    }

    public void ToggleUpgradesView(bool show)
    {
        upgradesPanel.SetActive(show);
        mainClickButton.interactable = !show;
    }

    public void UpdateUI()
    {
        scoreText.text = "Punkty: " + score;

        if (autoClickText != null)
        {
            if (autoClickLevel == 0)
                autoClickText.text = "Kup AutoClicker (Koszt: " + autoClickUpgradeCost + ")";
            else
                autoClickText.text = "AutoClicker Lvl " + autoClickLevel +
                                     "\nKlika co: " + autoClickInterval.ToString("F2") + "s" +
                                     "\nKoszt: " + autoClickUpgradeCost;
        }
    }
}