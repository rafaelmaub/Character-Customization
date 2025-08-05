using UnityEngine;
using GameEconomy;
using TMPro;

public class UICurrencyDisplay : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI coinTextDisplay;
    private int CurrentCoins => CurrencyManager.CurrentCoin;

    private void Awake()
    {
        SetCoinsText();
        CurrencyManager.OnCoinsChanged += UpdateCoinsDisplay;
    }

    private void OnDestroy()
    {
        CurrencyManager.OnCoinsChanged -= UpdateCoinsDisplay;
    }

    private void UpdateCoinsDisplay(int current, int difference)
    {
        if (difference < 0)
        {
            //Negative effect
        }
        else if(difference > 0)
        {
            //Positive Effect
        }


        SetCoinsText();
    }

    private void SetCoinsText()
    {
        coinTextDisplay.text = CurrentCoins.ToString();
    }

}
