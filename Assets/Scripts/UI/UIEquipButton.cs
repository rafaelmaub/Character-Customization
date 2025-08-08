using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
public class UIEquipButton : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Image usingIcon;
    [SerializeField] private Image equipIconDisplay;
    [SerializeField] private TextMeshProUGUI equipNameDisplay;
    [SerializeField] private TextMeshProUGUI equipPriceDisplay;

    protected EquipData equipment;

    public Action<UIEquipButton> OnEquipmentButtonClicked;
    public EquipData LinkedEquip => equipment;

    public void InitializeButton(EquipData equip)
    {
        equipment = equip;

        equipNameDisplay.text = equip.name;
        equipPriceDisplay.text = equip.Value.ToString();

        equipIconDisplay.sprite = equip.Icon;

    }

    public virtual void Equipment_BtnClicked()
    {
        Debug.Log("Accessing equipment " + equipment.EquipName);
        OnEquipmentButtonClicked?.Invoke(this);
    }

    public void SetButtonBougth()
    {
        equipPriceDisplay.text = "OWNED";
        equipPriceDisplay.color = Color.green;
        equipPriceDisplay.transform.GetChild(0)?.gameObject.SetActive(false);

    }

    public void SetButtonUsed(bool used)
    {
        usingIcon.enabled = used;
    }

}
