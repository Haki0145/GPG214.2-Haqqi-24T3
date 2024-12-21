using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.ComponentModel.Composition.Primitives;
using System.IO;

[CustomEditor(typeof(MetadatScriptableObject))]
public class MetadataScriptableObjectEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        MetadatScriptableObject metadata =(MetadatScriptableObject)target;

        if(GUILayout.Button("Export to Json"))
        {
            
            ExportToJson(metadata);
            EditorUtility.SetDirty(metadata);
        }
    }

    private void ExportToJson(MetadatScriptableObject metadata)
    {
        MetadataFile metadataFile = new MetadataFile
        {
            version = metadata.version,
            fileLink = metadata.associatedFileLink
        };

        string json = JsonUtility.ToJson(metadataFile, true);

        Directory.CreateDirectory(Application.streamingAssetsPath);

        File.WriteAllText(metadata.LocalMetadatFilePath, json);

        Debug.Log($"Metadata Exported to json at:{metadata.LocalMetadatFilePath} ");



    }
}
