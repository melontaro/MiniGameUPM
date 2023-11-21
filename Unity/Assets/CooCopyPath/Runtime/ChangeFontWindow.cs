using UnityEngine;
using System.Collections;
using UnityEditor;
using UnityEngine.UI;

public class ChangeFontWindow : EditorWindow
{
    [MenuItem("Tools/������������")]
    public static void Open()
    {
        EditorWindow.GetWindow(typeof(ChangeFontWindow), true);
    }

    static Font fromChangeFont;
    static Font toChangeFont;

    void OnGUI()
    {
        fromChangeFont = (Font)EditorGUILayout.ObjectField("ԭ����", fromChangeFont, typeof(Font), true, GUILayout.MinWidth(100));
        toChangeFont = (Font)EditorGUILayout.ObjectField("Ŀ������", toChangeFont, typeof(Font), true, GUILayout.MinWidth(100));
        if (GUILayout.Button("ȷ�ϸ���"))
        {
            Change();
        }
    }

    public static void Change()
    {
        if (Selection.objects == null || Selection.objects.Length == 0) return;
        Object[] labels = Selection.GetFiltered(typeof(Text), SelectionMode.Deep);
        foreach (Object item in labels)
        {
            Text label = (Text)item;
            if (label.font == fromChangeFont)
            {
                label.font = toChangeFont;
                Debug.Log(item.name + ":" + label.text);
                EditorUtility.SetDirty(item);
            }
        }
    }
}