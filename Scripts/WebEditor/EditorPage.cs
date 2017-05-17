using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[Serializable]
public class EditorPage
{
    private bool pageSettingsShown = false;
    public bool useAllAssets = true;

    public void generateEditorPageSettings()
    {
        GUIStyle myFoldoutStyle = new GUIStyle(EditorStyles.foldout);
        pageSettingsShown = EditorGUILayout.Foldout(pageSettingsShown, new GUIContent("Page Settings", "tooltip"), myFoldoutStyle);
        if (pageSettingsShown)
        {
            useAllAssets = EditorGUILayout.Toggle(new GUIContent("Use global assets", "Using global texture image file 'all_assets.png' for rendering scene"), 
                useAllAssets, GUILayout.MaxWidth(550));
        }
    }
}
