using UnityEngine;

[CreateAssetMenu(fileName = "Equipment Data", menuName = "Scriptable Objects/Equipment")]
public class EquipData : ScriptableObject, IIdentifier
{

    [Header("Information")]
    [SerializeField] protected string equipName;
    [SerializeField] protected string equipDescription;
    [SerializeField] protected Sprite equipIcon;

    [Header("Economy")]
    [SerializeField] protected int equipValue;

    [Header("Visuals")]
    [SerializeField] private Mesh equipMesh;
    [SerializeField] private Material[] materials;
    [SerializeField] private bool customColor;

    [Header("Debug")]
    [SerializeField] private int _id;
    [SerializeField] private bool _hideItem;

    public int ID { get => _id; set => SetID(value); }
    public bool HideItem => _hideItem;

    public string EquipName => equipName;
    public int Value => equipValue;
    public Sprite Icon => equipIcon;

    public void SetID(int id)
    {
        _id = id;
    }
}
