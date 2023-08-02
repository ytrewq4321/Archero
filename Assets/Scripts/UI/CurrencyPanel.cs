using UnityEngine;
using TMPro;

public class CurrencyPanel : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI currencyText;

    public void SetupCurrency(string currency)
    {
        currencyText.text = currency;
    }

    public void UpdateCurrency(string currency)
    {
        currencyText.text = currency;
    }
}
