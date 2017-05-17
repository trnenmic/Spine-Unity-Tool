using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class EditorGroup {

    [Tooltip("Name of scene to be used as stage in HTML5 canvas.")]
    public string groupName = "";
    [Tooltip("ID to be referenced by javascript")]
    public string groupSize = "";
    public string group = "";
    public int order = 0;

    private bool stageShown = true;
    public bool isErased = false;

    public EditorGroup(int order)
    {
        this.order = order;
    }

    public int generateContent(int compareOrder)
    {
        order = compareOrder;
        GUIStyle myFoldoutStyle = new GUIStyle(EditorStyles.foldout);
        myFoldoutStyle.fontStyle = FontStyle.Bold;
        EditorGUILayout.BeginVertical(EditorStyles.helpBox, GUILayout.MaxWidth(500));
        EditorGUILayout.BeginHorizontal(GUILayout.MaxWidth(500));
        stageShown = EditorGUILayout.Foldout(stageShown, new GUIContent("Stage (" + compareOrder + ") : " + groupSize, "tooltip"), myFoldoutStyle);
        if (GUILayout.Button("-", GUILayout.MaxWidth(30), GUILayout.MinWidth(20)))
        {
            isErased = true;
        }
        if (GUILayout.Button("↑", GUILayout.MaxWidth(30), GUILayout.MinWidth(20)))
        {
            order--;
        }
        if (GUILayout.Button("↓", GUILayout.MaxWidth(30), GUILayout.MinWidth(20)))
        {
            order++;
        }
        EditorGUILayout.EndHorizontal();

            EditorGUILayout.BeginHorizontal(GUILayout.MaxWidth(550));
            groupName = EditorGUILayout.TextField("Scene:", groupName);
            EditorGUILayout.EndHorizontal();
            groupSize = EditorGUILayout.TextField("Stage ID:", groupSize, GUILayout.MaxWidth(550));


        EditorGUILayout.EndVertical();
        return this.order;
    }
}
