using UnityEngine;

public class CharacterBodyPart : MonoBehaviour
{
    
    [SerializeField] private SkinnedMeshRenderer bodyRenderer;
    [SerializeField] private EquipType equipCompatibility;
    
    [SerializeField] private EquipData currentData;

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


    public bool IsItemCompatible(EquipData data)
    {
        //NOTE: Maybe this method could be used for other compatibilities like gender, size, even level?
        return data.Visual.EquipType == equipCompatibility;
    }
}


