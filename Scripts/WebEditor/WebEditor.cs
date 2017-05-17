using UnityEngine;
using UnityEditor;
using UnityEngine.SceneManagement;
using System;
using System.IO;
using UnityEditor.AnimatedValues;
using UnityEditor.SceneManagement;
using System.Collections.Generic;

class WebEditor : EditorWindow
{
    Vector2 scrollPos;
    string t = "This is a string inside a Scroll view!";
    [SerializeField]
    public List<EditorStage> editorStages = new List<EditorStage>();
    [SerializeField]
    public EditorPage editorPage = new EditorPage();
    private int LIMIT = 5;

    private GUIStyle buttonStyle;
    private bool preferencesShown = true;
    private bool sceneManagerShown = true;

    [SerializeField]
    [Tooltip("This is a great tooltip")]
    public string preferencesName = "preferences_1";
    [SerializeField]
    public string exportName = "cfg";
    public int stageCount = 2;
   
    public string myString;
    bool groupEnabled;
    bool myBool = true;
    float myFloat = 1.23f;
    public GameObject obj = null;

    GameObject fireEffect;


    [MenuItem("Tools/Digital Shard/Web Editor")]
    public static void ShowWindow()
    {
        EditorWindow.GetWindow(typeof(WebEditor));

    }

    private void Awake()
    {
        string json = File.ReadAllText(Application.dataPath + ProjectConstants.exportPath + ProjectConstants.exportPreferencesPath + "autosave" + ".json");
        JsonUtility.FromJsonOverwrite(json, this);
    }

    void OnInspectorUpdate()
    {
        Repaint();
    }

    void OnGUI()
    {
        scrollPos = EditorGUILayout.BeginScrollView(scrollPos);
    
        generateWindowPreferences();
        generateSceneManager();
        generatePageSettings();

        GUILayout.EndScrollView();
    }

    void generatePageSettings()
    {
        editorPage.generateEditorPageSettings();
    }


    void generateWindowPreferences()
    {
        GUIStyle myFoldoutStyle = new GUIStyle(EditorStyles.foldout);
        myFoldoutStyle.fontStyle = FontStyle.Bold;

        designButtons();
        preferencesShown = EditorGUILayout.Foldout(preferencesShown, new GUIContent("Preferences", "tooltip"), myFoldoutStyle);
        if (preferencesShown)
        {
            EditorGUILayout.BeginHorizontal(GUILayout.MaxWidth(500));
            preferencesName = EditorGUILayout.TextField("Preferences name:", preferencesName);
            if (GUILayout.Button("Save", GUILayout.MaxWidth(80), GUILayout.MinWidth(70))) //
            {
                string jsonExport = JsonUtility.ToJson(this, true);
                File.WriteAllText(Application.dataPath + ProjectConstants.exportPath + ProjectConstants.exportPreferencesPath + "autosave" + ".json", jsonExport);
                File.WriteAllText(Application.dataPath + ProjectConstants.exportPath + ProjectConstants.exportPreferencesPath + preferencesName + ".json", jsonExport);
            }
            if (GUILayout.Button("Load", GUILayout.MaxWidth(80), GUILayout.MinWidth(70)))
            {
                string json = File.ReadAllText(Application.dataPath + ProjectConstants.exportPath + ProjectConstants.exportPreferencesPath + preferencesName + ".json");
                JsonUtility.FromJsonOverwrite(json, this);
            }
            EditorGUILayout.EndHorizontal();
            EditorGUILayout.BeginHorizontal(GUILayout.MaxWidth(500));
            exportName = EditorGUILayout.TextField("Export name", exportName);
            if (GUILayout.Button("Validate", GUILayout.MaxWidth(80), GUILayout.MinWidth(70)))
            {

            }
            if (GUILayout.Button("Export", GUILayout.MaxWidth(80), GUILayout.MinWidth(70)))
            {
                MainGenerator mainGenerator = new MainGenerator(editorStages, editorPage, exportName);
                Debug.Log("SceneJsonGenerator created");
                mainGenerator.generateFiles();
            }
            EditorGUILayout.EndHorizontal();

        }
    }

    void designButtons()
    {

    }

    void generateSceneManager()
    {
        GUIStyle myFoldoutStyle = new GUIStyle(EditorStyles.foldout);
        myFoldoutStyle.fontStyle = FontStyle.Bold;
        sceneManagerShown = EditorGUILayout.Foldout(sceneManagerShown, new GUIContent("Stages", "tooltip"), myFoldoutStyle);
        if (sceneManagerShown)
        {
           
            EditorGUILayout.BeginVertical(EditorStyles.helpBox, GUILayout.MaxWidth(500));
            editorStages.Sort((st1, st2) => st1.order.CompareTo(st2.order));
            int compareOrder = 0;
            for (int i = 0; i < editorStages.Count; i++)
            {
                EditorStage s = editorStages[i];
                EditorStage previous = (i == 0) ? null : editorStages[i - 1];
                if (!s.isErased)
                {
                    int currentOrder = s.generateContent(compareOrder, previous);
                    if (currentOrder != compareOrder && currentOrder >= 0 && currentOrder < editorStages.Count)
                    {
                        editorStages[currentOrder].order = compareOrder;
                    }
                }
                compareOrder++;
                
            }
            editorStages = filterErased(editorStages);
            EditorGUILayout.EndVertical();
            if (GUILayout.Button("Add Stage", GUILayout.MaxWidth(500))) //
            {
                EditorStage editorStage = new EditorStage(editorStages.Count);
                editorStages.Add(editorStage);
            }
            EditorUtility.SetDirty(this);
        }
    }

    List<EditorStage> filterErased(List<EditorStage> input)
    {
        return input.FindAll(c => !c.isErased);
    }

    private void OnDestroy()
    {
        string jsonExport = JsonUtility.ToJson(this, true);
        File.WriteAllText(Application.dataPath + ProjectConstants.exportPath + ProjectConstants.exportPreferencesPath + "autosave" + ".json", jsonExport);
    }
}
