using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectConstants : MonoBehaviour {

    /*** Project Settings ***/
    public static bool exportHtml = true;
    public static bool exportConfigJson = true;
    public static bool exportAtlas = true;

    public static int unit = 100;
    public static int topTransform = 256;

    /*** Project Paths ***/
    public static string exportPath = "/DigitalShard/UnversionedAssets/spine_unity_tool/";
    public static string exportWebPath = "WebExport/";
    public static string exportJsonPath = "WebExport/json/";
    public static string exportImgPath = "WebExport/img/";
    public static string exportSpinePath = "WebExport/spine/";
    public static string exportPreferencesPath = "Scripts/Preferences/";
    public static string webTemplatesPath = "WebTemplates/";
    public static string scenePath = "/DigitalShard/UnversionedAssets/spine_unity_tool/Scenes/";

    /*** FileNames ***/
    public static string exportHtmlName = "output.html";
}
