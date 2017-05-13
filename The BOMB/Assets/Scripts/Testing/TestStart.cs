using UnityEngine;

public class TestStart : MonoBehaviour
{
#if UNITY_EDITOR
    /*public HullData hull;
    public MovementData move;
    public WeaponData weaponL;
    public WeaponData weaponR;

    public bool offline;

    void Awake()
    {
        Debug.Log("Bot Tester is Awake");
        Options.isOffline = offline;
    }

    void Start()
    {
        SaveBot.CurrentPresetData.hull = hull;
        SaveBot.CurrentPresetData.movement = move;

        SaveBot.CurrentPresetData.weapons.Clear();
        SaveBot.CurrentPresetData.weapons[0] = weaponL;
        SaveBot.CurrentPresetData.weapons[1] = weaponR;
    }*/

    private void Awake()
    {
        if (Options.sensitivity == 0)
        {
            Debug.LogWarning("Activating test start.");
            LoadPrefs tempLoad= gameObject.AddComponent<LoadPrefs>();
            Destroy(this, 1);
            Destroy(tempLoad, 1);
        }
    }

#else
    private void Awake()
    {
        Destroy(this);
    }
#endif
}
