using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;

[Serializable]
public class JsonStage
{
    /******/
    public string id;
    public int width;
    public int height;
    public string group;

    public bool moduleResize = true;
    public bool moduleMouseFocus = true;
    public bool moduleWalk = false;
    public bool moduleShootAim = false;
    public bool moduleDialogue = false;
    public bool moduleQuote = false;
    public bool moduleFight = false;
    public string moduleCharacter = "";

    public bool useLocalAssets = false;

    public List<JsonBitmap> bitmaps;
    public List<JsonImage> images;
    public List<JsonSkeleton> skeletons;
    public List<JsonTrigger> triggers;
    public List<JsonLight> lights;
    /******/

    public JsonStage(string id = "stage_1", int width = 640, int height = 640, string group = "", bool useLocalAssets = false)
    {
        this.useLocalAssets = useLocalAssets;
        this.id = id;
        this.width = width;
        this.height = height;
        this.bitmaps = new List<JsonBitmap>();
        this.images = new List<JsonImage>();
        this.skeletons = new List<JsonSkeleton>();
        this.triggers = new List<JsonTrigger>();
        this.lights = new List<JsonLight>();
    }
    

}

