using System;
using System.Collections.Generic;

[Serializable]
public class JsonWebPage {

    public List<JsonStage> stages;
    public string name;
    public bool useGlobalAssets;
    public List<String> groups;

    public JsonWebPage(String name = "scene", bool useGlobalAssets = true)
    {
        this.name = name;
        this.useGlobalAssets = useGlobalAssets;
        this.stages = new List<JsonStage>();
        this.groups = new List<String>();
    }

}
