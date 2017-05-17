using System;

[Serializable]
public class JsonBitmap : JsonGraphics {

    public string texture;

    public JsonBitmap(string name, float x, float y, int sort, float scaleX, float scaleY, string texture) 
        : base(name, x, y, sort, scaleX, scaleY)
    {
        this.texture = texture;
    }


}
