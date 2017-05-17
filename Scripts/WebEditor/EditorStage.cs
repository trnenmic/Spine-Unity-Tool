using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;

[Serializable]
public class EditorStage
{

    [Tooltip("Name of scene to be used as stage in HTML5 canvas.")]
    public string fName = "";
    [Tooltip("ID to be referenced by javascript")]
    public string id = "";
    public int order = 0;

    public string group = "";

    public int width = 640;
    public int height = 640;
    public bool useLocalAssets = false;

    private bool stageShown = true;
    private bool modulesShown = false;
    public bool isErased = false;

    public bool moduleResize = true;
    public bool moduleMouseFocus = true;
    public bool moduleWalk = false;
    public bool moduleShootAim = false;
    public bool moduleDialogue = false;
    public bool moduleQuote = false;
    public bool moduleFight = false;
    public string moduleCharacter = "";
    private string[] characterOptions;

    public int selected = 0;

    public EditorStage(int order)
    {
        this.order = order;
    }

    public int generateContent(int compareOrder, EditorStage previous)
    {
        order = compareOrder;
        GUIStyle myFoldoutStyle = new GUIStyle(EditorStyles.foldout);
        myFoldoutStyle.fontStyle = FontStyle.Bold;
        EditorGUILayout.BeginVertical(EditorStyles.helpBox, GUILayout.MaxWidth(500));
        EditorGUILayout.BeginHorizontal(GUILayout.MaxWidth(500));
        stageShown = EditorGUILayout.Foldout(stageShown, new GUIContent("Stage (" + compareOrder + ") : " + id, "tooltip"), myFoldoutStyle);
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
        if (stageShown)
        {
            EditorGUILayout.BeginHorizontal(GUILayout.MaxWidth(550));
            fName = EditorGUILayout.TextField("Scene:", fName);
            if (GUILayout.Button("Load", GUILayout.MaxWidth(100)))
            {
                string sceneToOpen = fName.EndsWith(".unity") ? fName : fName + ".unity";
                EditorSceneManager.OpenScene(Application.dataPath + ProjectConstants.scenePath + sceneToOpen, OpenSceneMode.Additive);
            }
            EditorGUILayout.EndHorizontal();
            id = EditorGUILayout.TextField("Stage ID:", id, GUILayout.MaxWidth(550));
            EditorGUILayout.BeginHorizontal(GUILayout.MaxWidth(550));
            group = EditorGUILayout.TextField("Tab group:", group, GUILayout.MaxWidth(550));
            string previousGroupName = ((previous == null || previous.group == null) ? ("") : (previous.group));
            EditorGUILayout.LabelField("Previous:", GUILayout.MaxWidth(60));
            if (GUILayout.Button(previousGroupName, GUILayout.Width(200)))
            {
                group = previousGroupName;
            }
            EditorGUILayout.EndHorizontal();
            EditorGUILayout.BeginHorizontal();
            width = EditorGUILayout.IntField("Width:", width, GUILayout.MaxWidth(275));
            height = EditorGUILayout.IntField("Height:", height, GUILayout.MaxWidth(275));
            EditorGUILayout.EndHorizontal();
            useLocalAssets = EditorGUILayout.Toggle("Use local assets:", useLocalAssets, GUILayout.MaxWidth(550));
            modulesShown = EditorGUILayout.Foldout(modulesShown, new GUIContent("Module settings", "Enabling interactive modules"), myFoldoutStyle);
            if (modulesShown)
            {
                moduleResize = EditorGUILayout.Toggle("Module resize:", moduleResize, GUILayout.MaxWidth(550));
                moduleMouseFocus = EditorGUILayout.Toggle("Module mouse focus:", moduleMouseFocus, GUILayout.MaxWidth(550));
                moduleWalk = EditorGUILayout.Toggle("Module walk:", moduleWalk, GUILayout.MaxWidth(550));
                moduleShootAim = EditorGUILayout.Toggle("Module shoot and aim:", moduleShootAim, GUILayout.MaxWidth(550));
                moduleDialogue = EditorGUILayout.Toggle("Module dialogue:", moduleDialogue, GUILayout.MaxWidth(550));
                moduleQuote = EditorGUILayout.Toggle("Module quote:", moduleQuote, GUILayout.MaxWidth(550));
                moduleFight = EditorGUILayout.Toggle("Module fight:", moduleFight, GUILayout.MaxWidth(550));
                characterOptions = new String[] { "none", "hero", "ai", "todd", "garry", "generic" };
                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.LabelField(new GUIContent("Module character", "Pick character scheme"), GUILayout.MaxWidth(200));
                selected = EditorGUILayout.Popup(selected, characterOptions, GUILayout.MaxWidth(100));
                moduleCharacter = characterOptions[selected];
                EditorGUILayout.EndHorizontal();
            }
        }

        EditorGUILayout.EndVertical();
        return this.order;
    }

}
