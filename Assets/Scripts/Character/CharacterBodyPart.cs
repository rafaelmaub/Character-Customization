using UnityEngine;

public class CharacterBodyPart : MonoBehaviour
{
    
    [SerializeField] private SkinnedMeshRenderer bodyRenderer;
    
    private CharacterEquipper equipper;

    [SerializeField] private EquipData currentData;

    private void Start()
    {
        ChangeEquipment(currentData);
    }

    public void ChangeEquipment(EquipData newData)
    {

        if(newData.Visual.EquipMesh != bodyRenderer.sharedMesh)
        {
            bodyRenderer.sharedMesh = newData.Visual.EquipMesh;
        }
        
        if( newData.Visual.Materials.Length > 0)
        {
            bodyRenderer.materials = newData.Visual.Materials;
        }
        


        currentData = newData;
    }

}


