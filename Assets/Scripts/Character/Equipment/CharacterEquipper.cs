using UnityEngine;

public class CharacterEquipper : MonoBehaviour
{
    [SerializeField] private CharacterBodyPart[] bodyParts;

    public void ChangeEquipOnBodyPart(EquipData data)
    {
        //Either this or a switch with body parts individually referenced, but a Switch statement is less open to code expansion
        foreach (CharacterBodyPart part in bodyParts) 
        {
            if(part.IsItemCompatible(data))
            {
                part.ChangeEquipment(data);
                return;
            }
        }
    }
}
