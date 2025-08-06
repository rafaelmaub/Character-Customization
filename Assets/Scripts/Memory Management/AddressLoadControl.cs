using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;

public class AddressLoadControl : Singleton<AddressLoadControl>
{
    [SerializeField] private List<ScriptableObject> loadedObjects = new List<ScriptableObject>();
    private Dictionary<string, ScriptableObject> loadedAddresses = new Dictionary<string, ScriptableObject>();
    public ScriptableObject[] LoadAssets(AssetLabelReference group)
    {

        Addressables.LoadAssetsAsync<ScriptableObject>(group, LoadedItemCallBack).Completed += (asset =>
        {
            
            //asset.Release();
        });

        return null;
    }

    private void LoadedItemCallBack(ScriptableObject so)
    {
        Debug.Log("Added " + so.name);
        loadedObjects.Add(so);


    }

    private void OnDestroy()
    {
        //foreach(ScriptableObject loadedObject in loadedObjects)
        //{
        //    Destroy(loadedObject);
        //}

        //loadedObjects.Clear();
    }
}
