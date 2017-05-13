using UnityEngine;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine.UI;

public class SaveBot : MonoBehaviour
{
    static PresetHolder[] presets = new PresetHolder[5];
    static int currentPreset;   //Between 0 and 4

    static PresetHolder DefaultPreset()
    {
        PresetHolder temp = new PresetHolder();

        temp.hull = "Dome";
        temp.movement = "Treads";
        temp.weapons.Add("Drill");
        temp.weapons.Add("Drill");

        return temp;
    }

    static PresetData loadedData;
    public static PresetData CurrentPresetData
    {
        get
        {
            if (loadedData == null)
            {
                if (presets[currentPreset] == null)
                {
                    LoadPresets();
                }
                if (presets[currentPreset] != null)
                {
                    loadedData = new PresetData();

                    //Debug.Log("Loading current preset.");
                    loadedData.hull = Resources.Load<HullData>("Part Database/Hulls/" + presets[currentPreset].hull);
                    loadedData.movement = Resources.Load<MovementData>("Part Database/Movements/" + presets[currentPreset].movement);
                    loadedData.gadget = Resources.Load<GadgetData>("Part Database/Gadgets/" + presets[currentPreset].gadget);
                    for (int a = 0; a < presets[currentPreset].weapons.Count; a++)
                    {
                        loadedData.weapons.Add(Resources.Load<WeaponData>("Part Database/Weapons/" + presets[currentPreset].weapons[a]));
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
    
    public static string activePart { get; private set; }
    public Text presetText;

    public delegate void PartUpdate();
    public static event PartUpdate HullUpdate;
    public static event PartUpdate MoveUpdate;
    public static event PartUpdate GadgetUpdate;
    public static event PartUpdate PairUpdate;
    public static event PartUpdate LeftWeaponUpdate;
    public static event PartUpdate RightWeaponUpdate;

    public delegate void PartTypeUpdate(string type);
    public static event PartTypeUpdate TypeUpdate;

    public static void WeaponUpdates()
    {
        if (LeftWeaponUpdate == null || RightWeaponUpdate == null)
        {
            Debug.LogError("No left or right updates recievers found.");
            WeaponUpdates();
        }
        else
        {
            LeftWeaponUpdate();
            RightWeaponUpdate();
        }
    }

#if UNITY_EDITOR
    void Awake()
    {
        if (presets[currentPreset] == null)
        {
            Debug.Log("Loading presets for editor play mode.");
            LoadPresets();
        }
        
    }
#endif

    void Start()
    {
        ChangeToPreset(currentPreset);
    }

    public static void LoadPresets() {
        for (int a = 0; a < presets.Length; a++){
            if (File.Exists(Application.persistentDataPath + "/Preset" + a)) {
                //Debug.Log("Loadig preset #" + a);
                FileStream presetFile = File.Open(Application.persistentDataPath + "/Preset" + a, FileMode.Open);
                BinaryFormatter biForm = new BinaryFormatter();

                presets[a] = (PresetHolder)biForm.Deserialize(presetFile);
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
        FileStream presetFile = File.Create(Application.persistentDataPath + "/Preset" + currentPreset);
        BinaryFormatter biForm = new BinaryFormatter();

        biForm.Serialize(presetFile, presets[currentPreset]);
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
        currentPreset = val--;
        ChangePreset(true);
    }

    public void ChangePreset(bool increase)
    {
        Helper.ChangeIndexNum(5, currentPreset, increase);
        loadedData = null;

        presetText.text = "Preset " + (currentPreset + 1) + ", " + presets[currentPreset].presetName;

        BroadcastPartUpdate(PartTypes.Hull);
        BroadcastPartUpdate(PartTypes.Movement);
        BroadcastPartUpdate(PartTypes.Gadget);
        BroadcastPartUpdate(PartTypes.Pair);
    }

    public void ChangeActivePartType(string type)
    {
        /*if (type == PartTypes.Hull)
        {
            activePart = type;
        }
        else if(type == PartTypes.Movement)
        {
            activePart = type;
        }
        else if (type == PartTypes.LeftWeapon)
        {
            activePart = type;
        }
        else if (type == PartTypes.RightWeapon)
        {
            activePart = type;
        }
        else if (type == PartTypes.Gadget)
        {
            activePart = type;
        }
        else if (type == null)
        {
            activePart = type;
        }*/

        activePart = type;
        TypeUpdate(activePart);
    }

    public void ChangeToPart(int num)
    {
        PartList list = Resources.Load<PartList>("Part Database/Master List");

        switch (activePart)
        {
            case PartTypes.Hull:
                presets[currentPreset].hull = list.allHulls[num].name;
                
                //Make sure pairs match the pairs of the new hull
                if (presets[currentPreset].weapons.Count < CurrentPresetData.hull.maxPairs * 2)
                {
                    //Debug.Log("Filing in extra spaces in pairs");
                    for (int x = presets[currentPreset].weapons.Count; x < (CurrentPresetData.hull.maxPairs * 2); x++)
                    {
                        presets[currentPreset].weapons.Add("Drill");
                    }
                }
                else if (presets[currentPreset].weapons.Count > CurrentPresetData.hull.maxPairs * 2)
                {
                    //Debug.Log("Removing extra spaces in pairs.");
                    int totalW = CurrentPresetData.hull.maxPairs * 2;
                    int wDif = presets[currentPreset].weapons.Count - totalW;

                    presets[currentPreset].weapons.RemoveRange(totalW, wDif);
                }
                loadedData = null;

                PairUpdate();
                HullUpdate();
                //BroadcastPartUpdate("Hull");
                break;
            case PartTypes.Movement:
                presets[currentPreset].movement = list.allMovements[num].name;
                
                MoveUpdate();
                //BroadcastPartUpdate("Movement");
                break;
            case PartTypes.LeftWeapon:
                presets[currentPreset].weapons[1 + CreatorPairer.indexAdjust] = list.allWeapons[num].name;

                LeftWeaponUpdate();
                break;
            case PartTypes.RightWeapon:
                presets[currentPreset].weapons[0 + CreatorPairer.indexAdjust] = list.allWeapons[num].name;

                RightWeaponUpdate();
                break;
            case PartTypes.Gadget:
                presets[currentPreset].gadget = list.allGadets[num].name;
                
                GadgetUpdate();
                //BroadcastPartUpdate("Gadget");
                break;
            default:
                Debug.LogError("No correct part type for activePart=" + activePart + " found!");
                break;
        }
    }

    public void ChangePart(bool increasing)
    {
        PartList list = Resources.Load<PartList>("Part Database/Master List");
        int a;

        switch (activePart)
        {
            case PartTypes.Hull:
                a = list.allHulls.IndexOf(CurrentPresetData.hull);
                loadedData = null;
                a = Helper.ChangeIndexNum(list.allHulls.Count, a, increasing);

                ChangeToPart(a);
                break;
            case PartTypes.Movement:
                a = list.allMovements.IndexOf(CurrentPresetData.movement);
                loadedData = null;
                a = Helper.ChangeIndexNum(list.allMovements.Count, a, increasing);

                ChangeToPart(a);
                break;
            case PartTypes.LeftWeapon:
                a = list.allWeapons.IndexOf(CurrentPresetData.weapons[1 + CreatorPairer.indexAdjust]);
                loadedData = null;
                a = Helper.ChangeIndexNum(list.allWeapons.Count, a, increasing);

                ChangeToPart(a);
                break;
            case PartTypes.RightWeapon:
                a = list.allWeapons.IndexOf(CurrentPresetData.weapons[0 + CreatorPairer.indexAdjust]);
                loadedData = null;
                a = Helper.ChangeIndexNum(list.allWeapons.Count, a, increasing);

                ChangeToPart(a);
                break;
            case PartTypes.Gadget:
                a = list.allGadets.IndexOf(CurrentPresetData.gadget);
                loadedData = null;
                a = Helper.ChangeIndexNum(list.allGadets.Count, a, increasing);

                ChangeToPart(a);
                break;
            default:
                Debug.LogError("No correct part type for activePart=" + activePart + " found!");
                break;
        }
    }

    public void BroadcastPartUpdate(string changePartType)  //SHOULD PROBABLY REPLACE THIS
    {
        //if (changePart != PartTypes)
        string message = "Update" + changePartType;
        BroadcastMessage(message, SendMessageOptions.RequireReceiver);
    }

    public void SetPressetName (string newName)
    {
        presets[currentPreset].presetName = newName;
        ChangeToPreset(currentPreset);
    }

    //Old code. probably delete all of it
    /*
    void Awake () {
		hulls.value = Hull;
		movements.value = Movement;
		weaponsL.value = Weapons [0];
		weaponsR.value = Weapons [1];
	}

	void Update () {
		x = 2 * (pairNum - 1);
	}

	public void HullValue () {
		Hull = hulls.value;
		PlayerPrefs.SetInt ("Hull", Hull);
	}

	public void PairValue () {
		pairNum = pair.value + 1;
		weaponsL.value = Weapons [0 + x];
		weaponsR.value = Weapons [1 + x];
	}

	public void WLValue () {
		Weapons [0 + x] = weaponsL.value;
	}

	public void WRValue () {
		Weapons [1 + x] = weaponsR.value;
	}

	public void MoveValue () {
		Movement = movements.value;
		PlayerPrefs.SetInt ("Movement", Movement);
	}*/
}

public class PresetData
{
    public string presetName;

    public HullData hull;
    public MovementData movement;
    public List<WeaponData> weapons = new List<WeaponData>();
    public GadgetData gadget;
}