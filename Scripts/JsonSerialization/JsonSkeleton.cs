using System;

[Serializable]
public class JsonSkeleton : JsonGraphics
{

    public string source;
    public string skin;
    public string idle;
    public bool isActor;

    public JsonSkeleton(string name, float x, float y, int sort, float scaleX, float scaleY, 
        string source, string skin, string idle, bool isActor)
        : base(name, x, y, sort, scaleX, scaleY)
    {
        this.source = source;
        this.skin = skin;
        this.idle = idle;
        this.isActor = isActor;
    }
}
