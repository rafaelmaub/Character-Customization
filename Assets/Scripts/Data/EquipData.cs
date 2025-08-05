using UnityEngine;

[CreateAssetMenu(fileName = "Equipment Data", menuName = "Scriptable Objects/Equipment")]
public class EquipData : ScriptableObject
{
    [SerializeField] protected string itemName;
    [SerializeField] protected string itemDescription;
    [SerializeField] protected Sprite itemIcon;

    [SerializeField] protected int itemValue;

    [SerializeField] private Mesh itemMesh;


}
