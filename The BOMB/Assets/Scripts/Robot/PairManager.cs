using UnityEngine;

public class PairManager : MonoBehaviour
{
    public int maxPair;
    int currentPair = 1;
    GameObject[] weapons;
    int x;

    void Awake()
    {
        if (gameObject.GetComponentInParent<PhotonView>().isMine)
        {
            enabled = true;
        }
    }

    void OnEnable()
    {
        System.Array.Resize(ref weapons, maxPair * 2);

        Invoke("FindWeapons", 0.01f);
    }

    void FindWeapons()
    {
        for (int a = 0; a < weapons.Length; a++)
        {
            weapons[a] = gameObject.transform.GetChild(a).gameObject;
        }
        
        SwitchPair();
    }

    void Update()
    {
        if (Input.GetAxis("Mouse ScrollWheel") > 0 && currentPair < maxPair && !MultiplayerPause.isPaused)
        {
            currentPair = currentPair + 1;
            SwitchPair();
        }
        else if (Input.GetAxis("Mouse ScrollWheel") < 0 && currentPair > 1 && !MultiplayerPause.isPaused)
        {
            currentPair = currentPair - 1;
            SwitchPair();
        }
    }

    void SwitchPair()
    {
        x = 2 * (currentPair - 1);

        foreach (GameObject go in weapons)
        {
            go.GetComponent<PhotonView>().RPC("ToggleActive", PhotonTargets.AllBuffered, false);
        }

        //Left Weapon
        weapons[0 + x].GetComponent<PhotonView>().RPC("ToggleActive", PhotonTargets.AllBuffered, true);
        HealthManager.weaponLHealth = weapons[0 + x].GetComponent<PhotonWeaponIntegrity>().integrity;
        HealthManager.weaponLHealthMax = weapons[0 + x].GetComponent<PhotonWeaponIntegrity>().maxWeaponIntegrity;
        weapons[0 + x].BroadcastMessage("UpdateAmmo", SendMessageOptions.RequireReceiver);

        //Right Weapon
        weapons[1 + x].GetComponent<PhotonView>().RPC("ToggleActive", PhotonTargets.AllBuffered, true);
        HealthManager.weaponRHealth = weapons[1 + x].GetComponent<PhotonWeaponIntegrity>().integrity;
        HealthManager.weaponRHealthMax = weapons[1 + x].GetComponent<PhotonWeaponIntegrity>().maxWeaponIntegrity;
        weapons[1 + x].BroadcastMessage("UpdateAmmo", SendMessageOptions.RequireReceiver);
    }

    public void WeaponDied(int index)
    {
        weapons[index] = null;
    }
}
