using UnityEngine;

public abstract class WeaponTemplate : MonoBehaviour
{
    protected int Slot;
    protected string ammoType;

    protected WeaponData data;
    protected PhotonView myView;
    protected PhotonHullManager hullMang;

    protected float damage;
    protected float fireRate;
    protected float range;
    protected float force;
    protected float cost;
    protected int maxAmmo;
    public int ammo { get; protected set; }
    
    protected virtual void Awake()
    {
        myView = GetComponentInParent<PhotonView>();
        if (myView.isMine)
        {
            enabled = true;

            //Get all variables from scriptable object data holder
            //(data holder set by actual weapon)
            fireRate = data.RoF;
            damage = data.damage;
            range = data.range;
            force = data.force;

            if (data.ammoType == WeaponData.AmmoTypes.Energy)
            {
                ammoType = "Energy";

                cost = data.ammoMax;
            }
            else if (data.ammoType == WeaponData.AmmoTypes.Standard)
            {
                ammoType = "Standard";

                maxAmmo = (int)data.ammoMax;
                ammo = maxAmmo;
            }
        }
        else
        {
            Destroy(this);
        }
    }

    protected virtual void Start()
    {
        Slot = (GetComponentInParent<PhotonWeaponIntegrity>().wIndex % 2) + 1;

        hullMang = GetComponentInParent<PhotonHullManager>();
    }


    protected abstract void Fire();

    protected void CheckAmmo()
    {
        if (ammoType == "Energy")
        {
            if (hullMang.energy >= cost)
            {
                hullMang.UseEnergy(cost);
                //UpdateAmmo();

                Fire();
            }
        }
        else if (ammoType == "Standard")
        {
            if (ammo > 0)
            {
                ammo--;
                UpdateAmmo();

                Fire();
            }
        }
    }


    protected void AddAmmo()
    {
        ammo = maxAmmo;
        UpdateAmmo();
    }

    protected void UpdateAmmo()
    {
        /*if (ammoType == "Energy")
        {
            if (Slot == 2)
            {
                HealthManager.weaponRUseEnergy = true;
            }
            else if (Slot == 1)
            {
                HealthManager.weaponLUseEnergy = true;
            }
        }
        else*/
        if (ammoType == "Standard")
        {
            if (Slot == 2)
            {
                HealthManager.weaponRUseEnergy = false;
                HealthManager.weaponRAmmoMax = maxAmmo;
                HealthManager.weaponRAmmo = ammo;
            }
            else if (Slot == 1)
            {
                HealthManager.weaponLUseEnergy = false;
                HealthManager.weaponLAmmoMax = maxAmmo;
                HealthManager.weaponLAmmo = ammo;
            }
        }
        
    }
}
