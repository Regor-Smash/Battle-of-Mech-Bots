using UnityEngine;

public class HealthManager : MonoBehaviour
{
    public static float hullHealth;
    public static float hullEnergy;
    public static float moveHealth;

    public static float weaponLHealth;
    public static int weaponLAmmo;
    public static bool weaponLUseEnergy;

    public static float weaponRHealth;
    public static int weaponRAmmo;
    public static bool weaponRUseEnergy;

    public static float hullHealthMax;
    public static float hullEnergyMax;
    public static float moveHealthMax;
    public static float weaponLHealthMax;
    public static int weaponLAmmoMax;
    public static float weaponRHealthMax;
    public static int weaponRAmmoMax;

    //	public static float lowThreshhold = 0.25f;
    //	public static bool hullHealthLow;
    //	public static bool hullEnergyLow;
    //	public static bool moveHealthLow;
    //	public static bool weaponLHealthLow;
    //	public static bool weaponRHealthLow;
    //
    //	void Update () {
    //		if (hullHealth / hullHealthMax <= lowThreshhold) {
    //			hullHealthLow = true;
    //		} else {
    //			hullHealthLow = false;
    //		}
    //		if (moveHealth / moveHealthMax <= lowThreshhold) {
    //			moveHealthLow = true;
    //		} else {
    //			moveHealthLow = false;
    //		}
    //		if (weaponLHealth / weaponLHealthMax <= lowThreshhold) {
    //			weaponLHealthLow = true;
    //		} else {
    //			weaponLHealthLow = false;
    //		}
    //		if (weaponRHealth / weaponRHealthMax <= lowThreshhold) {
    //			weaponRHealthLow = true;
    //		} else {
    //			weaponRHealthLow = false;
    //		}
    //	}
}
