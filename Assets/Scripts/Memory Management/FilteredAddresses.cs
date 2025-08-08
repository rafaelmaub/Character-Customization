using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;

[System.Serializable]
public class FilteredAddresses
{
    [SerializeField] private string category;
    public AssetLabelReference filterLabel;
    public List<string> addresses;
}
