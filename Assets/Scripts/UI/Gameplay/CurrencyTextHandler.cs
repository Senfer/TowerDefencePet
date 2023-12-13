using TMPro;
using UnityEngine;

public class CurrencyTextHandler : MonoBehaviour
{
    private PlayerWalletManager _walletManager;

    public TextMeshProUGUI Text;
    public string Format;

    void Start()
    {
        _walletManager = GameplayManager.Instance.WalletManager;
        Text = GetComponent<TextMeshProUGUI>();
        SetCurrencyText(_walletManager.CurrencyAmount);
        _walletManager.CurrencyAmountChanged += OnCurrencyChanged;
    }

    private void OnDestroy()
    {
        _walletManager.CurrencyAmountChanged -= OnCurrencyChanged;
    }

    private void OnCurrencyChanged(int previousValue, int value)
    {
        SetCurrencyText(value);
    }

    private void SetCurrencyText(int value)
    {
        Text.text = string.Format(Format, value.ToString());
    }
}
