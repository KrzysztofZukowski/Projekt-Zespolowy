using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public TMP_Text scoreText;
    public TMP_Text autoClickText;

    // Dodaj nowe pola
    public TMP_Text researchPointsText;
    public TMP_Text discountUpgradeText;
    public TMP_Text researchGenUpgradeText;

    [Header("IAP")]
    public GameObject buyNoAdsButton;

    public void UpdateScore(int score)
    {
        scoreText.text =""+ score;
    }

    public void UpdateAutoClicker(AutoClicker autoClicker)
    {
        if (autoClicker.Level == 0)
            autoClickText.text = "Kup AutoClicker (Koszt: " + autoClicker.UpgradeCost + ")";
        else
            autoClickText.text = "AutoClicker Lvl " + autoClicker.Level +
                                 "\nKlika co: " + autoClicker.Interval.ToString("F2") + "s" +
                                 "\nKoszt: " + autoClicker.UpgradeCost;
    }

    // Nowe metody
    public void UpdateResearchUI(ResearchManager research)
    {
        researchPointsText.text = $"Punkty badań: {research.researchPoints}";
        discountUpgradeText.text = $"Zniżka na ulepszenia: {research.discountLevel * 10}%\nKoszt: {research.discountUpgradeCost}";
        researchGenUpgradeText.text = $"Generacja punktów badań: {research.researchPerInterval}/interwał\nKoszt: {research.researchGenUpgradeCost}";
    }

    public void UpdateIAPUI(bool adsRemoved)
    {
        if (buyNoAdsButton != null)
        {
            buyNoAdsButton.SetActive(!adsRemoved);
        }
    }
}

