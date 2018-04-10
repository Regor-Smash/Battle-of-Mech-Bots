using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine.UI;

public class SaveBot : MonoBehaviour
{
    static BotOutline[] presets = new BotOutline[5];
    static int presetIndex;   //Between 0 and 4
    static BotOutline currentPreset
    {
        get
        {
            return presets[presetIndex];
        }

        set
        {
            presets[presetIndex] = value;
            resetCurrentData();
        }
    }

    static BotOutline DefaultPreset()
    {
        BotOutline temp = new BotOutline();

        temp.hull = "Dome";
        temp.movement = "Treads";
        temp.weapons.Add("Drill");
        temp.weapons.Add("Drill");

        return temp;
    }

    static BotData loadedData;
    public static BotData CurrentPresetData
    {
        get
        {
            if (loadedData == null)
            {
                if (currentPreset == null)
                {
                    LoadPresets();
                }
                if (currentPreset != null)
                {
                    loadedData = new BotData();

                    //Debug.Log("Loading current preset.");
                    loadedData.hull = Resources.Load<HullData>("Part Database/Hulls/" + currentPreset.hull);
                    loadedData.movement = Resources.Load<MovementData>("Part Database/Movements/" + currentPreset.movement);
                    loadedData.gadget = Resources.Load<GadgetData>("Part Database/Gadgets/" + currentPreset.gadget);
                    for (int a = 0; a < currentPreset.weapons.Count; a++)
                    {
                        loadedData.weapons.Add(Resources.Load<WeaponData>("Part Database/Weapons/" + currentPreset.weapons[a]));
                    }

                    return loadedData;
                }
                else
                {
                    Debug.LogError("Couldn't load data, no preset foud!");
                    return null;
                }
            }
            else
            {
                return loadedData;
            }
        }
    }
    public static void resetCurrentData()
    {
        loadedData = null;
    }

    public Text presetText;

#if UNITY_EDITOR
    void Awake()
    {
        if (currentPreset == null)
        {
            Debug.Log("Loading presets for editor play mode.");
            LoadPresets();
        }
        
    }
#endif

    void Start()
    {
        ChangeToPreset(presetIndex);
    }

    public static void LoadPresets() {
        for (int a = 0; a < presets.Length; a++){
            if (File.Exists(Application.persistentDataPath + "/Preset" + a)) {
                //Debug.Log("Loadig preset #" + a);
                FileStream presetFile = File.Open(Application.persistentDataPath + "/Preset" + a, FileMode.Open);
                BinaryFormatter biForm = new BinaryFormatter();

                presets[a] = (BotOutline)biForm.Deserialize(presetFile);
                presetFile.Close();
            } else {
                Debug.LogError("Preset #" + a + " not found! Recreating all presets.");
                SaveAllPresets();
                break;
            }
        }
    }

    //Save one preset at a time
    public void SaveCurrentPreset() {
        FileStream presetFile = File.Create(Application.persistentDataPath + "/Preset" + presetIndex);
        BinaryFormatter biForm = new BinaryFormatter();

        biForm.Serialize(presetFile, currentPreset);
        presetFile.Close();
    }

    //Save all presents at once
    public static void SaveAllPresets() {
        for (int a = 0; a < presets.Length; a++) {
            //Debug.Log("Saving preset #" + a);
            FileStream presetFile = File.Create(Application.persistentDataPath + "/Preset" + a);
            BinaryFormatter biForm = new BinaryFormatter();

            if (presets[a] == null)
            {
                presets[a] = DefaultPreset();
            }

            biForm.Serialize(presetFile, presets[a]);
            presetFile.Close();
        }
    }

    public void ChangeToPreset (int val)
    {
        presetIndex = val--;
        ChangePreset(true);
    }

    public void ChangePreset(bool increase)
    {
        Helper.ChangeIndexNum(5, presetIndex, increase);
        resetCurrentData();

        presetText.text = "Preset " + (presetIndex + 1) + ", " + currentPreset.presetName;

        //CreatorManager.HullUpdate();

    }

    public void SetPressetName (string newName)
    {
        currentPreset.presetName = newName;
        ChangeToPreset(presetIndex);
    }

    public static void SetHull(string name)
    {
        currentPreset.hull = name;

        //Make sure pairs match the pairs of the new hull
        if (currentPreset.weapons.Count < CurrentPresetData.hull.maxPairs * 2)
        {
            //Debug.Log("Filing in extra spaces in pairs");
            for (int x = currentPreset.weapons.Count; x < (CurrentPresetData.hull.maxPairs * 2); x++)
            {
                currentPreset.weapons.Add("Drill");
            }
        }
        else if (currentPreset.weapons.Count > CurrentPresetData.hull.maxPairs * 2)
        {
            //Debug.Log("Removing extra spaces in pairs.");
            int totalW = SaveBot.CurrentPresetData.hull.maxPairs * 2;
            int wDif = currentPreset.weapons.Count - totalW;

            currentPreset.weapons.RemoveRange(totalW, wDif);
        }
        resetCurrentData();
    }

    public static void SetMovement(string name)
    {
        currentPreset.movement = name;
    }

    public static void SetLeftWeapon(string name)
    {
        currentPreset.weapons[1 + CreatorPairer.indexAdjust] = name;
    }

    public static void SetRightWeapon(string name)
    {
        currentPreset.weapons[0 + CreatorPairer.indexAdjust] = name;
    }

    public static void SetGadget(string name)
    {

    }
}