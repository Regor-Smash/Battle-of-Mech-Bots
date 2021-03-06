﻿using UnityEngine;

public class CreatorWeaponRight : MonoBehaviour
{
    GameObject weapon;

    void Awake()
    {
        SaveBot.RightWeaponUpdate += UpdateWeaponR;
    }

    void UpdateWeaponR()
    {
        if (weapon != null)
        {
            Destroy(weapon);
        }
        weapon = (GameObject)Instantiate(SaveBot.CurrentPresetData.weapons[0 + CreatorPairer.indexAdjust].partVisuals,
            transform.position, transform.rotation);
    }

    private void OnDestroy()
    {
        SaveBot.RightWeaponUpdate -= UpdateWeaponR;
    }
}
