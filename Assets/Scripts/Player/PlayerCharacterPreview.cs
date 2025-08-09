using UnityEngine;

public class PlayerCharacterPreview : MonoBehaviour
{
    [SerializeField] private CharacterEquipper equipper;

    private void Start()
    {
        //Apply a small delay to get correct data from PlayerData
        //GameObjects could be separated in scenes so it makes sure player data is already loaded
        Invoke("RestoreCurrentEquipment", 1f);
    }

    public void TryEquipment(EquipData equip)
    {
        equipper.ChangeEquipOnBodyPart(equip);
    }

    public void RestoreCurrentEquipment()
    {
        //Get from PlayerData equipment
        foreach(EquipData equip in PlayerData.Instance.CurrentEquips)
        {
            equipper.ChangeEquipOnBodyPart(equip);
        }
    }
}
