using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;
using OfficeOpenXml;
using UnityEditor;
using UnityEngine;
using Object = UnityEngine.Object;

public class GameControlerExporterTool : Editor
{
    static int rowStart = 5;
    static int colStart = 3;

    [MenuItem("GameObject/导出GameControler数据到Excel", false, 0)]
    static void GameControler()
    {
        var obj = Selection.activeObject as GameObject;

        var fullPath = "D:\\GitWorkSpace\\TapGameWitch\\Unity\\Assets\\Config\\Excel\\GameControlerConfig.xlsx";
        // if (File.Exists(fullPath)) File.Delete(fullPath);
        var excelFile = new ExcelPackage(new FileInfo(fullPath));
        var st = excelFile.Workbook.Worksheets["Sheet1"]; //[0]不可以为数字0,必须传字符串,表单名称

        
        //
        List<ControlerUIData> controlerUIDatas = new List<ControlerUIData>();
        var pageNum =int.Parse(obj.name.ElementAt(obj.name.Length - 1).ToString()) ;
        
        for (int i = 0; i < obj.transform.childCount; i++)
        {
            var controlerUIData = new ControlerUIData();

          var child=  obj.transform.GetChild(i);
         
          var stagePos = child.GetComponent<RectTransform>().anchoredPosition;
          controlerUIData.Num = i;
          controlerUIData.Postion = new vect2(stagePos.x, stagePos.y);//$"{stagePos.x},{stagePos.y}";
            controlerUIDatas.Add(controlerUIData);
            
        }
        
        
        
        var page = st.Cells[rowStart+pageNum, colStart];
        page.Value = pageNum;

        var stage = st.Cells[rowStart+pageNum, colStart + 1];
        stage.Value = JsonConvert.SerializeObject(controlerUIDatas);

   

        excelFile.Save();
        excelFile.Dispose();
        Debug.Log("导出完成");
    }


    public class ControlerUIData
    {
       
        public int Num;
        public vect2 Postion;

      
    }
    
    public class  vect2
    {
        public float x;
        public float y;
        public vect2(float x, float y)
        {
            this.x = x;
            this.y = y;
        }
    }
}