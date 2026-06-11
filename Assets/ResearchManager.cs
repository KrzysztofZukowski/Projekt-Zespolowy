using UnityEngine;

public class ResearchManager : MonoBehaviour
{
    public int researchPoints = 0;
    public int researchPerInterval = 1;
    public float researchInterval = 10f;
    public int discountLevel = 0;
    public int researchGenLevel = 0;

    public int discountUpgradeCost = 10;
    public int researchGenUpgradeCost = 20;

    public float upgradeDiscount = 1.0f; // 1.0 = brak zni¿ki, 0.9 = 10% taniej

    public delegate void ResearchChanged();
    public event ResearchChanged OnResearchChanged;

    void Start()
    {
        InvokeRepeating(nameof(GenerateResearch), researchInterval, researchInterval);
    }

    void GenerateResearch()
    {
        researchPoints += researchPerInterval;
        OnResearchChanged?.Invoke();
    }

    public void BuyDiscountUpgrade()
    {
        if (researchPoints >= discountUpgradeCost)
        {
            researchPoints -= discountUpgradeCost;
            discountLevel++;
            upgradeDiscount = Mathf.Max(0.5f, upgradeDiscount - 0.1f); // max 50% zni¿ki
            discountUpgradeCost = Mathf.CeilToInt(discountUpgradeCost * 2.5f);
            OnResearchChanged?.Invoke();
        }
    }

    public void BuyResearchGenUpgrade()
    {
        if (researchPoints >= researchGenUpgradeCost)
        {
            researchPoints -= researchGenUpgradeCost;
            researchGenLevel++;
            researchPerInterval++;
            researchGenUpgradeCost = Mathf.CeilToInt(researchGenUpgradeCost * 2.5f);
            OnResearchChanged?.Invoke();
        }
    }
}
