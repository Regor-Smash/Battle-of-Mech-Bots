using UnityEngine;
using System.Collections;

public class PhotonRocketLaunch : WeaponTemplate
{
    public GameObject launcherBarrel;

    float timeStamp;
    int barrelNum = 0;

    protected override void Awake()
    {
        data = Resources.Load<WeaponData>("Part Database/Weapons/Rocket Apparatus");
        base.Awake();
    }

    void Update()
    {
        if (Input.GetButton("Fire" + Slot) && Time.time >= timeStamp && !MultiplayerPause.isPaused)
        {
            CheckAmmo();
        }
    }

    protected override void Fire()
    {
        PhotonNetwork.Instantiate("PhotonRocket", transform.position, transform.rotation, 0);
        timeStamp = Time.time + fireRate;
        barrelNum++;
        if (barrelNum > 4)
        {
            barrelNum = 1;
        }

        StartCoroutine("RotateBarrel");
    }

    private IEnumerator RotateBarrel()
    {
        //new WaitForSeconds(fireRate / 2);

        //float startTime = Time.time;
        float endTime = Time.time + (fireRate/* / 2*/);

        while (Time.time <= endTime)
        {
            float difference = (Time.deltaTime / (fireRate/* / 2*/)) * 90;
            launcherBarrel.transform.Rotate(0, 0, difference);

            yield return new WaitForEndOfFrame();
        }
        
        launcherBarrel.transform.localRotation = Quaternion.Euler(0, 0, 90 * barrelNum);
    }
}
