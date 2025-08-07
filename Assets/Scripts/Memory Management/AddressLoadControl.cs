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
    public async Awaitable LoadAssetsAsync(AssetLabelReference group, Action<ScriptableObject> individualItemCallback)
    {
        IList<IResourceLocation> locations = await Addressables.LoadResourceLocationsAsync(group).Task;
        
        foreach (IResourceLocation location in locations)
        {
            var handle = Addressables.LoadAssetAsync<ScriptableObject>(location);
            await handle.Task;
           

            if (handle.Status == UnityEngine.ResourceManagement.AsyncOperations.AsyncOperationStatus.Succeeded)
            {
                individualItemCallback.Invoke(handle.Result);
            }
            

            Addressables.Release(handle);
        }

    }

    private void OnDestroy()
    {

    }
}
