using UnityEditor.AddressableAssets.Settings;
using UnityEngine;

[CreateAssetMenu(fileName = "Item List Data", menuName = "Scriptable Objects/List Data")]
public class ListData : ScriptableObject
{
    [SerializeField] private string listName;
    [SerializeField] private string listDescription;

    [SerializeField] private Sprite iconography;

    [SerializeField] private AddressableAssetGroup listContent;


    public string ListName => listName;
    public Sprite ListIcon => iconography;
    public AddressableAssetGroup ListContent => listContent;
}
