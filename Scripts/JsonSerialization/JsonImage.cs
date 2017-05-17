using System;


[Serializable]
public class JsonImage : JsonGraphics
{

    /*public string name;
    public string path;
    public int sort;
    public int x;
    public int y;
    public float scaleX;
    public float scaleY;*/

    public string path;

    public JsonImage (string name, float x, float y, int sort, float scaleX, float scaleY, string path) : base(name, x, y, sort, scaleX, scaleY)
    {
        this.path = path;
    }
}
