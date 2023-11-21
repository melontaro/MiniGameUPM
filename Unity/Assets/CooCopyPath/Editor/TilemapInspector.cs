using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.Tilemaps;
using System.IO;

[CustomEditor(typeof(TilemapBehaviour))]
[CanEditMultipleObjects]
public class TilemapInspector : Editor
{
    private TilemapBehaviour tilemapBehaviour;

    public override void OnInspectorGUI()
    {
        tilemapBehaviour = (TilemapBehaviour)target;
        base.OnInspectorGUI();
        if (GUILayout.Button("������ͼ"))
        {
            ExportMap();
        }

        if (GUILayout.Button("�����ͼ"))
        {
            if (EditorUtility.DisplayDialog("��ʾ", "ȷ��Ҫ�����ͼ��", "ȷ��", "ȡ��"))
                ClearMap();
        }
    }


    public void ExportMap()
    {
        TileBase[] tileArray = tilemapBehaviour.Tilemap.GetTilesBlock(tilemapBehaviour.area);

        Debug.Log(string.Format("Tilemap��{0} ׼��������ͼ����", tilemapBehaviour.Tilemap.name));

        int tilecount = 0;

        Dictionary<string, string> data = new Dictionary<string, string>();

        for (int i = tilemapBehaviour.area.xMin; i < tilemapBehaviour.area.xMax; i++)
        {
            for (int j = tilemapBehaviour.area.yMin; j < tilemapBehaviour.area.yMax; j++)
            {

                Vector3Int tempVec = new Vector3Int(i, j, 0);
                if (tilemapBehaviour.Tilemap.GetTile(tempVec) == null)
                    continue;

                Debug.Log(string.Format("λ�ã�{0} Tile��{1}", tempVec.ToString(), tilemapBehaviour.Tilemap.GetTile(tempVec).ToString()));

                data.Add(i + "_" + j, tilemapBehaviour.Tilemap.GetTile(tempVec).name);

                tilecount++;
            }
        }

        string jsonData = LitJson.JsonMapper.ToJson(data);

        ExportMap(jsonData, tilemapBehaviour.Tilemap.name);

        Debug.Log(string.Format("Tilemap��{0} �ܹ�Tile������{1}", tilemapBehaviour.Tilemap.name, tilecount.ToString()));

    }


    public void ExportMap(string data, string mapName)
    {
        string fullPath = EditorUtility.SaveFilePanel("�����ͼ�ļ�", Application.dataPath, mapName, "json");
        if (string.IsNullOrEmpty(fullPath))
            return;

        TextWriter tw = new StreamWriter(fullPath, false);
        tw.Write(data);
        tw.Close();
        Debug.Log("������ͼ��� path:" + fullPath);
        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
    }

    public void ClearMap()
    {
        tilemapBehaviour.Tilemap.ClearAllTiles();
    }
}