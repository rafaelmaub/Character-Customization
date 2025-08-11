using UnityEngine;
using Unity.Netcode;
using System.Collections.Generic;

public class PlayerCharacterPreview : MonoBehaviour
{
    [SerializeField] private CharacterEquipper equipper;
    [SerializeField] private GameObject loadingIcon;

    private void Awake()
    {
        equipper.HideAllBodyParts(true);
        loadingIcon.SetActive(true);

        PlayerData.Instance.OnEquipsFullyLoaded += InitialLoadOfEquips;
    }


    private void Start()
    {
        //Apply a small delay to get correct data from PlayerData
        //GameObjects could be separated in scenes so it makes sure player data is already loaded
        //Invoke("RestoreCurrentEquipment", 3f);

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

    private void InitialLoadOfEquips()
    {
        equipper.HideAllBodyParts(false);
        loadingIcon.SetActive(false);
        RestoreCurrentEquipment();
        PlayerData.Instance.OnEquipsFullyLoaded -= InitialLoadOfEquips;
    }
}
