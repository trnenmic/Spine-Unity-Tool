using System;
using UnityEngine;

[Serializable]
public class JsonGraphics : JsonStandardObject {


    public int sort;
    public float scaleX;
    public float scaleY;

    public JsonGraphics(string name, float x, float y, int sort, float scaleX, float scaleY)
        : base(name, x, y)
    {
        this.sort = sort;
        this.scaleX = scaleX;
        this.scaleY = scaleY;
    }

}
