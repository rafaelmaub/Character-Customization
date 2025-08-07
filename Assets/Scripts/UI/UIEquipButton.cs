using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class UIEquipButton : MonoBehaviour
{
    [SerializeField] private Image itemIconDisplay;
    [SerializeField] private TextMeshProUGUI itemNameDisplay;
    [SerializeField] private TextMeshProUGUI itemPriceDisplay;
    private EquipData equipment;



    public void InitializeButton(EquipData equip)
    {
        equipment = equip;

        itemNameDisplay.text = equip.name;
        itemPriceDisplay.text = equip.Value.ToString();

        itemIconDisplay.sprite = equip.Icon;
    }

}
