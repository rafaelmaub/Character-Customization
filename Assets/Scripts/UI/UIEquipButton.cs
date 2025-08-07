using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class UIEquipButton : MonoBehaviour
{
    [SerializeField] private Image equipIconDisplay;
    [SerializeField] private TextMeshProUGUI equipNameDisplay;
    [SerializeField] private TextMeshProUGUI equipPriceDisplay;

    protected EquipData equipment;



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
    }

}
