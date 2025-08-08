using UnityEngine;
using UnityEditor;
using System.IO;
using System.Linq;
using System;

public class DatabaseManager
{
    [MenuItem("Tools/Rebuild Items ID")]
    public static void AssignIDsToScriptableObjects()
    {
        string folderPath = EquipmentUtils.Source_Path;

        if (!Directory.Exists(folderPath))
        {
            Debug.LogError($"Folder not found: {folderPath}");
            return;
        }

        string[] guids = AssetDatabase.FindAssets("t:ScriptableObject", new[] { folderPath });

        for(int i = 0; i < guids.Length; i++)
        {
            string assetPath = AssetDatabase.GUIDToAssetPath(guids[i]);
            ScriptableObject so = AssetDatabase.LoadAssetAtPath<ScriptableObject>(assetPath);

            if(so is IIdentifier identifier)
            {
                assetPath = assetPath.Replace(folderPath, "").Replace(".asset", "");
                identifier.SetID(assetPath);
                //Debug.Log($"Assigned ID {i} to {so.name}");
                Debug.Log(assetPath);
            }

            
        }


        AssetDatabase.SaveAssets(); // Save all pending asset changes
        Debug.Log("ScriptableObject ID assignment complete.");
    }
}
