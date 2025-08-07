using System;

public class UIEquipsTabsControl : UITabControl<ListData>
{
    public Action<ListData> OnTabSelected;

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

        ListData listSelected = allTabs[index];
        OnTabSelected?.Invoke(listSelected);
    }




}
