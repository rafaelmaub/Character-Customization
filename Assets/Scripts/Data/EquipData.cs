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
    [SerializeField] protected EquipVisual visual;

    [Header("Debug")]
    [SerializeField] private string _id;
    [SerializeField] private bool _hideItem;

    public string ID { get => _id; set => SetID(value); }
    public bool HideItem => _hideItem;


    public string EquipName => equipName;
    public int Value => equipValue;
    public Sprite Icon => equipIcon;

    public EquipVisual Visual => visual;


    public void SetID(string id)
    {
        _id = id;
    }
}

