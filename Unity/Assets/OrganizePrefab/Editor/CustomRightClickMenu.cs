using System;
using System.IO;
using UnityEditor;
using UnityEngine;
using Object = UnityEngine.Object;

public class CustomRightClickMenu: MonoBehaviour
{
    
    [MenuItem("Assets/复制此Prefab",false,10)]
    static void MyTestA()
    {
        var obj=    Selection.activeObject as GameObject;
       GetResourcePaths(obj);
   
    }

    public static void GetResourcePaths(GameObject prefab)
    {
        string targetDirectory; // 目标文件夹路径
        // 获取 Prefab 的路径
        string prefabPath = AssetDatabase.GetAssetPath(prefab);
        // 检查目标文件夹是否存在，如果不存在则创建
        // 复制资源文件到目标文件夹
        var oo = Application.dataPath.Replace("Assets", "");
      ;
        targetDirectory =Path.Combine(oo+  Path.GetDirectoryName(prefabPath), "New");
        if (Directory.Exists( targetDirectory))
        {
            Directory.Delete(targetDirectory, true);
          
        }
        Directory.CreateDirectory(targetDirectory);

        // 获取 Prefab 的所有依赖项
        string[] dependencies = AssetDatabase.GetDependencies(prefabPath);

        // 打印所有依赖项的路径
        foreach (string dependency in dependencies)
        {
            if (dependency.EndsWith(".cs"))continue;
            if (dependency.EndsWith(".shader"))continue; if (dependency.EndsWith(".asset"))continue;
            if (dependency.EndsWith(".psd"))continue;            if (dependency.EndsWith(".ttf"))continue;
            if (dependency.EndsWith(".cginc"))continue;

            // 检查资源路径是否为空
            if (!string.IsNullOrEmpty(dependency))
            {
                // 获取资源文件名
                string fileName = Path.GetFileName(dependency);

                // 生成目标文件路径
                string targetPath = Path.Combine(targetDirectory, fileName);

                // 复制资源文件到目标文件夹
             
                
                File.Move(Path.Combine(oo,dependency),Path.Combine(oo,targetPath)); // true表示如果目标文件已存在，则覆盖它
                
                
                string oldfile=Path.Combine(oo,dependency+".meta");
                string newfile=Path.Combine(oo,targetPath+".meta");
                File.Move(oldfile,newfile); // true表示如果目标文件已存在，则覆盖它
                Debug.Log($"复制资源: {dependency} 到 {targetPath}");
            }
        }
        
        AssetDatabase.Refresh();
    }

  public  class PrefabData
    {
        public Component cmp;
        public Object obj;
    }

}