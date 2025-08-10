using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class UIEquipsDisplay : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Transform parentContainer;
    [SerializeField] private UIEquipButton equipButtonPrefab;
    [SerializeField] private UIEquipsTabsControl tabControl;
    [SerializeField] private LoadAssetFilter displayFilter;

    private CancellationTokenSource cts;
    private List<UIEquipButton> spawnedButtons = new List<UIEquipButton>();

    public Action<UIEquipButton> OnAnyEquipSelected;



    private void Awake()
    {
        tabControl.OnTabSelected += OpenList;
    }

    private void OpenList(ListData data)
    {
        CleanDisplay();

        Debug.Log("Opening items " + data.name);

        StartCoroutine(LoadEquipsCoroutine(data));
    }

    private IEnumerator LoadEquipsCoroutine(ListData data)
    {
        if(cts != null)
        {
            cts.Cancel();
            cts.Dispose();
        }

        cts = new CancellationTokenSource();

        Awaitable latestAsyncHandler = AddressLoadControl.Instance.LoadAssetsAsync(data.ListContent, SpawnEquipButton, cts.Token);

        //Filter the display of items depending on an Enum
        //Could maybe be done with a more complex system of filters and conditions (Limited Edition equips, Owned, Not Owned, Rare/Common/Legendary equips)
        
        /*
        switch (displayFilter)
        {
            case LoadAssetFilter.PlayerOwned:
                asyncHandler = AddressLoadControl.Instance.LoadAssetsAsyncInList(data.ListContent, PlayerData.Instance.OwnedEquipIds, SpawnEquipButton);
                break;
            case LoadAssetFilter.NotPlayerOwned:
            default:
                asyncHandler = AddressLoadControl.Instance.LoadAssetsAsync(data.ListContent, SpawnEquipButton);
                break;
        }
        */

        //Display Load/Download Icon somewhere in canvas

        while(!latestAsyncHandler.IsCompleted)
        {
            if(spawnedButtons.Count > 10)
            {
                //Debug.Log("Reached limit, wait for another request");
                //yield return new WaitUntil(() => latestAsyncHandler.IsCompleted);
                //NOTE: I want to put some sort of pause for the async operation when it reaches a certain amount of items spawned
                //The system would wait for the player to scroll all the way down before continuing with the display
                //Probably use the scroll bar value as reference
            }


            yield return null;
        }

        latestAsyncHandler = null;
        //Hide Load Icon to indicate there are no more loading to be done
    }

    private void SpawnEquipButton(ScriptableObject item)
    {
        if(item is EquipData equip)
        {
            if (equip.HideItem) return;

            UIEquipButton newEquip = Instantiate(equipButtonPrefab, parentContainer);
            spawnedButtons.Add(newEquip);
            newEquip.InitializeButton(equip);
            newEquip.OnEquipmentButtonClicked += OnAnyEquipSelected;


            var awaitable = PlayerData.Instance.HasEquipAsync(equip.ID, (has) =>
            {
                if(has)
                {
                    newEquip.SetButtonBougth();
                }
                
            });
        }

    }

    private void CleanDisplay()
    {

        StopAllCoroutines();

        foreach (UIEquipButton btn in spawnedButtons)
        {
            btn.OnEquipmentButtonClicked -= OnAnyEquipSelected;
            Destroy(btn.gameObject);
        }
        

        spawnedButtons.Clear();
    }
}
