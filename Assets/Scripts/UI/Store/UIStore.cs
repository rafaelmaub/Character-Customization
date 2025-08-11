using GameEconomy;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class UIStore : MonoBehaviour
{
    [SerializeField] private UIEquipsDisplay equipDisplay;  
    [SerializeField] private Button buyButton;

    private PlayerCharacterPreview CharacterPreview 
    { 
        get 
        {
            if(_characterPreview == null)
                _characterPreview = FindAnyObjectByType<PlayerCharacterPreview>();

            return _characterPreview;
        }
    }
    private PlayerCharacterPreview _characterPreview;
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
        CharacterPreview.RestoreCurrentEquipment();

        selectedEquipButton = btn;
        buyButton.gameObject.SetActive(false);

        if (btn != null)
        {
            if(!btn.Owned)
            {
                buyButton.gameObject.SetActive(true);
                buyButton.interactable = CurrencyManager.CurrentCoin >= btn.LinkedEquip.Value;
                
            }
            else
            {
                PlayerData.Instance.UseEquip(btn.LinkedEquip);
            }

            CharacterPreview.TryEquipment(btn.LinkedEquip);
            OnEquipHighlighted.Invoke(btn.LinkedEquip);
           
        }

    }

    public void BuyEquip()
    {
        CurrencyManager.ChangeCoins(-selectedEquipButton.LinkedEquip.Value);
        selectedEquipButton.SetButtonBougth();

        PlayerData.Instance.AcquireNewEquip(selectedEquipButton.LinkedEquip);
        buyButton.gameObject.SetActive(false);
    }


    public void UnloadAssets()
    {
        equipDisplay.CleanDisplay();

        AddressLoadControl.Instance.NotReleaseHandle(PlayerData.Instance.CurrentEquipIds);

        Resources.UnloadUnusedAssets();
    }
}
