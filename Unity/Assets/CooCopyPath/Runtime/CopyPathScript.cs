using UnityEngine;
using UnityEditor;

using System.Collections.Generic;
#if Lean
using Lean.Gui;
#endif

using System.IO;

public class CopyPathScript : MonoBehaviour
{
    [MenuItem("GameObject/CopyPath/CopyFullPath", false, 0)]
    static void InitFullPath()
    {
        if (Selection.gameObjects != null && Selection.gameObjects.Length == 1)
        {
            string pathStr = string.Empty;
            GetPath(Selection.gameObjects[0].transform, ref pathStr);
        
            TextEditor te = new TextEditor();
            te.text = pathStr;
            te.SelectAll();
            te.Copy();
        }
        else
        {
            Debug.LogError("请只选择一个物体进行复制路径;");
        }
    }

    // GetCompomentByPath<Text>("gongfaInfo/ScrollRect/Viewport/Content/gongfaAdd/txtShengMing/txtAttri");
    [MenuItem("GameObject/CopyPath/CopyTextByFullPath", false, 0)]
    static void InitFullByCommentTextPath()
    {
        if (Selection.gameObjects != null && Selection.gameObjects.Length == 1)
        {
            string pathStr = string.Empty;
            GetPath(Selection.gameObjects[0].transform, ref pathStr);

            TextEditor te = new TextEditor();
            pathStr = @"GetCompomentByPath<Text>(" + "\"" + pathStr + "\"" + ");";
            te.text = pathStr;
            te.SelectAll();
            te.Copy();
        }
        else
        {
            Debug.LogError("请只选择一个物体进行复制路径;");
        }
    }

    [MenuItem("GameObject/CopyPath/CopyImageByFullPath", false, 0)]
    static void InitFullByCommentImagePath()
    {
        if (Selection.gameObjects != null && Selection.gameObjects.Length == 1)
        {
            string pathStr = string.Empty;
            GetPath(Selection.gameObjects[0].transform, ref pathStr);

            TextEditor te = new TextEditor();
            pathStr = @"GetCompomentByPath<Image>(" + "\"" + pathStr + "\"" + ");";
            te.text = pathStr;
            te.SelectAll();
            te.Copy();
        }
        else
        {
            Debug.LogError("请只选择一个物体进行复制路径;");
        }
    }

    [MenuItem("GameObject/CopyPath/CopyParentPath", false, 0)]
    static void InitParent()
    {
        if (Selection.gameObjects != null && Selection.gameObjects.Length == 1)
        {
            TextEditor te = new TextEditor();
            if (Selection.gameObjects[0].transform.parent == null)
            {
                Debug.LogError("无父物体;");
            }
            else
            {
                string pathStr = string.Empty;
                GetPath(Selection.gameObjects[0].transform.parent, ref pathStr);
                te.text = pathStr;
                te.SelectAll();
                te.Copy();
            }
        }
        else
        {
            Debug.LogError("请只选择一个物体进行复制路径;");
        }
    }

    [MenuItem("GameObject/CopyPath/CopyName", false, 0)]
    static void InitName()
    {
        if (Selection.gameObjects != null && Selection.gameObjects.Length == 1)
        {
            string pathStr = Selection.gameObjects[0].name;
            TextEditor te = new TextEditor();
            te.text = pathStr;
            te.SelectAll();
            te.Copy();
        }
        else
        {
            Debug.ClearDeveloperConsole();
            Debug.LogError("请只选择一个物体进行复制路径;");
        }
    }
    
    [MenuItem("GameObject/CopyPath/ThisCopyFullPath", false, 0)]
    static void ThisCopyFullPath()
    {
        if (Selection.gameObjects != null && Selection.gameObjects.Length == 1)
        {
            string pathStr = string.Empty;
            GetPath(Selection.gameObjects[0].transform, ref pathStr);
        
            TextEditor te = new TextEditor();
            te.text = string.Format("this.transform.find(\"{0}\");", pathStr);
            te.SelectAll();
            te.Copy();
        }
        else
        {
            Debug.LogError("请只选择一个物体进行复制路径;");
        }
    }

    static string GetPath(Transform tr, ref string str)
    {
        if (tr != null)
        {
            str = tr.name + str;
            tr = tr.parent;
            if (tr != null)
            {
                str = "/" + str;
            }

            GetPath(tr, ref str);
        }
        else
        {
            return str;
        }

        return str;
    }

    [MenuItem("GameObject/CreateBindPoint", false, 0)]
    static void CreateBindPoint()
    {
        if (Selection.gameObjects != null && Selection.gameObjects.Length == 1)
        {
            Transform parent = Selection.gameObjects[0].transform;
            GameObject bindObj = new GameObject();
            bindObj.transform.SetParent(parent, false);
            bindObj.name = "bindpoint";


            GameObject head = new GameObject();
            head.transform.SetParent(bindObj.transform, false);
            head.name = "head";

            GameObject body = new GameObject();
            body.transform.SetParent(bindObj.transform, false);
            body.name = "body";

            GameObject foot = new GameObject();
            foot.transform.SetParent(bindObj.transform, false);
            foot.name = "foot";
        }
        else
        {
            Debug.LogError("请只选择一个物体操作");
        }
    }


    
    public static string GetPathRelativeToParent(Transform child, Transform parent)
    {
        string path = child.name;
        while (child.parent != null)
        {
            if (child.parent == parent)
            {
                break;
            }

            child = child.parent;
            path = child.name + "/" + path;
        }

        return path;
    }



    public static void CreateFile(string content,Transform parent)
    {
        //\AAAGame\Scripts\UI\UIVariables\SignUpForm.Variables.cs
        // 获取文件路径
        string path = Application.dataPath +"/AAAGame/Scripts/UI/UIVariables/"+parent.name+ ".Variables.cs";
Debug.LogError(path);
        if (File.Exists(path))
        {
            // 文件存在，清空内容
            File.WriteAllText(path, string.Empty);
            File.WriteAllText(path, content);
        }
        else
        {
            // 文件不存在，创建文件
            using (StreamWriter writer = File.CreateText(path))
            {
                writer.WriteLine(content);
            }
        }
    }
    

   static void ChangeLayer(Transform trans, string targetLayer)
    {
        if (LayerMask.NameToLayer(targetLayer)==-1)
        {
            Debug.Log("Layer中不存在,请手动添加LayerName");
            
            return;
        }
        //遍历更改所有子物体layer
        trans.gameObject.layer = LayerMask.NameToLayer(targetLayer);
        foreach (Transform child in trans)
        {
            ChangeLayer(child, targetLayer);
            Debug.Log(child.name +"子对象Layer更改成功！");
        }
    }

}