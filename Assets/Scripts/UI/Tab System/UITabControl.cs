using UnityEngine;
using System.Collections.Generic;

/// <summary>
/// This class was designed to be compatible with any other possible uses in situations that demands multiple sections
/// Like a settings menu, that open different windows in different tabs
/// It's generic so it can be used to display other <typeparamref name="T"/> data 
/// </summary>
/// <typeparam name="T"></typeparam>

public abstract class UITabControl<T> : MonoBehaviour
{
    [SerializeField] protected T[] allTabs;
    [SerializeField] protected Transform tabsHolder;
    [SerializeField] protected UITabButton tabButtonPrefab;

    protected List<UITabButton> spawnedTabs = new List<UITabButton>();


    protected abstract void SpawnTabs();

    protected virtual void CleanTabs()
    {
        foreach (UITabButton btn in spawnedTabs)
        {
            Destroy(btn.gameObject);
        }

        spawnedTabs.Clear();
    }

    protected UITabButton SpawnTabInHolder()
    {
        UITabButton spawnedTabBtn = Instantiate(tabButtonPrefab, tabsHolder);
        spawnedTabs.Add(spawnedTabBtn);
        return spawnedTabBtn;
    }


}
