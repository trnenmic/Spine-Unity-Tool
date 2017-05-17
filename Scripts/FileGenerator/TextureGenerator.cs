using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class TextureGenerator {

    public void generateAtlasWithTextures(JsonStage stage, List<Texture2D> textures)
    {
        /**** GENERATE ATLAS AND TEXTURE PACK ****/
        int textureMaxSize = 8192;
        long maxFilled = textureMaxSize * textureMaxSize * 1 / 2;
        long pictureNameIndex = 0;
        AtlasGenerator generator = new AtlasGenerator();
        Texture2D atlas = new Texture2D(textureMaxSize, textureMaxSize);
        Rect[] rectangles = ShortPacker.PackTextures(atlas, textures.ToArray(), 512, 512, 2048); //8192
        byte[] bytes = atlas.EncodeToPNG();
        File.WriteAllBytes(Application.dataPath + ProjectConstants.exportPath
            + ProjectConstants.exportImgPath + stage.id + pictureNameIndex.ToString() + ".png", bytes);
        string atlasTxt2 = generator.generateAtlas(rectangles, textures.ToArray(), atlas, stage.id + pictureNameIndex.ToString());
        File.WriteAllText(Application.dataPath + ProjectConstants.exportPath
            + ProjectConstants.exportImgPath + stage.id + ".atlas", atlasTxt2);
    }
}
