using System.Collections.Generic;
using System.IO;
using Coo.Client;
using UnityEditor;
using UnityEngine;

public class SpriteScriptableObjectMenu
{
    [MenuItem("Assets/Create/SpriteScriptableObject", false, 10)]
    static void Export()
    {
        SpritesAsset sp = ScriptableObject.CreateInstance<SpritesAsset>();
        //sp.m_SpritesAssets = new List<SpriteItem>();
        foreach (var sprite in Selection.objects)
        {
            if (sprite is Sprite == false)
            {
                continue;
            }

            SpriteItem si = new SpriteItem();

            si.SpriteName = sprite.name;
            si.Sprite = sprite as Sprite;
            sp.m_SpritesAssets.Add(si);
        }

        string path = AssetDatabase.GetAssetPath(Selection.activeObject);
        FileInfo fi = new FileInfo(path);
        var directory = fi.Directory.FullName;
        Debug.LogError($"{directory}\\" + fi.Directory.Name + ".asset");
        AssetDatabase.CreateAsset(sp, $"Assets/" + fi.Directory.Name + ".asset");
        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
    }
}