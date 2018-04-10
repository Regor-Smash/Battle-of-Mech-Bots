using UnityEngine;
using UnityEngine.UI;

public class CreatorStatPanel : MonoBehaviour
{
    public Text title;
    public Text stats;
    
    void Awake()
    {
        CreatorManager.HullUpdate += UpdateHullStats;
        CreatorManager.MoveUpdate += UpdateMoveStats;
        CreatorManager.GadgetUpdate += UpdateGadgetStats;
        CreatorManager.LeftWeaponUpdate += UpdateWeaponLStats;
        CreatorManager.RightWeaponUpdate += UpdateWeaponRStats;

        CreatorManager.TypeUpdate += UpdateType;
    }

    public void UpdateType(string type)
    {
        if (type == PartTypes.Hull)
        {
            UpdateHullStats();
        }
        else if (type == PartTypes.Movement)
        {
            UpdateMoveStats();
        }
        else if(type == PartTypes.Gadget)
        {
            UpdateGadgetStats();
        }
        else if (type == PartTypes.LeftWeapon)
        {
            UpdateWeaponLStats();
        }
        else if (type == PartTypes.RightWeapon)
        {
            UpdateWeaponRStats();
        }
        else
        {
            title.text = "Name";
            stats.text = "Integrity = ?";
        }
    }
    
    void UpdateHullStats()
    {
        //if(saver.activePart == "Hull")
        HullData data = SaveBot.CurrentPresetData.hull;
        title.text = data.partName;
        stats.text = "Integrity = " + data.maxIntegrity +
            "\nEnergy = " + data.maxEnergy +
            "\nMass = " + data.mass +
            "\nPairs = " + data.maxPairs;
        if (data.size != 1)
        {
            stats.text += "\nSize = " + data.size + "x";
        }
        
    }

    void UpdateMoveStats()
    {
        //if (saver.activePart == "Movement")
        MovementData data = SaveBot.CurrentPresetData.movement;
        title.text = data.partName;
        stats.text = "Integrity = " + data.integrityMultiplier * 100 + "%";
        if (data.capacity > 0)
        {
            stats.text += "\nCapacity = " + data.capacity;
        }
        else
        {
            stats.text += "\nCapacity = Infinite!";
        }
        stats.text += "\nPower = " + data.power +
            "\nJump = " + data.jumpPower +
            "\nMax Speed = " + data.maxSpeed.z +
            "\nMin Speed = " + data.minSpeed.z;
    }

    void UpdateGadgetStats()
    {
        //if (saver.activePart == "Gadget")
        GadgetData data = SaveBot.CurrentPresetData.gadget;
        title.text = data.partName;
        stats.text = "WIP";
    }

    void UpdateWeaponLStats()
    {
        if (CreatorManager.activePart == "WeaponL")
        {
            WeaponData data = SaveBot.CurrentPresetData.weapons[1 + CreatorPairer.indexAdjust];
            title.text = data.partName;
            stats.text = "Integrity = " + data.integrityMultiplier * 100 + "%" +
                "\nAmmo Type = " + data.ammoType;
            if (data.ammoType == WeaponData.AmmoTypes.Standard)
            {
                stats.text += "\nAmmo = " + data.ammoMax;
            }
            else if (data.ammoType == WeaponData.AmmoTypes.Energy)
            {
                if (data.RoF < 1)
                {
                    stats.text += "\nE Cost = " + data.ammoMax / data.RoF + "/sec";
                }
                else if (data.RoF > 1)
                {
                    stats.text += "\nE Cost = " + data.ammoMax;
                }
            }
            if (data.RoF < 1)
            {
                stats.text += "\nRate of Fire = " + 1 / data.RoF + "/sec";
            }
            else if (data.RoF > 1)
            {
                stats.text += "\nRate of Fire = " + data.RoF + " sec";
            }
            stats.text += "\nDamage = " + data.damage +
                "\nRange = " + data.range;
            if (data.force > 0)
            {
                stats.text += "\nForce = " + data.force;
            }
            if (data.projectileSpeed > 0)
            {
                stats.text += "\nProjectile Speed = " + data.projectileSpeed;
            }
            if (data.specialDesc != "")
            {
                stats.text += "\n" + data.specialDesc + " = " + data.special;
            }
        }
    }

    void UpdateWeaponRStats()
    {
        if (CreatorManager.activePart == "WeaponR")
        {
            WeaponData data = SaveBot.CurrentPresetData.weapons[0 + CreatorPairer.indexAdjust];
            title.text = data.partName;
            stats.text = "Integrity = " + data.integrityMultiplier * 100 + "%" +
                "\nAmmo Type = " + data.ammoType;
            if (data.ammoType == WeaponData.AmmoTypes.Standard)
            {
                stats.text += "\nAmmo = " + data.ammoMax;
            }
            else if (data.ammoType == WeaponData.AmmoTypes.Energy)
            {
                if (data.RoF < 1)
                {
                    stats.text += "\nE Cost = " + data.ammoMax / data.RoF + "/sec";
                }
                else if (data.RoF > 1)
                {
                    stats.text += "\nE Cost = " + data.ammoMax;
                }
            }
            if (data.RoF < 1)
            {
                stats.text += "\nRate of Fire = " + 1 / data.RoF + "/sec";
            }
            else if (data.RoF > 1)
            {
                stats.text += "\nRate of Fire = " + data.RoF + " sec";
            }
            stats.text += "\nDamage = " + data.damage +
                "\nRange = " + data.range;
            if (data.force > 0)
            {
                stats.text += "\nForce = " + data.force;
            }
            if (data.projectileSpeed > 0)
            {
                stats.text += "\nBullet Speed = " + data.projectileSpeed;
            }
            if (data.specialDesc != "")
            {
                stats.text += "\n" + data.specialDesc + " = " + data.special;
            }
        }
    }

    private void OnDestroy()
    {
        CreatorManager.HullUpdate -= UpdateHullStats;
        CreatorManager.MoveUpdate -= UpdateMoveStats;
        CreatorManager.GadgetUpdate -= UpdateGadgetStats;
        CreatorManager.LeftWeaponUpdate -= UpdateWeaponLStats;
        CreatorManager.RightWeaponUpdate -= UpdateWeaponRStats;

        CreatorManager.TypeUpdate -= UpdateType;
    } 
}
