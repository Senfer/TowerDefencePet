using System;
using UnityEngine;

public class PlayerWalletManager : MonoBehaviour
{
    public int InitialCurrencyAmount;
    public int CurrencyAmount { get; private set; }

    public event Action<int, int> CurrencyAmountChanged = delegate { };

    private void Awake()
    {
        CurrencyAmount = InitialCurrencyAmount;
    }

    public void IncreaseCurrency(int amount)
    {
        var previousAmount = CurrencyAmount;
        CurrencyAmount += amount;
        CurrencyAmountChanged(previousAmount, CurrencyAmount);
    }

    public void DecreaseCurrency(int amount)
    {
        var previousAmount = CurrencyAmount;
        CurrencyAmount -= amount;
        CurrencyAmountChanged(previousAmount, CurrencyAmount);
    }
}
