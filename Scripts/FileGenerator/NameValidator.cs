using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;

public class NameValidator
{

    List<string> ids = new List<string>();
    List<string> textureNames = new List<string>();
    List<string> lightNames = new List<string>();
    string regex = "[^a-zA-Z0-9_.]+";

    List<EditorStage> stages;

    public NameValidator(List<EditorStage> stages)
    {
        this.stages = stages;
    }

    public void prepareForNextStage()
    {
        textureNames = new List<string>();
        lightNames = new List<string>();
    }

    public void refreshManipulator()
    {
        textureNames = new List<string>();
        lightNames = new List<string>();
        ids = new List<string>();
    }

    bool isUniqueAfterRepair(EditorStage editorStage)
    {
        string id = repairName(editorStage.id);
        bool result = (ids.Contains(id) ? false : true);
        ids.Add(id);
        editorStage.id = id;
        return result;
    }

    string repairName(string repairable)
    {
        return Regex.Replace(repairable, regex, String.Empty);
    }

    public string repairAndValidateEditorData()
    {
        string errorMessage = "";
        errorMessage += validateStages();
        errorMessage += validateGroups();
        return errorMessage;
    }

    string validateGroups()
    {
        string errorMessage = "";
        for (int i = 0; i < stages.Count; i++)
        {
            string currentGroupName = stages[i].group;
            bool groupEnd = false;
            for (int j = i - 1; j >= 0; j--)
            {
                if (groupEnd && stages[j].group == currentGroupName && currentGroupName != "")
                {
                    errorMessage += ("Group with name " + currentGroupName + " is not coherent. Same group name should be used only for stages"
                        + "that are next to each other");
                    break;
                }
                if (stages[j].group != currentGroupName)
                {
                    groupEnd = true;
                }
            }
        }
        return errorMessage;
    }

    void addGroupsToPage(JsonWebPage page)
    {
        List<String> groupNames = new List<String>();
        foreach (EditorStage editorStage in stages)
        {
            if (editorStage.group != "")
            {
                if (!groupNames.Contains(editorStage.group))
                {
                    groupNames.Add(editorStage.group);
                }
            }
        }
        page.groups = groupNames;
    }

    string validateStages()
    {
        string errorMessage = "";
        foreach (EditorStage editorStage in stages)
        {
            if (!isUniqueAfterRepair(editorStage))
            {
                errorMessage += "Stage with name: " + editorStage.fName + " does not have unique id. This is necessary for javascript to work properly.\n\n";
            }
        }
        return errorMessage;
    }
}
