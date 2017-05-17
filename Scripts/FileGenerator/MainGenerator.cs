using DigitalShard;
using System.Collections.Generic;
using System.IO;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainGenerator
{

    string exportName = "cfg";
    public NameValidator validator;
    public List<EditorStage> stages;
    public EditorPage editorPage;
    HtmlBuilder htmlBuilder;
    JsonBuilder jsonBuilder;

    public MainGenerator(List<EditorStage> stages, EditorPage page, string exportName)
    {
        this.editorPage = page;
        this.stages = stages;
        this.validator = new NameValidator(stages);
        this.exportName = exportName;
    }

    public void generateFiles()
    {
        string error = validator.repairAndValidateEditorData();
        if (error.Equals(""))
        {
            JsonWebPage page = new JsonWebPage(useGlobalAssets: editorPage.useAllAssets);
            this.htmlBuilder = new HtmlBuilder();
            this.jsonBuilder = new JsonBuilder(stages, editorPage);
            jsonBuilder.addGroupsToPage(page);
            htmlBuilder.appendString(File.ReadAllText(Application.dataPath + ProjectConstants.exportPath + ProjectConstants.webTemplatesPath + "article_intro.html"));
            foreach (EditorStage editorStage in stages)
            {
                if (ProjectConstants.exportConfigJson || ProjectConstants.exportAtlas)
                {
                    string sceneName = editorStage.fName.EndsWith(".unity") ? editorStage.fName : editorStage.fName + ".unity";
                    Scene scene = EditorSceneManager.OpenScene(Application.dataPath + ProjectConstants.scenePath + sceneName, OpenSceneMode.Single);
                    Vector3 p = jsonBuilder.findMainCameraPosition();
                    p.y += jsonBuilder.topTransform;
                    JsonStage stage = new JsonStage(id: editorStage.id, group: editorStage.group,
                        height: editorStage.height, width: editorStage.width, useLocalAssets: editorStage.useLocalAssets);
                    jsonBuilder.setModuleSettings(stage, editorStage);
                    page.stages.Add(stage);
                    List<Texture2D> textures = new List<Texture2D>();
                    List<object> graphics = jsonBuilder.prepareGraphicsObjects();
                    jsonBuilder.addGraphicsData(stage, textures, graphics, p);
                    List<TriggerBase> triggers = jsonBuilder.addTriggers(stage, p);
                    if (ProjectConstants.exportAtlas)
                    {
                        TextureGenerator textureGenerator = new TextureGenerator();
                        textureGenerator.generateAtlasWithTextures(stage, textures);
                    }

                }
                if (ProjectConstants.exportHtml)
                {
                    htmlBuilder.apendHtmlPart(editorStage);
                    htmlBuilder.setLastStage(editorStage);
                }
            }
            if (ProjectConstants.exportHtml)
            {
                htmlBuilder.appendString(File.ReadAllText(Application.dataPath + ProjectConstants.exportPath + ProjectConstants.webTemplatesPath + "footer.html"));
                File.WriteAllText(Application.dataPath + ProjectConstants.exportPath + ProjectConstants.exportWebPath + "output.html", htmlBuilder.htmlPage);
            }
            if (ProjectConstants.exportConfigJson)
            {
                string jsonExport = JsonUtility.ToJson(page, true);
                File.WriteAllText(Application.dataPath + ProjectConstants.exportPath + ProjectConstants.exportJsonPath + exportName + ".json", jsonExport);
            }
        }
        else
        {
            Debug.LogError(error);
        }
    }



    
}
