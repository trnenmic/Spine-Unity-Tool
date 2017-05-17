using DigitalShard;
using Spine.Unity;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEditor;
using UnityEngine;

public class JsonBuilder : MonoBehaviour
{


    int unit;
    public int topTransform = 256;
    public EditorPage editorPage;
    public List<EditorStage> stages;
    string regex = "[^a-zA-Z0-9_.]+";
    string exportName = "cfg";


    public JsonBuilder(List<EditorStage> stages, EditorPage editorPage)
    {
        unit = ProjectConstants.unit;
        topTransform = ProjectConstants.topTransform;
        this.stages = stages;
        this.editorPage = editorPage;
    }

    public void setModuleSettings(JsonStage stage, EditorStage editorStage)
    {
        stage.moduleCharacter = editorStage.moduleCharacter;
        stage.moduleDialogue = editorStage.moduleDialogue;
        stage.moduleQuote = editorStage.moduleQuote;
        stage.moduleFight = editorStage.moduleFight;
        stage.moduleResize = editorStage.moduleResize;
        stage.moduleShootAim = editorStage.moduleShootAim;
        stage.moduleWalk = editorStage.moduleWalk;
        stage.moduleMouseFocus = editorStage.moduleMouseFocus;
    }

    public void addGroupsToPage(JsonWebPage page)
    {
        List<String> groupNames = new List<String>();
        foreach (EditorStage editorStage in stages)
        {
            if (editorStage.group != "")
            {
                if (!groupNames.Contains(editorStage.group))
                {
                    groupNames.Add(editorStage.group);
                }
            }
        }
        page.groups = groupNames;
    }


    public Vector3 findMainCameraPosition()
    {
        Camera[] cameras = Resources.FindObjectsOfTypeAll(typeof(Camera)) as Camera[];
        foreach (Camera cam in cameras)
        {
            if (cam.CompareTag("MainCamera"))
            {
                return cam.transform.position * unit;
            }
        }
        return new Vector3(0, 0, 0);
    }

    public List<object> prepareGraphicsObjects()
    {
        object[] sRenderers = Resources.FindObjectsOfTypeAll(typeof(SpriteRenderer));
        object[] meshes = Resources.FindObjectsOfTypeAll(typeof(SkeletonAnimation));
        foreach (object o in meshes)
        {
            Debug.Log(o);
        }

        object[] sAnimations = Resources.FindObjectsOfTypeAll(typeof(SkeletonAnimation));
        Debug.Log(sAnimations);
        int originalLength = sRenderers.Length;
        Array.Resize<object>(ref sRenderers, originalLength + sAnimations.Length);
        Array.Copy(sAnimations, 0, sRenderers, originalLength, sAnimations.Length);
        List<object> graphics = new List<object>(sRenderers);

        SortingLayersSorter sorter = new SortingLayersSorter(SortingLayer.layers);
        graphics.Sort((x, y) => sorter.sortByLayers(x, y));
        return graphics;
    }

    public void addGraphicsData(JsonStage stage, List<Texture2D> textures, List<object> graphics, Vector3 p)
    {
        int index = 0;
        List<string> names = new List<string>();
        List<string> lightNames = new List<string>();
        foreach (object obj in graphics)
        {
            SpriteRenderer sr = obj as SpriteRenderer;
            SkeletonAnimation sk = obj as SkeletonAnimation;
            if (sr != null)
            {
                if (sr.CompareTag("Light"))
                {
                    //bool isDisconnectedPrefabInstance = PrefabUtility.GetPrefabParent(sr) != null && PrefabUtility.GetPrefabObject(sr.transform) == null;
                    if (PrefabUtility.GetPrefabParent(sr) != null)
                    {
                        string name = Regex.Replace(sr.name, regex, "");
                        if (!lightNames.Contains(name))
                        {
                            float y = sr.transform.position.y * unit - p.y;
                            float x = sr.transform.position.x * unit - p.x;//- sr.sprite.rect.width / 2;
                            string type = "Spot";
                            if (((MyLight)sr.GetComponent("MyLight")).type == MyLight.Type.Point)
                            {
                                type = "Point";
                            }
                            JsonLight light = new JsonLight(name, x, y, 0, sr.transform.lossyScale.x,
                            sr.transform.lossyScale.y, sr.color.a, type);
                            stage.lights.Add(light);
                            lightNames.Add(name);
                        }

                    }
                }
                else
                {
                    Texture2D texture = sr.sprite.texture;
                    if (!containsTexture(texture, textures))
                    {
                        textures.Add(texture);
                    }
                    string name = Regex.Replace(sr.name, regex, "");
                    if (!names.Contains(name))
                    {
                        float y = sr.transform.position.y * unit - p.y;
                        float x = sr.transform.position.x * unit - p.x;//- sr.sprite.rect.width / 2;
                        JsonBitmap bitmap = new JsonBitmap(name, x, y, index, sr.transform.localScale.x,
                        sr.transform.localScale.y, sr.sprite.texture.name);
                        stage.bitmaps.Add(bitmap);
                        names.Add(name);
                    }
                    else
                    {
                        string message = "Skeleton name of object: " + sr.ToString()
                        + "is not unique after escaping chars. Escaped name: " + name
                        + " was not added to json.";
                        Debug.Log(message);
                    }
                    index++;
                }

            }
            else if (sk != null)
            {
                bool isActor = sk.gameObject.CompareTag("Player");
                if (!(PrefabUtility.GetPrefabParent(sk) == null && PrefabUtility.GetPrefabObject(sk.transform) != null))
                {
                    string name = Regex.Replace(sk.name, regex, "");
                    JsonSkeleton skeleton = new JsonSkeleton(name, sk.transform.position.x * unit - p.x,
                    sk.transform.position.y * unit - p.y, index, sk.transform.localScale.x, sk.transform.localScale.y,
                    sk.skeletonDataAsset.name.Substring(0, sk.skeletonDataAsset.name.Length - "_SkeletonData".Length),
                    sk.initialSkinName, sk.AnimationName, isActor);
                    stage.skeletons.Add(skeleton);
                    names.Add(name);
                }
                index++;
            }

        }
    }

    bool containsTexture(Texture2D texture, List<Texture2D> textures)
    {
        foreach (Texture2D t in textures)
        {
            if (t.name.Equals(texture.name) || t.Equals(texture))
            {
                return true;
            }
        }
        return false;
    }

    public List<TriggerBase> addTriggers(JsonStage stage, Vector3 p)
    {
        List<TriggerBase> triggers = new List<TriggerBase>(Resources.FindObjectsOfTypeAll(typeof(TriggerBase)) as TriggerBase[]);
        List<string> names = new List<string>();
        int index = 1;
        foreach (TriggerBase tr in triggers)
        {
            Trigger t = tr as Trigger;
            TriggerInput ti = tr as TriggerInput;
            string triggerName = Regex.Replace(tr.name, regex, "");
            if (!names.Contains(triggerName))
            {
                JsonTrigger jt = new JsonTrigger(stage.id + "__" + triggerName + "__" + index,
                tr.transform.position.x * unit - p.x, tr.transform.position.y * unit - p.y);
                jt.type = t != null ? "DISTANCE" : "DISTANCE+INPUT";
                stage.triggers.Add(jt);
                index = index++;
                names.Add(triggerName);
            }
            else
            {
                string message = "Trigger name of object: " + tr.ToString()
                    + "is not unique after escaping chars. Escaped name: " + triggerName
                    + " Therefore was not added to json.";
                Debug.Log(message);
            }

        }
        return triggers;
    }
}
