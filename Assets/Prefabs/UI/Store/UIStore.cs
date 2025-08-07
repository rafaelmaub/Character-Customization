using GameEconomy;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class UIStore : MonoBehaviour
{
    [SerializeField] private UIEquipsDisplay equipDisplay;
    [SerializeField] private Button buyButton;

    private UIEquipButton selectedEquipButton;

    public UnityEvent<EquipData> OnEquipHighlighted;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        equipDisplay.OnAnyEquipSelected += SwitchSelectedEquipBtn;
    }



    private void SwitchSelectedEquipBtn(UIEquipButton btn)
    {
        //Restore character display
        selectedEquipButton = btn;
        
        if (btn != null)
        {
            buyButton.gameObject.SetActive(true);
            buyButton.interactable = CurrencyManager.CurrentCoin >= btn.LinkedEquip.Value;

            OnEquipHighlighted.Invoke(btn.LinkedEquip);
        }
        else
        {
            buyButton.gameObject.SetActive(false);
        }
    }
}
