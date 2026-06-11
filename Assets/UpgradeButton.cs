using UnityEngine;
using TMPro;

public class UpgradeButton : MonoBehaviour
{
    public ClickerManager manager; // Przeciągnij tu GameManager
    public int bonus;
    public int cost;
    public string upgradeName;

    public TMP_Text buttonText;

    void Start()
    {
        cost = PlayerPrefs.GetInt("Cost_" + upgradeName, cost);
        UpdateUI();
    }

    public void OnButtonClick()
    {
        if (manager.gameData.Score >= cost)
        {
            manager.gameData.Score -= cost;
            manager.gameData.PointsPerClick += bonus;

            // Opcjonalnie: zwiększ koszt po zakupie
            cost = (int)(cost * 1.6f);

            manager.uiManager.UpdateScore(manager.gameData.Score); // Odśwież punkty główne
            UpdateUI();         // Odśwież tekst na tym przycisku
            manager.gameData.Save(); // Zapisz stan gry po zakupie
        }
    }

    void UpdateUI()
    {
        buttonText.text = upgradeName + " (+" + bonus + ")\nKoszt: " + cost;
    }
}
