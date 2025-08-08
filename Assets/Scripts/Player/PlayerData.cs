using UnityEngine;
using System.Collections.Generic;
using UnityEngine.AddressableAssets;
using System.Linq;
using Newtonsoft.Json;
using System;
using System.Threading.Tasks;

/// <summary>
/// The PlayerData script works as some kind of local server
/// Storing data that ideally would be stored online, like account, items owned, items equipped
/// </summary>
/// 
public class PlayerData : Singleton<PlayerData>
{

    [SerializeField] private List<string> ownedEquips_Address = new List<string>();
    [SerializeField] private List<string> currentEquips_Address = new List<string>();

    [SerializeField] private List<EquipData> currentEquips = new List<EquipData>();


    public List<EquipData> CurrentEquips => currentEquips;
    public List<string> OwnedEquipIds => ownedEquips_Address;

    private const string OwnedDataKey = "OWNED_EQUIPS";
    private const string CurrentDataKey = "CURRENT_EQUIPS";


    protected override void Awake()
    {
        base.Awake();
        LoadData();
    }

    public void SaveData()
    {
        PlayerPrefs.SetString(OwnedDataKey, JsonConvert.SerializeObject(ownedEquips_Address));
        PlayerPrefs.SetString(CurrentDataKey, JsonConvert.SerializeObject(currentEquips_Address));
    }

    public void LoadData()
    {
        if(PlayerPrefs.GetString(OwnedDataKey, "") == "" || PlayerPrefs.GetString(CurrentDataKey, "") == "")
        {
            //NO DATA
            Debug.Log("No save found! Creating data...");
            CreateSaveData();
            SaveData();
        }
        else
        {
            Debug.Log("Loading data file...");

            ownedEquips_Address = JsonConvert.DeserializeObject<string[]>(PlayerPrefs.GetString(OwnedDataKey, "")).ToList();
            currentEquips_Address = JsonConvert.DeserializeObject<string[]>(PlayerPrefs.GetString(CurrentDataKey, "")).ToList();

            currentEquips.Clear();
            foreach(string currentEquipId in currentEquips_Address)
            {
                var awaitable = AddressLoadControl.Instance.LoadAssetAsync(EquipmentUtils.GetItemAddress(currentEquipId), (so) =>
                {
                    if (so is EquipData equip)
                    {
                        currentEquips.Add(equip);
                    }
                    else
                    {
                        Debug.LogError("There is an impostor in the save file");
                    }
                });
            }
        }


    }

    private void CreateSaveData()
    {
        foreach (EquipData current in CurrentEquips)
        {
            currentEquips_Address.Add(current.ID);
            ownedEquips_Address.Add(current.ID);
        }
    }

    public async Awaitable HasItemAsync(string ID, Action<bool> callback)
    {
        bool has = false;
        
        await Task.Run(() =>
        {
            foreach (string owned in ownedEquips_Address)
            {
                if (owned == ID)
                {
                    has = true;
                    break;
                }
            }
        });

        callback.Invoke(has);
    }

    public void AcquireNewItem(EquipData data)
    {
        ownedEquips_Address.Add(data.ID);

        EquipData oldItem = currentEquips.Find(x => x.Visual.EquipType == data.Visual.EquipType);
        CurrentEquips.Remove(oldItem);
        currentEquips.Add(data);

        currentEquips_Address.Clear();
        foreach (EquipData currentEquip in currentEquips)
        {
            currentEquips_Address.Add(currentEquip.ID);
        }

        SaveData();
    }

}
