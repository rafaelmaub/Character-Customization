using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceLocations;
using static UnityEngine.Rendering.VirtualTexturing.Debugging;

public class AddressLoadControl : Singleton<AddressLoadControl>
{
    List<AsyncOperationHandle> allHandles = new List<AsyncOperationHandle>();

    public async Awaitable LoadAssetsAsync(AssetLabelReference group, Action<ScriptableObject> individualItemCallback, CancellationToken token = default)
    {
        IList<IResourceLocation> locations = await Addressables.LoadResourceLocationsAsync(group).Task;

        foreach (IResourceLocation location in locations)
        {
            //Filter here?
            var handle = Addressables.LoadAssetAsync<ScriptableObject>(location);
            await handle.Task;

            if(token.IsCancellationRequested)
            {
                Addressables.Release(handle);
                break;
            }

            if (handle.Status == UnityEngine.ResourceManagement.AsyncOperations.AsyncOperationStatus.Succeeded)
            {
                individualItemCallback.Invoke(handle.Result);
            }

            allHandles.Add(handle);

            //Addressables.Release(handle);
        }

    }


    public async Awaitable LoadAssetAsync(string address, Action<ScriptableObject> individualItemCallback, CancellationToken token = default)
    {
        var handle = Addressables.LoadAssetAsync<ScriptableObject>(address);
        await handle.Task;


        if (token.IsCancellationRequested)
        {
            Addressables.Release(handle);
            return;
        }

        if (handle.Status == UnityEngine.ResourceManagement.AsyncOperations.AsyncOperationStatus.Succeeded)
        {
            individualItemCallback.Invoke(handle.Result);
        }

        allHandles.Add(handle);

        //Addressables.Release(handle);
    }

    public void ReleaseHandle(ScriptableObject so)
    {
        
    }

    public void ReleaseHandle(string id)
    {
        int indexToRemove = -1;

        for (int i = 0; i < allHandles.Count; i++)
        {
            var handle = allHandles[i];
            if (handle.IsValid() && handle.Task.IsCompletedSuccessfully && handle.Task.Result is ScriptableObject so && so is EquipData equip && equip.ID == id)
            {
                indexToRemove = i;
                Addressables.Release(handle);
                break;
            }
        }

        if(indexToRemove >= 0) allHandles.RemoveAt(indexToRemove);
    }

    public void NotReleaseHandle(List<string> idsNotToRelease)
    {
        List<int> indexesToRemove = new List<int>();

        for (int i = 0; i < allHandles.Count; i++)
        {
            var handle = allHandles[i];
            if (handle.IsValid() && handle.Task.IsCompletedSuccessfully && handle.Task.Result is ScriptableObject so && so is EquipData equip && !idsNotToRelease.Contains(equip.ID))
            {
                indexesToRemove.Add(i);

                Addressables.Release(handle);

            }
        }

        foreach (int index in indexesToRemove)
        {
            if(index < allHandles.Count) allHandles.RemoveAt(index);


        }

#if !UNITY_EDITOR
        Addressables.CleanBundleCache();
#endif
    }

}
