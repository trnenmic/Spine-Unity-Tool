using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class JsonInput {

    public string type; //KEY, BUTTON
    public string keyCode;
    public string buttonId;
    public string controls;

	public JsonInput(string type = "KEY+BUTTON", string keyCode = "e", string buttonId = "button_e", string controls = "walk")
    {
        this.type = type;
        this.keyCode = keyCode;
        this.buttonId = buttonId;
        this.controls = controls;
    }
}
