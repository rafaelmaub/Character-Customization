using UnityEngine;

[System.Serializable]
public class EquipVisual
{
    [SerializeField] private Mesh equipMesh;
    [SerializeField] private Material[] materials;
    [SerializeField] private bool customColor;
    [SerializeField] private EquipType equipType;

    public Mesh EquipMesh => equipMesh;
    public Material[] Materials => materials;
    public bool CustomColor => customColor;
    public EquipType EquipType => equipType;
}