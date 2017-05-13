using UnityEngine;

public class PhotonRocketLaunch : WeaponTemplate
{
    float timeStamp;

    protected override void Awake()
    {
        data = Resources.Load<WeaponData>("Part Database/Weapons/Rocket Apparatus");
        base.Awake();
    }

    void Update()
    {
        if (Input.GetButtonDown("Fire" + Slot) && Time.time >= timeStamp && !MultiplayerPause.isPaused)
        {
            CheckAmmo();
        }
    }

    protected override void Fire()
    {
        PhotonNetwork.Instantiate("PhotonRocket", transform.position, transform.rotation, 0);
        timeStamp = Time.time + fireRate;
    }
}
