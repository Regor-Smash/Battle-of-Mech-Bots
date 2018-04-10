using System;
using System.Collections.Generic;

[Serializable]
public class BotOutline
{
    //Only holds names of parts
    public string presetName;

    public string hull;
    public string movement;
    public List<string> weapons = new List<string> ();
    public string gadget;
    //public string module;
}

public class BotData
{
    //Holds all data for parts
    public string presetName;

    public HullData hull;
    public MovementData movement;
    public List<WeaponData> weapons = new List<WeaponData>();
    public GadgetData gadget;
    //public ModuleData module;

    public BotOutline Outline()
    {
        BotOutline outline = new BotOutline();

        outline.hull = hull.name;
        outline.movement = movement.name;
        outline.gadget = gadget.name;
        //outline.module = module.name;

        foreach (WeaponData weapon in weapons)
        {
            outline.weapons.Add(weapon.name);
        }

        return outline;
    }
}
