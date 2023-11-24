using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
#if Lean
using Lean.Gui;
#endif

using System.IO;

public class CopyPathScript : MonoBehaviour
{
    [MenuItem("Tools/解析AssetBundle", false, 0)]
    static void InitFullPath()
    {
        string path = "Assets/StreamingAssets/WebGL";
        AssetBundle assetBundle = AssetBundle.LoadFromFile(path);
        AssetBundleManifest manifest = assetBundle.LoadAsset<AssetBundleManifest>("AssetBundleManifest");
        List<BundleData> bundleDatas = new List<BundleData>();
        foreach (var item in manifest.GetAllAssetBundles())
        {
            Debug.LogError(item);
            var abui = AssetBundle.LoadFromFile("Assets/StreamingAssets/" + item);

            foreach (var assetName in abui.GetAllAssetNames())
            {
                Debug.LogError(assetName);
            }

            if (item.Contains('_'))
            {
                var bundleName = item.Split('_')[0];
                BundleData ba = new BundleData();
                ba.name = bundleName;
                ba.nameWithMd5 = item;
                bundleDatas.Add(ba);
            }
        }

        var json = LitJson.JsonMapper.ToJson(bundleDatas);
        Debug.LogError(json);
        var pathFile = Path.Combine(Application.dataPath, "Bundles/Config/AssetBundle.txt");
        File.WriteAllText(pathFile, json);
        assetBundle.Unload(true);
    }
}

class BundleData
{
    public string name;
    public string nameWithMd5;
}