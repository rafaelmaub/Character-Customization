using UnityEngine;

public class UIInventory : MonoBehaviour
{
    [SerializeField] private UIEquipsDisplay equipDisplay;
    private void Start()
    {
        equipDisplay.OnAnyEquipSelected += ChangeEquip;
        //Tell 'equipDisplay' to only display items that are in the inventory
    }


    private void ChangeEquip(UIEquipButton equip)
    {
        //set button checkmark
    }
}
