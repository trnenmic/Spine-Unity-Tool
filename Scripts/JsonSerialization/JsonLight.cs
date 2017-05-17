using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class JsonLight : JsonGraphics {

    public float alpha;
    public string type;

    public JsonLight(string name, float x, float y, int sort, float scaleX, float scaleY,
        float alpha, string type)
        : base(name, x, y, sort, scaleX, scaleY)
    {
        this.alpha = alpha;
        this.type = type;
    }
}
