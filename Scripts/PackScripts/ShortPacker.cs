using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShortPacker : MonoBehaviour {

    public static Rect[] PackTextures(Texture2D texture, Texture2D[] textures, int width, int height, int maxSize)
    {

        if (width > maxSize && height > maxSize) return null;
        if (width > maxSize || height > maxSize) { int temp = width; width = height; height = temp; }

        MaxRectsBinPack bp = new MaxRectsBinPack(width, height, false);
        Rect[] rects = new Rect[textures.Length];

        for (int i = 0; i < textures.Length; i++)
        {
            Texture2D tex = textures[i];
            Rect rect = bp.Insert(tex.width, tex.height, MaxRectsBinPack.FreeRectChoiceHeuristic.RectBestAreaFit);
            if (rect.width == 0 || rect.height == 0)
            {
                return PackTextures(texture, textures, width * (width <= height ? 2 : 1), height * (height < width ? 2 : 1), maxSize);
            }
            rects[i] = rect;
        }
        texture.Resize(width, height);
        texture.SetPixels(new Color[width * height]);
        for (int i = 0; i < textures.Length; i++)
        {
            Texture2D tex = textures[i];
            Rect rect = rects[i];
            Color[] colors = tex.GetPixels();

            if (rect.width != tex.width)
            {
                Color[] newColors = tex.GetPixels();

                for (int x = 0; x < rect.width; x++)
                {
                    for (int y = 0; y < rect.height; y++)
                    {
                        int prevIndex = ((int)rect.height - (y + 1)) + x * (int)tex.width;
                        newColors[x + y * (int)rect.width] = colors[prevIndex];
                    }
                }

                colors = newColors;
            }

            texture.SetPixels((int)rect.x, (int)rect.y, (int)rect.width, (int)rect.height, colors);
            rect.x /= width;
            rect.y /= height;
            rect.width /= width;
            rect.height /= height;
            rects[i] = rect;
        }
        texture.Apply();
        return rects;

    }

}
