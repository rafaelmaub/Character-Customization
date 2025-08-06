using UnityEditor.AddressableAssets.Settings;
using UnityEngine.AddressableAssets;

using UnityEngine;


public class UIEquipsTabsControl : UITabControl<ListData>
{

    private void Start()
    {
        SpawnTabs();
    }


    protected override void SpawnTabs()
    {
        foreach(ListData data in allTabs)
        {
            if (data != null)
            {
                UITabButton btnSpawned = SpawnTabInHolder();
                
                btnSpawned.SetTabIcon(data.ListIcon);
                btnSpawned.ConnectTabButton(index: spawnedTabs.Count - 1);

                btnSpawned.OnClickTabBtn += OpenTab;

            }
        }
    }

    public void OpenTab(int index)
    {
        Debug.Log("Opening tab " + allTabs[index].name);

        //Display items in the UI after clicking
        //Load addressables
    }

}
