using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class AssetBundles : MonoBehaviour
{


    [SerializeField] string fileName = "weektwelve";
    string combinedPath;
    private AssetBundle weekTwelveBundle;

    // Start is called before the first frame update
    void Start()
    {
        LoadAssetBundle();
        LoadGround();

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void LoadGround()
    {
        if(weekTwelveBundle == null)
        {
            return;
        }

        GameObject groundPrefab = weekTwelveBundle.LoadAsset<GameObject>("Ground");

        if(groundPrefab != null )
        {
            var ground  =  Instantiate(groundPrefab);
            ground.transform.position = transform.position;
        }
    }

    void LoadAssetBundle()
    {
        combinedPath = Path.Combine(Application.streamingAssetsPath, fileName);

        if (File.Exists(combinedPath))
        {
            weekTwelveBundle = AssetBundle.LoadFromFile(combinedPath);
            Debug.Log("Asset Bundle Loaded");
        }
        else
        {
            Debug.Log("File does not exist" + combinedPath);
        }
    }
}
