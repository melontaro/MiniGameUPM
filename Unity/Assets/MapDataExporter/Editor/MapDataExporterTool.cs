using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;
using OfficeOpenXml;
using UnityEditor;
using UnityEngine;
using Object = UnityEngine.Object;

public class MapDataExporterTool : Editor
{
    static int rowStart = 5;
    static int colStart = 3;

    [MenuItem("Assets/导出Map数据到Excel", false, 10)]
    static void MyTestB()
    {
        var obj = Selection.activeObject as GameObject;
        var path = AssetDatabase.GetAssetPath(obj);
        var directory = Path.GetDirectoryName(Path.GetFullPath(path));
        var fullPath = Path.Combine(directory, "StageUIConfig.xlsx");
        // if (File.Exists(fullPath)) File.Delete(fullPath);
        var excelFile = new ExcelPackage(new FileInfo(fullPath));
        var st = excelFile.Workbook.Worksheets["Sheet1"]; //[0]不可以为数字0,必须传字符串,表单名称

        
        //
        List<StageUIData> StageUIDatas = new List<StageUIData>();
        var pageNum =int.Parse(obj.name.ElementAt(obj.name.Length - 1).ToString()) ;
        
        for (int i = 0; i < obj.transform.Find("Stages").childCount; i++)
        {
            var stageUIData = new StageUIData();

          var stageObj=  obj.transform.Find("Stages").GetChild(i);
          var stageNum =int.Parse(stageObj.name.ElementAt(stageObj.name.Length - 1).ToString()) ;
          var stagePos = stageObj.GetComponent<RectTransform>().anchoredPosition;
          stageUIData.StageNum = stageNum;
          stageUIData.Postion = $"{stagePos.x},{stagePos.y}";
            StageUIDatas.Add(stageUIData);
            
        }
        
        
        
        var page = st.Cells[rowStart+pageNum, colStart];
        page.Value = pageNum;

        var stage = st.Cells[rowStart+pageNum, colStart + 1];
        stage.Value = JsonConvert.SerializeObject(StageUIDatas);

        var info = st.Cells[rowStart+pageNum, colStart + 2];
        info.Value = $"页面{pageNum}的所有stage";

        excelFile.Save();
        excelFile.Dispose();
        Debug.Log("导出完成");
    }


    public class StageUIData
    {
       
        public int StageNum;
        public string Postion;
    }
}