using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;


[CreateAssetMenu(fileName = "FileMetaData", menuName = "Metadata/File Metadata")]
public class MetadatScriptableObject : ScriptableObject
{

    [Header("Meta Data")]
    public string filename = "file metadata";
    public string extension = ".json";
    public string metadataFileLink;
    [Header("File Data")]
    public string associatedFileLink;
    public string associatedFileExtension;
    public string version;

    public string LocalMetadatFilePath => Path.Combine(Application.streamingAssetsPath, filename + extension);
    public string LocalFilePath => Path.Combine(Application.streamingAssetsPath, filename + associatedFileExtension);
    public string DirectMetadataDownloadLink => GoogleDriveHelper.ConvertToDirectDownloadLink(metadataFileLink);
    public string RemoteFileDownloadLink { get {return GoogleDriveHelper.ConvertToDirectDownloadLink(associatedFileLink); } set { } }

    public void SetupLocalMetaData()
    {
        version = string.Empty;

        if (File.Exists(LocalMetadatFilePath))
        {
            string localMetaDataContent = File.ReadAllText(LocalMetadatFilePath).ToString();
            MetadataFile localMetaData = JsonUtility.FromJson<MetadataFile>(localMetaDataContent);

            if (localMetaData != null)
            {
                version = localMetaData.version;
            }
            File.Delete(LocalMetadatFilePath);
        }
        else
        {
            version = "-1";
        }
    }

    public bool FileNeedsUpdating()
    {
        Debug.Log("Checking local metadata file at path" + LocalMetadatFilePath);

        if (!File.Exists(LocalMetadatFilePath))
        {
            Debug.Log("Metadata file does not exist, update is required");
            return true;
        }

        string metadataContent = File.ReadAllText(LocalMetadatFilePath);

        if (string.IsNullOrEmpty(metadataContent))
        {
            Debug.Log("Metadata file is empty, update is required");
            return true;
        }

        MetadataFile remoteMetaData = JsonUtility.FromJson<MetadataFile>(metadataContent);
        if (remoteMetaData == null)
        {
            Debug.LogError("Failed to parse metadata content, update required");
            return true;
        }

        if(version != remoteMetaData.version) 
        {
            Debug.Log($"new version detected: {remoteMetaData.version}. updating from {version}");
            version = remoteMetaData.version;

            RemoteFileDownloadLink = GoogleDriveHelper.ConvertToDirectDownloadLink(remoteMetaData.fileLink);

            return true;
        }

        Debug.Log($"{filename} is up-to-date");
        return false;
    }

    public void DeleteLocalFile()
    {
        if (File.Exists(LocalFilePath))
        {
            File.Delete(LocalFilePath);
            Debug.Log($"Deleting outdated file ast {LocalFilePath}");
        }
    }

}
