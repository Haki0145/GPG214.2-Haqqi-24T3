using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;

public class CreateAssetBundles 
{
    [MenuItem("Assets/Build AssetBundles")]

    static void BuildAllAssetBundles()
    {
        string assetBundleDirectory = Path.Combine(Application.streamingAssetsPath,"AssetBundles");

        if (!Directory.Exists(assetBundleDirectory))
        {
            Directory.CreateDirectory(assetBundleDirectory);
        }

        BuildPipeline.BuildAssetBundles(assetBundleDirectory, BuildAssetBundleOptions.None, BuildTarget.StandaloneWindows);
    }
}
