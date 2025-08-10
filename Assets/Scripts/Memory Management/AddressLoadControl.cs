using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.ResourceLocations;
using static UnityEngine.Rendering.VirtualTexturing.Debugging;

public class AddressLoadControl : Singleton<AddressLoadControl>
{
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


            Addressables.Release(handle);
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


        Addressables.Release(handle);
    }

}
