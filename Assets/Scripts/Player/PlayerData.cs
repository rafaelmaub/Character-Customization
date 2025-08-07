using UnityEngine;
using System.Collections.Generic;
/// <summary>
/// The PlayerData script works as some kind of local server
/// Storing data that ideally would be stored online, like account, items owned, items equipped
/// </summary>
/// 

public class PlayerData : Singleton<PlayerData>
{
    [SerializeField] private List<string> ownedEquips_Path;
    [SerializeField] private List<string> currentEquips;



}
