using UnityEngine;
using UnityEngine.SceneManagement;

public class SpawnWeaponPhoton : MonoBehaviour
{
    public Vector3[] posOffsets;
    //public Quaternion[] rotOffsets;
    int maxWeapon;

    GameObject Weapon;

    void Awake()
    {
        if (gameObject.GetComponentInParent<PhotonView>().isMine)
        {
            enabled = true;
        }
    }

    void Start()
    {
        maxWeapon = GetComponent<PairManager>().maxPair * 2;
        if (SaveBot.CurrentPresetData.weapons == null)
        {
            Cursor.lockState = CursorLockMode.None;
            PhotonNetwork.Disconnect();
            SceneManager.LoadScene("Creator");
            //SaveBot.TestWSet (maxWeapon);
        }
        else
        {
            SpawnWeapon();
        }
    }

    void SpawnWeapon()
    {
        for (int a = 0; a < maxWeapon; a++)
        {
            Weapon = (GameObject)PhotonNetwork.Instantiate(SaveBot.CurrentPresetData.weapons[a].prefabName, Vector3.zero, Quaternion.identity/*rotOffsets[a%2]*/, 0);
            Weapon.GetComponent<PhotonWeaponIntegrity>().wIndex = a;

            Weapon.transform.SetParent (transform,false);
            Weapon.transform.localPosition = posOffsets[a % 2];
            if (a % 2 == 0)
            {
                Weapon.transform.localScale = new Vector3(-1, 1, 1);
            }
            else
            {
                Weapon.transform.localScale = new Vector3(1, 1, 1);
            }
            Weapon.layer = LayerMask.NameToLayer("MyBot");
        }
    }
}
