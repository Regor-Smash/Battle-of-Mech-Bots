using UnityEngine;
using System.Collections;

[AddComponentMenu("Integrity/Wepaon Integrity", 3)]
public class PhotonWeaponIntegrity : MonoBehaviour, IntegrityInterface
{
    WeaponData data;

    public float integrity { get; protected set; }

    [HideInInspector] public float maxWeaponIntegrity;
    [HideInInspector] public int wIndex;

    bool isReady = false;

    void Awake()
    {
        if (gameObject.GetComponentInParent<PhotonView>().isMine)
        {
            enabled = true;
        }
    }

    void Start()
    {
        data = SaveBot.CurrentPresetData.weapons[wIndex];
    }

    private IEnumerator OnTransformParentChanged()
    {
        Debug.Log("a");
        while (integrity == 0)
        {
            Debug.Log("b");
            if (data != null)
            {
                maxWeaponIntegrity = data.integrityMultiplier * GetComponentInParent<PhotonHullManager>().integrity;
                integrity = maxWeaponIntegrity;
            }

            yield return new WaitForEndOfFrame();
        }

        isReady = true;
    }

    public void Die(PhotonPlayer killer)
    {
        if (killer != null && killer.isLocal)
        {
            ScoreKeeper.Score += ScoreKeeper.destroyPoints;
        }

        GetComponentInParent<PairManager>().WeaponDied(wIndex);

        if (GetComponent<PhotonView>().isMine)
        {
            PhotonNetwork.Destroy(gameObject);
        }
        else if (!GetComponent<PhotonView>())
        {
            Destroy(gameObject, 0);
        }
    }

    [PunRPC]
    public void TakeDamage(float dam, PhotonPlayer player)
    {
        if (isReady == true)
        {
            integrity = integrity - dam;
            if (integrity <= 0)
            {
                Die(player);
            }
            else if (wIndex%2 == 1)
            {
                HealthManager.weaponLHealth = integrity;
            }
            else if (wIndex % 2 == 0)
            {
                HealthManager.weaponRHealth = integrity;
            }

        }
    }
}
