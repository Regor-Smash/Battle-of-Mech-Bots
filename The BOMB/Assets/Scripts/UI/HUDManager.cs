using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class HUDManager : MonoBehaviour
{

    Slider bar;

    public enum PartTypes
    {
        HullInteg, MoveInteg, WLInteg, WRInteg, HullEnergy, WLAmmo, WRAmmo
    }
    public PartTypes partType;

    void Start()
    {
        bar = GetComponent<Slider>();

        /*switch (partType)
        {
            case PartTypes.HullInteg:
                bar.maxValue = HealthManager.hullHealthMax;
                break;
            case PartTypes.MoveInteg:
                bar.maxValue = HealthManager.moveHealthMax;
                break;
            case PartTypes.WLInteg:
                bar.maxValue = HealthManager.weaponLHealthMax;
                break;
            case PartTypes.WRInteg:
                bar.maxValue = HealthManager.weaponRHealthMax;
                break;
            case PartTypes.HullEnergy:
                bar.maxValue = HealthManager.hullEnergyMax;
                break;
            case PartTypes.WLAmmo:
                bar.maxValue = HealthManager.weaponLAmmoMax;
                break;
            case PartTypes.WRAmmo:
                bar.maxValue = HealthManager.weaponRAmmoMax;
                break;
            default:
                Debug.Log("Type enum on" + name + "does not match.");
                break;
        }*/
    }

    void Update()
    {
        switch (partType)
        {
            case PartTypes.HullInteg:
                if (bar.maxValue == 0)
                {
                    bar.maxValue = HealthManager.hullHealthMax;
                }
                bar.value = HealthManager.hullHealth;
                break;
            case PartTypes.MoveInteg:
                if(bar.maxValue == 0)
                {
                    bar.maxValue = HealthManager.moveHealthMax;
                }
                bar.value = HealthManager.moveHealth;
                break;
            case PartTypes.WLInteg:
                bar.maxValue = HealthManager.weaponLHealthMax;
                bar.value = HealthManager.weaponLHealth;
                break;
            case PartTypes.WRInteg:
                bar.maxValue = HealthManager.weaponRHealthMax;
                bar.value = HealthManager.weaponRHealth;
                break;
            case PartTypes.HullEnergy:
                if (bar.maxValue == 0)
                {
                    bar.maxValue = HealthManager.hullEnergyMax;
                }
                bar.value = HealthManager.hullEnergy;
                break;
            case PartTypes.WLAmmo:
                bar.maxValue = HealthManager.weaponLAmmoMax;
                bar.value = HealthManager.weaponLAmmo;
                if (HealthManager.weaponLUseEnergy)
                {
                    transform.FindChild("Background").gameObject.SetActive(false);
                    transform.FindChild("Fill Area").gameObject.SetActive(false);
                }
                else
                {
                    transform.FindChild("Background").gameObject.SetActive(true);
                    transform.FindChild("Fill Area").gameObject.SetActive(true);
                }
                break;
            case PartTypes.WRAmmo:
                bar.maxValue = HealthManager.weaponRAmmoMax;
                bar.value = HealthManager.weaponRAmmo;
                if (HealthManager.weaponRUseEnergy)
                {
                    transform.FindChild("Background").gameObject.SetActive(false);
                    transform.FindChild("Fill Area").gameObject.SetActive(false);
                }
                else
                {
                    transform.FindChild("Background").gameObject.SetActive(true);
                    transform.FindChild("Fill Area").gameObject.SetActive(true);
                }
                break;
            default:
                Debug.Log("Type enum on" + name + "does not match.");
                break;
        }
    }
}
