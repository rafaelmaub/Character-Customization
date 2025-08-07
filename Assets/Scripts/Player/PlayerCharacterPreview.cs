using UnityEngine;

public class PlayerCharacterPreview : MonoBehaviour
{
    [SerializeField] private CharacterEquipper equipper;

    public void TryEquipment(EquipData equip)
    {
        equipper.ChangeEquipOnBodyPart(equip);
    }

    public void RestoreCurrentEquipment()
    {
        //Get from PlayerData equipment
        foreach(EquipData equip in PlayerData.Instance.currentEquips)
        {
            equipper.ChangeEquipOnBodyPart(equip);
        }
    }
}
