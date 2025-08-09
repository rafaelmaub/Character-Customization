using System.Collections.Generic;
using Unity.Collections;
using Unity.Netcode;
using UnityEngine;

public class NetworkCharacterInfo : NetworkBehaviour
{
    public NetworkList<FixedString64Bytes> equips = new NetworkList<FixedString64Bytes>(readPerm: NetworkVariableReadPermission.Everyone, writePerm: NetworkVariableWritePermission.Server);
    
    [SerializeField] private List<EquipData> loadedEquipData = new List<EquipData>();
    [SerializeField] private CharacterEquipper equipper;
    public override void OnNetworkSpawn()
    {
        base.OnNetworkSpawn();

        //If at any point the server decides to re-update that list, every player will also update
        equips.OnListChanged += Equips_OnListChanged;

        if (IsLocalPlayer)
        {
            foreach(string id in PlayerData.Instance.CurrentEquipIds)
            {
                LoadUserItemFromServerRPC(id);
            }
            
        }

        if(IsServer)
        {
            transform.position = Random.onUnitSphere * 3f;
        }

    }

    private void Equips_OnListChanged(NetworkListEvent<FixedString64Bytes> changeEvent)
    {
        var awaitable = AddressLoadControl.Instance.LoadAssetAsync(EquipmentUtils.GetItemAddress(changeEvent.Value.ConvertToString()), so => 
        {
            if(so is EquipData equip)
            {
                EquipData oldEquipData = loadedEquipData.Find(item => item.Visual.EquipType == equip.Visual.EquipType);
                if (oldEquipData)
                {
                    loadedEquipData.Remove(oldEquipData);                   
                }

                loadedEquipData.Add(equip);
                equipper.ChangeEquipOnBodyPart(equip);
            }
        });
    }



    /// <summary>
    /// Ideally, the player would request the server for the information stored in the account
    /// Since we don't have a database for user information, the data is stored locally in their machine
    /// </summary>
    [Rpc(SendTo.Server)]
    public void LoadUserItemFromServerRPC(FixedString64Bytes playerDataEquip)
    {
        //Get Rpc Requester ID and search data in the database
        //Place data inside Network List

        //Since we don't have all of that, the player will pass a list from PlayerData and the server will write on the networklist
        equips.Add(playerDataEquip);

    }
}
