using UnityEngine;

public class CreatorManager : MonoBehaviour
{
    public static string activePart { get; private set; }

    public delegate void PartUpdate();
    public static event PartUpdate HullUpdate;
    public static event PartUpdate MoveUpdate;
    public static event PartUpdate GadgetUpdate;
    public static event PartUpdate PairUpdate;
    public static event PartUpdate LeftWeaponUpdate;
    public static event PartUpdate RightWeaponUpdate;

    public delegate void PartTypeUpdate(string type);
    public static event PartTypeUpdate TypeUpdate;

    private void Awake()
    {
        HullUpdate += PairUpdate;
    }

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

    public void ChangeActivePartType(string type)
    {
        activePart = type;
        TypeUpdate(activePart);
    }

    public void ChangeToPart(int num)
    {
        PartList list = Resources.Load<PartList>("Part Database/Master List");

        switch (activePart)
        {
            case PartTypes.Hull:
                SaveBot.SetHull(list.allHulls[num].name);
                HullUpdate();
                //BroadcastPartUpdate("Hull");
                break;
            case PartTypes.Movement:
                SaveBot.SetMovement(list.allMovements[num].name);
                MoveUpdate();
                //BroadcastPartUpdate("Movement");
                break;
            case PartTypes.LeftWeapon:
                SaveBot.SetLeftWeapon(list.allWeapons[num].name);
                LeftWeaponUpdate();
                break;
            case PartTypes.RightWeapon:
                SaveBot.SetRightWeapon(list.allWeapons[num].name);
                RightWeaponUpdate();
                break;
            case PartTypes.Gadget:
               SaveBot.SetGadget(list.allGadets[num].name);
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
        int index;

        switch (activePart)
        {
            case PartTypes.Hull:
                index = list.allHulls.IndexOf(SaveBot.CurrentPresetData.hull);
                index = Helper.ChangeIndexNum(list.allHulls.Count, index, increasing);

                ChangeToPart(index);
                break;
            case PartTypes.Movement:
                index = list.allMovements.IndexOf(SaveBot.CurrentPresetData.movement);
                index = Helper.ChangeIndexNum(list.allMovements.Count, index, increasing);

                ChangeToPart(index);
                break;
            case PartTypes.LeftWeapon:
                index = list.allWeapons.IndexOf(SaveBot.CurrentPresetData.weapons[1 + CreatorPairer.indexAdjust]);
                index = Helper.ChangeIndexNum(list.allWeapons.Count, index, increasing);

                ChangeToPart(index);
                break;
            case PartTypes.RightWeapon:
                index = list.allWeapons.IndexOf(SaveBot.CurrentPresetData.weapons[0 + CreatorPairer.indexAdjust]);
                index = Helper.ChangeIndexNum(list.allWeapons.Count, index, increasing);

                ChangeToPart(index);
                break;
            case PartTypes.Gadget:
                index = list.allGadets.IndexOf(SaveBot.CurrentPresetData.gadget);
                index = Helper.ChangeIndexNum(list.allGadets.Count, index, increasing);

                ChangeToPart(index);
                break;
            default:
                Debug.LogError("No correct part type for activePart=" + activePart + " found!");
                break;
        }
    }

    /*public void BroadcastPartUpdate(string changePartType)  //SHOULD PROBABLY REPLACE THIS
    {
        //if (changePart != PartTypes)
        string message = "Update" + changePartType;
        BroadcastMessage(message, SendMessageOptions.RequireReceiver);
    }*/
}
