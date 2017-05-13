using UnityEngine;

public class GatRayFire : WeaponTemplate
{
    float strayMod;
    Vector3 bulletStray;

    LineRenderer line;
    Ray ray;
    RaycastHit hit;

    Collider Col;
    //bool colDied = false;

    AudioSource sound;

    protected override void Awake()
    {
        data = Resources.Load<WeaponData>("Part Database/Weapons/Gatling Gun");
        base.Awake();

        strayMod = data.special;

        line = GetComponent<LineRenderer>();
        line.enabled = false;

        sound = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (Input.GetButtonDown("Fire" + Slot) && !MultiplayerPause.isPaused)
        {
            //Debug.Log("Trying to Fire");
            InvokeRepeating("CheckAmmo", 2, fireRate);
        }
        if (Input.GetButtonUp("Fire" + Slot) || MultiplayerPause.isPaused)
        {
            CancelInvoke("CheckAmmo");
            line.enabled = false;
        }
    }

    protected override void Fire()
    {
        PlayAudio();

        float x = Random.Range(-strayMod, strayMod);
        float y = Random.Range(-strayMod, strayMod);
        float z = Random.Range(-strayMod, strayMod);
        bulletStray = new Vector3(x, y, z);
        ray = new Ray(transform.position, transform.forward + bulletStray);

        line.enabled = true;
        line.SetPosition(0, transform.InverseTransformPoint(ray.GetPoint(0.5f)));
        if (Physics.Raycast(ray, out hit, range))
        {
            line.SetPosition(1, transform.InverseTransformPoint(hit.point));

            Col = hit.collider;

            if (hit.rigidbody)
            {
                hit.rigidbody.AddForceAtPosition(transform.forward * force, hit.point, ForceMode.Impulse);
            }
            if (!Col.tag.Contains("My"))
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

                /*if (Col.GetComponent<PhotonIntegrity>() || Col.GetComponent<PhotonWeaponIntegrity>() || Col.GetComponent<PhotonMoveIntegrity>())
                {
                    if (Col.GetComponent<PhotonIntegrity>().GetIntegrity() > 0 || Col.GetComponent<PhotonWeaponIntegrity>().GetIntegrity() > 0 ||
                        Col.GetComponent<PhotonMoveIntegrity>().GetIntegrity() > 0)
                    {
                        Col.GetComponent<PhotonView>().RPC("TakeDamage", PhotonTargets.AllBufferedViaServer, damage);

                    }
                    else
                    {
                        Debug.Log(Col.name + " should be dead");
                    }
                }
                if (Col.GetComponent<PhotonHullManager>())
                {
                    if (Col.GetComponent<PhotonHullManager>().GetIntegrity() > 0)
                    {
                        Col.GetComponent<PhotonView>().RPC
                            ("TakeHullDamage", PhotonTargets.AllBufferedViaServer, damage, GetComponentInParent<PhotonView>());
                    }
                    else
                    {
                        Debug.Log(Col.name + " (hull) should be dead.");
                    }
                }*/
            }

            /*if (Col.GetComponent<PhotonIntegrity>() && !Col.tag.Contains("My") && !colDied)
            {
                if (Col.GetComponent<PhotonIntegrity>().GetIntegrity() - damage <= 0 && Col.GetComponent<PhotonIntegrity>().GetIntegrity() > 0)
                {
                    ScoreKeeper.Score = ScoreKeeper.Score + ScoreKeeper.destroyPoints;
                    //colDied = true;
                }
                if (Col.GetComponent<PhotonIntegrity>().GetIntegrity() > 0 || Col.GetComponent<PhotonWeaponIntegrity>().GetIntegrity() > 0 ||
                    Col.GetComponent<PhotonMoveIntegrity>().GetIntegrity() > 0)
                {
                    Col.GetComponent<PhotonView>().RPC("TakeDamage", PhotonTargets.AllBufferedViaServer, damage);
                }
            }
            else if (Col.GetComponent<PhotonHullManager>() && !Col.tag.Contains("My") && !colDied)
            {
                if (Col.GetComponent<PhotonHullManager>().GetIntegrity() - damage <= 0)
                {
                    //colDied = true;
                }
                if (Col.GetComponent<PhotonHullManager>().GetIntegrity() > 0)
                {
                    Col.GetComponent<PhotonView>().RPC("TakeHullDamage", PhotonTargets.AllBufferedViaServer, damage, GetComponentInParent<PhotonView>());
                }
            }
            if (!Col.GetComponent<PhotonHullManager>() && !Col.GetComponent<PhotonIntegrity>() && colDied)
            {
                //colDied = false;
            }*/
        }
        else
        {
            line.SetPosition(1, transform.InverseTransformPoint(ray.GetPoint(range)));
            Col = null;
            /*if (colDied)
            {
                colDied = false;
            }*/
        }

        Invoke("LineOff", fireRate / 2);
    }
    
    void LineOff()
    {
        line.enabled = false;
    }

    void OnDisable ()
    {
        CancelInvoke("Fire");
        line.enabled = false;
    }

    void PlayAudio ()
    {
        GameObject newAudio = new GameObject() /*(GameObject)Instantiate(new GameObject(), transform.position, transform.rotation)*/;
        newAudio.transform.SetParent(transform, true);
        newAudio.name = "Gatling Gun Sound";

        newAudio.AddComponent<AudioSource>();
        newAudio.GetComponent<AudioSource>().clip = sound.clip;
        newAudio.GetComponent<AudioSource>().outputAudioMixerGroup = sound.outputAudioMixerGroup;
        newAudio.GetComponent<AudioSource>().Play();

        Destroy(newAudio, sound.clip.length);
    }
}
