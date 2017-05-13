using System;
using System.Collections.Generic;

[Serializable]
public class PresetHolder {
    public string presetName;

    public string hull;
    public string movement;
    public List<string> weapons = new List<string> ();
    public string gadget;
}
