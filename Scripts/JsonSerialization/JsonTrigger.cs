using System;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

[Serializable]
public class JsonTrigger : JsonStandardObject {

    public string type;
    public string onSkeleton;
    public string onElement;
    public string animation;
    public int distance;
    public string controls;
    
    public JsonTrigger(string name, float x, float y, string type = "DISTANCE+INPUT",  // DISTANCE, DISTANCE+INPUT, INPUT
        string onSkeleton ="", string onElement = "", string animation ="", int distance = 64, string controls="")
        : base(name, x, y)
    {
        this.type = type;
        this.onSkeleton = onSkeleton;
        this.onElement = onElement;
        this.animation = animation;
        this.controls = controls;
        this.distance = distance;
    }
}
