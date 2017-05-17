using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JsonStandardObject {


    //[JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
    public string name;
    public int x;
    public int y;

    public JsonStandardObject(string name, float x, float y)
    {
        this.name = name;
        this.x = Mathf.RoundToInt(x);
        this.y = Mathf.RoundToInt(y);
    }


}
