using System.Collections.Generic;
using UnityEditor.AddressableAssets.Settings;
using UnityEngine;
using UnityEngine.AddressableAssets;

public class AddressLoadControl : Singleton<AddressLoadControl>
{
    [SerializeField] private List<ScriptableObject> loadedObjects = new List<ScriptableObject>();

    public ScriptableObject[] LoadAssets(AddressableAssetGroup group)
    {
        foreach(AddressableAssetEntry entry in group.entries)
        {
            Debug.Log(entry.address);
        }

        return null;
    }
}
