using UnityEngine;

public class CreatorWeaponLeft : MonoBehaviour
{
    GameObject weapon;

    void Awake()
    {
        CreatorManager.LeftWeaponUpdate += UpdateWeaponL;
    }

    void UpdateWeaponL()
    {
        if (weapon != null)
        {
            Destroy(weapon);
        }
        weapon = (GameObject)Instantiate(SaveBot.CurrentPresetData.weapons[1 + CreatorPairer.indexAdjust].partVisuals,
            transform.position, transform.rotation);
        weapon.transform.localScale = new Vector3(-1, 1, 1);
    }

    private void OnDestroy()
    {
        CreatorManager.LeftWeaponUpdate -= UpdateWeaponL;
    }
}
