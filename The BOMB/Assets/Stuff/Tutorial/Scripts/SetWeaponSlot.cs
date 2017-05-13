using UnityEngine;
using System.Collections;

public class SetWeaponSlot : MonoBehaviour
{
    public enum WeaponSides { NotRight, NotLeft }   //Because enums are weird
    public WeaponSides weaponSide;

    void Awake()
    {
        GetComponent<PhotonWeaponIntegrity>().wIndex = (int)weaponSide;
    }
}
