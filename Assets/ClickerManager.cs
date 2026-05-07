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

    void Awake()
    {
        LoadGame();
    }

    void Start()
    {
        UpdateUI();
        StartCoroutine(AutoClickLoop());
    }

    public void SaveGame()
    {
        PlayerPrefs.SetInt("Score", score);
        PlayerPrefs.SetInt("PointsPerClick", pointsPerClick);
        PlayerPrefs.SetInt("AutoClickLevel", autoClickLevel);
        PlayerPrefs.SetInt("AutoClickCost", autoClickUpgradeCost);
        PlayerPrefs.SetFloat("AutoClickInterval", autoClickInterval);

        PlayerPrefs.Save(); // Wymusza zapis na dysku
        Debug.Log("Gra zapisana!");
    }

    public void LoadGame()
    {
        // Drugi parametr w GetInt/GetFloat to wartoœæ domyœlna, jeœli zapis nie istnieje
        score = PlayerPrefs.GetInt("Score", 0);
        pointsPerClick = PlayerPrefs.GetInt("PointsPerClick", 1);
        autoClickLevel = PlayerPrefs.GetInt("AutoClickLevel", 0);
        autoClickUpgradeCost = PlayerPrefs.GetInt("AutoClickCost", 50);
        autoClickInterval = PlayerPrefs.GetFloat("AutoClickInterval", 5.0f);

        Debug.Log("Gra wczytana!");
    }

    public void AddPoint()
    {
        score += pointsPerClick;
        UpdateUI();
        SaveGame();
    }

    void OnApplicationQuit()
    {
        SaveGame();
    }

    // Wywo³ywane, gdy gracz zminimalizuje grê (np. odbierze telefon, wróci do menu telefonu)
    // To jest KLUCZOWE na Androidzie/iOS
    void OnApplicationFocus(bool hasFocus)
    {
        // Jeœli hasFocus jest false, oznacza to, ¿e gra w³aœnie zosta³a zminimalizowana
        if (!hasFocus)
        {
            SaveGame();
        }
    }

    // Opcjonalnie: Zapis co okreœlony czas (np. co 60 sekund) dla bezpieczeñstwa
    private float timer = 0;
    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= 60f)
        {
            SaveGame();
            timer = 0;
        }
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
            SaveGame();
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