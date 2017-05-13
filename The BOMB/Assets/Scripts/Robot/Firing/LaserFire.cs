using UnityEngine;

public class LaserFire : WeaponTemplate
{
    LineRenderer line;
    Collider Col;
    bool colDied;

    //public float strayMod;
    //Vector3 bulletStray;
    Ray ray;
    RaycastHit hit;

    AudioSource sound;

    protected override void Awake()
    {
        data = Resources.Load<WeaponData>("Part Database/Weapons/Laser");
        base.Awake();

        line = GetComponent<LineRenderer>();
        line.enabled = false;

        sound = GetComponent<AudioSource>();

        colDied = false;
    }

    void Update()
    {
        if (Input.GetButtonDown("Fire" + Slot) && !MultiplayerPause.isPaused && hullMang.energy >= cost)
        {
            line.enabled = true;
            InvokeRepeating("CheckAmmo", 0, fireRate);

            sound.Play();
        }

        if (Input.GetButtonUp("Fire" + Slot) || MultiplayerPause.isPaused || hullMang.energy < cost)
        {
            line.enabled = false;
            CancelInvoke("CheckAmmo");

            sound.Stop();
        }
    }

    protected override void Fire()
    {
        line.SetPosition(0, Vector3.zero);

        //float x = Random.Range (-strayMod,strayMod);
        //float y = Random.Range (-strayMod,strayMod);
        //float z = Random.Range (-strayMod,strayMod);
        //bulletStray = new Vector3 (x, y, z);
        ray = new Ray(transform.position, transform.forward/* + bulletStray*/);

        if (Physics.Raycast(ray, out hit, range))
        {
            line.SetPosition(1, transform.InverseTransformPoint(hit.point));
            Col = hit.collider;

            if (!Col.tag.Contains("My") && !colDied)
            {
                if (Col.GetComponent<IntegrityInterface>() != null)
                {
                    if (Col.GetComponent<IntegrityInterface>().integrity > 0)
                    {
                        Col.GetComponent<PhotonView>().RPC("TakeDamage", PhotonTargets.AllBufferedViaServer, damage, myView.owner);
                    }
                    else
                    {
                        Debug.Log(Col.name + " should be dead");
                    }
                }

                /*if (Col.GetComponent<PhotonIntegrity>())
                {
                    if (Col.GetComponent<PhotonIntegrity>().GetIntegrity() - damage <= 0 && Col.GetComponent<PhotonIntegrity>().GetIntegrity() > 0)
                    {
                        ScoreKeeper.Score = ScoreKeeper.Score + ScoreKeeper.destroyPoints;
                        colDied = true;
                    }
                    if (Col.GetComponent<PhotonIntegrity>().GetIntegrity() > 0)
                    {
                        Col.GetComponent<PhotonView>().RPC("TakeDamage", PhotonTargets.AllBufferedViaServer, damage);
                    }
                }
                else if (Col.GetComponent<PhotonHullManager>())
                {
                    if (Col.GetComponent<PhotonHullManager>().GetIntegrity() - damage <= 0)
                    {
                        colDied = true;
                    }
                    if (Col.GetComponent<PhotonHullManager>().GetIntegrity() > 0)
                    {
                        Col.GetComponent<PhotonView>().RPC("TakeHullDamage", PhotonTargets.AllBufferedViaServer, damage, GetComponentInParent<PhotonView>());
                    }
                }*/
                if (!Col.GetComponent<PhotonHullManager>() && !Col.GetComponent<PhotonIntegrity>() && colDied)
                {
                    colDied = false;
                }
            }
        }
        else
        {
            line.SetPosition(1, transform.InverseTransformPoint(ray.GetPoint(range)));
            if (colDied)
            {
                colDied = false;
            }
        }
    }

    void OnDisable()
    {
        CancelInvoke("Fire");
        line.enabled = false;
    }

    /*void UpdateAmmo()
    {
        if (Slot == 2)
        {
            HealthManager.weaponRUseEnergy = true;
        }
        else if (Slot == 1)
        {
            HealthManager.weaponLUseEnergy = true;
        }
    }*/
}
