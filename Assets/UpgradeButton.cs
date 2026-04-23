using UnityEngine;
using TMPro;

public class UpgradeButton : MonoBehaviour
{
    public ClickerManager manager; // Przeci¹gnij tu GameManager
    public int bonus;
    public int cost;
    public string upgradeName;

    public TMP_Text buttonText;

    void Start()
    {
        UpdateUI();
    }

    public void OnButtonClick()
    {
        if (manager.score >= cost)
        {
            manager.score -= cost;
            manager.pointsPerClick += bonus;

            // Opcjonalnie: zwiêksz koszt po zakupie
            cost = (int)(cost * 1.6f);

            manager.UpdateUI(); // Odœwie¿ punkty g³ówne
            UpdateUI();         // Odœwie¿ tekst na tym przycisku
        }
    }

    void UpdateUI()
    {
        buttonText.text = upgradeName + " (+" + bonus + ")\nKoszt: " + cost;
    }
}