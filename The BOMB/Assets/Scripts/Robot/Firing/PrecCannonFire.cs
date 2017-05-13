using UnityEngine;

public class PrecCannonFire : WeaponTemplate
{
    LineRenderer line;
    float timeStamp;
    Collider Col;

    AudioSource sound;

    protected override void Awake()
    {
        data = Resources.Load<WeaponData>("Part Database/Weapons/Precision Cannon");
        base.Awake();

        line = GetComponent<LineRenderer>();
        line.enabled = false;

        sound = GetComponent<AudioSource>();
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
        sound.Play();

        Ray ray = new Ray(transform.position, transform.forward);
        RaycastHit hit;

        line.enabled = true;
        line.SetPosition(0, Vector3.zero);
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

                /*if (Col.GetComponent<PhotonIntegrity>() && )
                {
                    if (Col.GetComponent<PhotonIntegrity>().GetIntegrity() - damage <= 0 && Col.GetComponent<PhotonIntegrity>().GetIntegrity() > 0)
                    {
                        ScoreKeeper.Score = ScoreKeeper.Score + ScoreKeeper.destroyPoints;
                    }
                    if (Col.GetComponent<PhotonIntegrity>().GetIntegrity() > 0)
                    {
                        Col.GetComponent<PhotonView>().RPC("TakeDamage", PhotonTargets.AllBufferedViaServer, damage);
                    }
                }
                else if (Col.GetComponent<PhotonHullManager>() && !Col.tag.Contains("My"))
                {
                    if (Col.GetComponent<PhotonHullManager>().GetIntegrity() > 0)
                    {
                        Col.GetComponent<PhotonView>().RPC("TakeHullDamage", PhotonTargets.AllBufferedViaServer, damage, GetComponentInParent<PhotonView>());
                    }
                }*/
            }
        }
        else
        {
            line.SetPosition(1, transform.InverseTransformPoint(ray.GetPoint(range)));
        }

        timeStamp = Time.time + fireRate;
        Invoke("LineOff", 0.05f);
    }

    void LineOff()
    {
        line.enabled = false;
    }

    private void OnDisable()
    {
        LineOff();
    }
}
