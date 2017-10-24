using UnityEngine;

public class GatlinGunRotate : MonoBehaviour
{

    /*[HideInInspector]
	public*/
    int Slot;

    float spinSpeed;

    void Awake()
    {
        if (gameObject.GetComponentInParent<PhotonView>().isMine)
        {
            enabled = true;
            spinSpeed = 1 / Resources.Load<WeaponData>("Part Database/Weapons/Gatling Gun").RoF;
        }
    }

    void Start()
    {
        Slot = (GetComponentInParent<PhotonWeaponIntegrity>().wIndex % 2) + 1;
    }

    void Update()
    {
        if (Input.GetButton("Fire" + Slot) && !MultiplayerPause.isPaused)
        {
            transform.Rotate(0, 0, spinSpeed);
        }
    }
}
