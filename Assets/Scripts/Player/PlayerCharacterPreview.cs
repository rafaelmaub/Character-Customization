using UnityEngine;
using Unity.Netcode;

public class PlayerCharacterPreview : MonoBehaviour
{
    [SerializeField] private CharacterEquipper equipper;

    private void Start()
    {
        //Apply a small delay to get correct data from PlayerData
        //GameObjects could be separated in scenes so it makes sure player data is already loaded
        Invoke("RestoreCurrentEquipment", .5f);

        NetworkManager.Singleton.OnClientStarted += () => equipper.HideAllBodyParts(true);
        NetworkManager.Singleton.OnClientStopped += (host) => equipper.HideAllBodyParts(false);
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
