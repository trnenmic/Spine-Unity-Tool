using System;
using UnityEngine;

public class AtlasGenerator
{


    public AtlasGenerator()
    {

    }

    public string generateAtlas(Rect[] rectangles, Texture2D[] textures, Texture2D atlas, string sceneName)
    {

        string content = "\n" + sceneName + ".png"
            + "\nsize: " + atlas.width + "," + atlas.height
            + "\nformat: " + atlas.format
            + "\nfilter: " + atlas.filterMode + "," + atlas.filterMode
            + "\nrepeat: " + "none" + "\n";
        
        
        for (int i = 0; i < rectangles.Length; i++)
        {
            if(textures[i].name == "hall_plain_v1")
            {
                Debug.Log(rectangles[i].x * atlas.width + " " + rectangles[i].y * atlas.height);
            }
            Debug.Log("i");
            Debug.Log(rectangles[i].width * atlas.width);
            Debug.Log(textures[i].width);
            float rectWidth = rectangles[i].width * atlas.width;
            float rectHeight = rectangles[i].height * atlas.height;
            bool rotate = !(rectangles[i].width * atlas.width == textures[i].width && rectangles[i].height * atlas.height == textures[i].height);
            content += textures[i].name + "\n"
                + "  rotate: "+ (rotate ? "true" : "false") + "\n"
                + "  xy: " + rectangles[i].x * atlas.width + ", " + (atlas.height - (rectangles[i].y * atlas.height + rectangles[i].height * atlas.height)) + "\n"
                + "  size: " + (rotate ? rectHeight : rectWidth) + ", " + (rotate ? rectWidth : rectHeight) + "\n"
                + "  orig: " + (rotate ? rectHeight : rectWidth)  + ", " + (rotate ? rectWidth : rectHeight) + "\n"
                + "  offset: 0, 0" + "\n"
                ;
        }

        return content;
    }


    public static Sprite GetSprite(string atlasPath, string name)
    {

        foreach (var obj in Resources.LoadAll<Sprite>(atlasPath))
        {
            if (obj.name == name)
            {
                return obj;
            }
        }
        Debug.LogWarningFormat("[Cant get icon] name: {0}", name);
        return null;

    }
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

}
