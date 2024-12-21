using System.Collections;
using System.Collections.Generic;
using System.IO;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Networking;

public class FileDownloader : MonoBehaviour
{

    [SerializeField] private List<MetadatScriptableObject> filesToDownload = new List<MetadatScriptableObject>();
    //public string downloadPath;

    // Start is called before the first frame update
    void Start()
    {
       // Debug.Log(GoogleDriveHelper.ConvertToDirectDownloadLink(downloadPath));
       StartCoroutine(CheckAndDownloadFiles());
    }

    private IEnumerator CheckAndDownloadFiles()
    {
        foreach(MetadatScriptableObject metaData in filesToDownload)
        {
            metaData.SetupLocalMetaData();
            yield return StartCoroutine(DownloadFile(metaData.DirectMetadataDownloadLink, metaData.LocalMetadatFilePath));
            yield return new WaitForEndOfFrame();

            if(metaData.FileNeedsUpdating()) 
            {
                metaData.DeleteLocalFile();

                if(!string.IsNullOrEmpty(metaData.LocalMetadatFilePath) && !string.IsNullOrEmpty(metaData.DirectMetadataDownloadLink))
                {
                    yield return StartCoroutine(DownloadFile(metaData.RemoteFileDownloadLink, metaData.LocalFilePath));
                    yield return new WaitForEndOfFrame();
                }
                else
                {
                    Debug.Log("Failed to obtain a valid download link, or local meta data path is not valid");
                }

                
            }
            else
            {
                Debug.Log($"{metaData.filename} is up-to-date");
            }

            yield return null;
        }

        yield return null;
    }

    private IEnumerator DownloadFile(string fileLink, string savePath)
    {
        if(string.IsNullOrEmpty(fileLink)) 
        {
            Debug.Log("Invalid file Link");
            yield break;
        }

        UnityWebRequest request = UnityWebRequest.Get(fileLink);
        yield return request.SendWebRequest();

        if(request.result != UnityWebRequest.Result.Success) 
        {
            Debug.LogError($"Failed to download file:{request.error}");
        }
        else
        {
            Directory.CreateDirectory(Path.GetDirectoryName(savePath));
            File.WriteAllBytes(savePath, request.downloadHandler.data);
            Debug.Log($"File Download Successfully to: {savePath}");
        }
    }


}
