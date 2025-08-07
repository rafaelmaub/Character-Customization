using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIEquipsDisplay : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Transform parentContainer;
    [SerializeField] private UIEquipButton equipButtonPrefab;
    [SerializeField] private UIEquipsTabsControl tabControl;

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
        var asyncHandler = AddressLoadControl.Instance.LoadAssetsAsync(data.ListContent, SpawnEquipButton);
        //Display Load/Download Icon somewhere in canvas

        while(!asyncHandler.IsCompleted)
        {
            if(spawnedButtons.Count > 10)
            {
                Debug.Log("Reached limit, wait for another request");
                yield return new WaitUntil(() => asyncHandler.IsCompleted);
                //NOTE: I want to put some sort of pause for the async operation when it reaches a certain amount of items spawned
                //The system would wait for the player to scroll all the way down before continuing with the display
                //Probably use the scroll bar value as reference
            }


            yield return null;
            

        }

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
