using UnityEngine;
using TMPro;

public class ClickerManager : MonoBehaviour
{
    public int score = 0;
    public TMP_Text scoreText;

    // Funkcja wywo³ywana przy klikniêciu
    public void AddPoint()
    {
        score++;
        UpdateUI();
    }

    // Aktualizacja tekstu na ekranie
    void UpdateUI()
    {
        scoreText.text = "Punkty: " + score.ToString();
    }
}