using UnityEngine;
using System.Text.RegularExpressions;

public class GoogleDriveHelper 
{
    

    public static string ConvertToDirectDownloadLink(string rawLink)
    {
        Match match = Regex.Match(rawLink, @"(?:drive\.google\.com/.*?\/d\/|id=)([a-zA-Z0-9_-]+)");

        

        string result = string.Empty;

        if (match.Success)
        {
            string id = match.Groups[1].Value;
            result = "https://drive.google.com/uc?export=download&id=" + id;
            Debug.Log(result);
        }
        else
        {
            Debug.LogError("Invalid Google Drive Link");
        }

        return result;
    }


}
