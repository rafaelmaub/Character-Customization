using UnityEngine;

[CreateAssetMenu(fileName = "Equipment Data", menuName = "Scriptable Objects/Equipment")]
public class EquipData : ScriptableObject, IIdentifier
{
    [Header("Information")]
    [SerializeField] protected string itemName;
    [SerializeField] protected string itemDescription;
    [SerializeField] protected Sprite itemIcon;

    [Header("Economy")]
    [SerializeField] protected int itemValue;

    [Header("Visuals")]
    [SerializeField] private Mesh itemMesh;
    [SerializeField] private Material[] materials;

    [Header("Debug")]
    [SerializeField] private int _id;

    public int ID { get => _id; set => SetID(value); }

    public void SetID(int id)
    {
        _id = id;
    }
}
