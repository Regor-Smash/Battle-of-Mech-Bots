using UnityEngine;

public class DrillRotate : WeaponTemplate
{
    //bool isActive = false;
    GameObject thing;
    bool thingDied;
    
    Ray ray;
    RaycastHit hit;
    //Collider Col;
    
    float spinSpeed;

    //Range callibration
    GameObject rangeIndicator;
    public bool testRange;

    protected override void Awake()
    {
        data = Resources.Load<WeaponData>("Part Database/Weapons/Drill");
        base.Awake();

        spinSpeed = data.special;
        
        thingDied = false;
    }

    void Update()
    {
        if (Input.GetButtonDown("Fire" + Slot) && !MultiplayerPause.isPaused && hullMang.energy >= cost)
        {
            //isActive = true;
            InvokeRepeating("CheckAmmo", 0, fireRate);
        }
        if (Input.GetButtonUp("Fire" + Slot) || MultiplayerPause.isPaused || hullMang.energy < cost)
        {
            //isActive = false;
            CancelInvoke("CheckAmmo");
        }
        #region RangeTesting
        if (testRange)
        {
            if (rangeIndicator == null)
            {
                rangeIndicator = (GameObject)Instantiate(Resources.Load("Simple Plane"), Vector3.zero, new Quaternion(0.7f, 0, 0, 0.7f));
            }
            else
            {
                ray = new Ray(transform.position, transform.forward);
                rangeIndicator.transform.position = ray.GetPoint(range);
            }
        }
        #endregion
    }

    void FixedUpdate()
    {
        if (Input.GetButton("Fire" + Slot) && !MultiplayerPause.isPaused && hullMang.energy >= cost)
        {
            transform.Rotate(0, 0, spinSpeed);
        }
    }

    //	void OnCollisionEnter (Collision col) {
    //		Debug.Log ("Hit");
    //		if (isActive && col.gameObject.GetComponent <PhotonIntegrity> ()) {
    //			thing = col.gameObject;
    //			Debug.Log ("Hit Integrity");
    //			InvokeRepeating ("Damage",0,RoF);
    //		} else if (isActive && col.gameObject.GetComponent <PhotonHullIntegrity> ()) {
    //			thing = col.gameObject;
    //			InvokeRepeating ("Damage",0,RoF);
    //		}
    //	}
    //
    //	void OnCollisionStay (Collision col) {
    //		Debug.Log ("Hit Stay");
    //		if (isActive && col.gameObject.GetComponent <PhotonIntegrity> ()) {
    //			thing = col.gameObject;
    //			Debug.Log ("Hit Integrity Stay");
    //			InvokeRepeating ("Damage",0,RoF);
    //		} else if (isActive && col.gameObject.GetComponent <PhotonHullIntegrity> ()) {
    //			thing = col.gameObject;
    //			InvokeRepeating ("Damage",0,RoF);
    //		}
    //	}
    //
    //	void OnCollisionExit (Collision col) {
    //		Debug.Log ("Exit Hit");
    //		CancelInvoke ("Damage");
    //		thing = null;
    //	}

    protected override void Fire()
    {
        ray = new Ray(transform.position, transform.forward);

        if (Physics.Raycast(ray, out hit, range))
        {
            //Debug.Log ("Drill hit!");
            thing = hit.collider.gameObject;

            if (/*thing.GetComponent<PhotonIntegrity>() && */!thing.tag.Contains("My") && !thingDied)
            {
                if (thing.GetComponent<IntegrityInterface>() != null)
                {
                    if (thing.GetComponent<IntegrityInterface>().integrity > 0)
                    {
                        thing.GetComponent<PhotonView>().RPC("TakeDamage", PhotonTargets.AllBufferedViaServer, damage, myView);
                    }
                    else
                    {
                        Debug.Log(thing.name + " should be dead");
                    }
                }

                /*if (thing.GetComponent<PhotonIntegrity>().integrity - damage <= 0 && thing.GetComponent<PhotonIntegrity>().integrity > 0
                    && thing.GetComponent<PhotonIntegrity>().scoreable)
                {
                    ScoreKeeper.Score = ScoreKeeper.Score + ScoreKeeper.destroyPoints;
                    thingDied = true;
                }
                if (thing.GetComponent<PhotonIntegrity>().integrity > 0)
                {
                    thing.GetComponent<PhotonView>().RPC("TakeDamage", PhotonTargets.AllBufferedViaServer, damage);
                }
                else if (thing.GetComponent<PhotonHullManager>() && !thing.tag.Contains("My") && thingDied)
                {
                    if (thing.GetComponent<PhotonHullManager>().GetIntegrity() - damage <= 0)
                    {
                        thingDied = true;
                    }
                    if (thing.GetComponent<PhotonHullManager>().GetIntegrity() > 0)
                    {
                        thing.GetComponent<PhotonView>().RPC("TakeHullDamage", PhotonTargets.AllBufferedViaServer, damage, GetComponentInParent<PhotonView>());
                    }
                }*/
            }
        }
        else
        {
            thingDied = false;
        }
    }

    void OnDisable ()
    {
        CancelInvoke("CheckAmmo");
    }
}
