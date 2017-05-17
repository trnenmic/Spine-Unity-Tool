using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class HtmlBuilder {

    public string htmlPage = "";
    public string groupHtmlTab = "";
    public string groupHtmlWidgets = "";
    public string htmlName = ProjectConstants.exportHtmlName;
    public EditorStage lastStage;

    string replace(string pattern, string text)
    {
        return text;
    }

    public void apendHtmlPart(EditorStage editorStage)
    {
        string result = "";
        string tmp = "";
        if (editorStage.moduleCharacter == "none")
        {
            if (lastStage == null || lastStage.moduleCharacter == "none")
            {

            }
            else if (lastStage.moduleCharacter != "none")
            {
                //finalize block of characters
                groupHtmlWidgets += File.ReadAllText(Application.dataPath + ProjectConstants.exportPath + ProjectConstants.webTemplatesPath + "characters_5_end.html");
                groupHtmlTab += File.ReadAllText(Application.dataPath + ProjectConstants.exportPath + ProjectConstants.webTemplatesPath + "characters_3_end_rows.html");
                result += groupHtmlTab + groupHtmlWidgets;
                groupHtmlWidgets = "";
                groupHtmlTab = "";
            }
            if (editorStage.moduleFight)
            {
                tmp = File.ReadAllText(Application.dataPath + ProjectConstants.exportPath + ProjectConstants.webTemplatesPath + "fight_widget.html");
                tmp = tmp.Replace("{fight_scene}", editorStage.id);
                result += tmp;
            }
            else if (editorStage.moduleShootAim)
            {
                tmp = File.ReadAllText(Application.dataPath + ProjectConstants.exportPath + ProjectConstants.webTemplatesPath + "weapons_4_widget.html");
                tmp = tmp.Replace("{weapons_scene}", editorStage.id);
                result += tmp;
            }
        }
        else
        {
            if (lastStage == null || lastStage.moduleCharacter == "none")
            {
                //new character article
                tmp = File.ReadAllText(Application.dataPath + ProjectConstants.exportPath + ProjectConstants.webTemplatesPath + "characters_1_intro.html");
                tmp = tmp.Replace("{characters}", editorStage.group);
                groupHtmlTab += tmp;
                tmp = File.ReadAllText(Application.dataPath + ProjectConstants.exportPath + ProjectConstants.webTemplatesPath + "characters_2_tab_button.html");
                tmp = tmp.Replace("{hero}", editorStage.moduleCharacter);
                groupHtmlTab += tmp;
                tmp = File.ReadAllText(Application.dataPath + ProjectConstants.exportPath + ProjectConstants.webTemplatesPath + "characters_4_widget.html");
                tmp = tmp.Replace("{character_hero_scene}", editorStage.id);
                tmp = tmp.Replace("{loop}", "loop");
                groupHtmlWidgets += tmp;

            }
            else if (lastStage.moduleCharacter != "none")
            {
                //add subblocks of articles
                tmp = File.ReadAllText(Application.dataPath + ProjectConstants.exportPath + ProjectConstants.webTemplatesPath + "characters_2_tab_button.html");
                tmp = tmp.Replace("{hero}", editorStage.moduleCharacter);
                groupHtmlTab += tmp;
                tmp = File.ReadAllText(Application.dataPath + ProjectConstants.exportPath + ProjectConstants.webTemplatesPath + "characters_4_widget.html");
                tmp = tmp.Replace("{character_hero_scene}", editorStage.id);
                tmp = tmp.Replace("{loop}", "hide");
                groupHtmlWidgets += tmp;
            }
        }
        htmlPage += result;
    }

    public void appendString(string value)
    {
        htmlPage += value;
    }

    public void setLastStage(EditorStage editorStage)
    {
        lastStage = editorStage;
    }
}
