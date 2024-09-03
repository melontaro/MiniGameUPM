using System.Collections.Generic;
using System.IO;
using System.Text;
using Coo.Client;
using UnityEditor;
using UnityEngine;

public class WebGLUIEditor:EditorWindow
{
    // 定义一个字符串变量，用于存储输入框的文本
    private string inputText = "Enter text here";

    // 添加菜单项以打开窗口
    [MenuItem("Tools/WebGLUI")]
    public static void ShowWindow()
    {
        // 显示现有窗口，如果没有则创建一个新的
        EditorWindow.GetWindow(typeof(WebGLUIEditor), false, "My Window");
    }

    void OnGUI()
    {
        // 输入框
        GUILayout.Label("Input Text:", EditorStyles.boldLabel);
        inputText = EditorGUILayout.TextField(inputText);

        // 按钮点击事件
        if (GUILayout.Button("Click Me!"))
        {
            // 当按钮被点击时，可以在这里执行一些操作
          //  Debug.Log("Button clicked with input text: " + inputText);
            GenerateGet(inputText);
        }
    }

    void GenerateGet(string rootPath)
    {
        StringBuilder sb = new StringBuilder();
        string[] subdirectoryEntries = Directory.GetDirectories(rootPath);
        int count = subdirectoryEntries.Length;
        string str1= $" var list = new YIUIBindVo[{count}];";
        sb.Append(str1);
        int index = 0;
        // 遍历子文件夹
        foreach (string subdirectory in subdirectoryEntries)
        {
          Debug.LogError(subdirectory);
          var uiPath = Path.Combine(Application.dataPath, subdirectory);
          DirectoryInfo dir = new DirectoryInfo(uiPath);
          string str2 =
              $"list[{index}] = new YIUIBindVo\n          {{\n              UIPanel = UIType.{dir.Name},\n             " +
              $" UILayer = UILayer.Mid,\n              ComponentType = typeof(ET.Client.{dir.Name}Component),\n          }};\n";
          
          
          sb.Append(str2);
          
          
          index++;
        }
        Debug.LogError(sb.ToString());
    }
    
    
    
    
    
    
    

}