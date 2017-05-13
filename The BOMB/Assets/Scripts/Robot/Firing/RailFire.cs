using UnityEngine;

public class RailFire : WeaponTemplate
{
    float chargeRate;
    public GameObject chargeEffect;
    //GameObject railBullet;
    
    protected override void Awake()
    {
        data = Resources.Load<WeaponData>("Part Database/Weapons/Rail Gun");
        base.Awake();
        chargeRate = fireRate;
    }

    void Update()
    {
        if (Input.GetButtonDown("Fire" + Slot) && !MultiplayerPause.isPaused && ammo > 0)
        {
            Invoke("CheckAmmo", chargeRate);
            chargeEffect.SetActive(true);
        }
        if (Input.GetButtonUp("Fire" + Slot) || MultiplayerPause.isPaused)
        {
            CancelInvoke("CheckAmmo");
            chargeEffect.SetActive(false);
        }
    }

    protected override void Fire()
    {
        chargeEffect.SetActive(false);

        /*railBullet = */
        PhotonNetwork.Instantiate("RailBullet", transform.position, transform.rotation, 0);
    }
}
