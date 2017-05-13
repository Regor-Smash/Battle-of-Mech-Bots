using UnityEngine;

public class RAMSpike : MonoBehaviour
{
    WeaponData data;
    PhotonView myView;

    float damage;
    bool isFast = false;
    Rigidbody hull;
    Collider Col;
    bool newImpact;

    float ramRange;
    Ray ray;
    RaycastHit hit;

    GameObject rangeIndicator;
    public bool testRange;

    void Awake()
    {
        myView = GetComponentInParent<PhotonView>();
        if (myView.isMine)
        {
            enabled = true;

            data = Resources.Load<WeaponData>("Part Database/Movements/RAM Spike");
            damage = data.damage;
            ramRange = data.range;
        }
        else
        {
            Destroy(this);
        }
    }

    void Start()
    {
        hull = transform.GetComponentInParent<Rigidbody>();
    }

    void Update()
    {
        if (transform.InverseTransformDirection(hull.velocity).z >= 75)
        {
            isFast = true;
        }
        else if (transform.InverseTransformDirection(hull.velocity).z < 75)
        {
            isFast = false;
        }

        ray = new Ray(transform.position, transform.forward);
        if (Physics.Raycast(ray, out hit, ramRange) && isFast)
        {
            if (newImpact)
            {
                //Debug.Log ("Ram spike hit!");
                Col = hit.collider;
                Impact();
                newImpact = false;
            }
        }
        else if (!newImpact)
        {
            newImpact = true;
        }

        if (testRange)
        {
            if (rangeIndicator == null)
            {
                rangeIndicator = (GameObject)Instantiate(Resources.Load("Simple Plane"), Vector3.zero, new Quaternion(0.7f, 0, 0, 0.7f));
            }
            else
            {
                ray = new Ray(transform.position, transform.forward);
                rangeIndicator.transform.position = ray.GetPoint(ramRange);
            }
        }
    }

    void Impact()
    {
        if (isFast /*&& Col.GetComponent<PhotonIntegrity>()*/ && !Col.tag.Contains("My"))
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

            /*if (Col.GetComponent<PhotonIntegrity>().GetIntegrity() - damage <= 0 && Col.GetComponent<PhotonIntegrity>().GetIntegrity() > 0
                && Col.GetComponent<PhotonIntegrity>().scoreable)
            {
                ScoreKeeper.Score = ScoreKeeper.Score + ScoreKeeper.destroyPoints;
            }
            if (Col.GetComponent<PhotonIntegrity>().GetIntegrity() > 0)
            {
                Col.GetComponent<PhotonView>().RPC("TakeDamage", PhotonTargets.AllBufferedViaServer, damage);
            }
        }
        else if (isFast && Col.GetComponent<PhotonHullManager>() && !Col.tag.Contains("My"))
        {
            if (Col.GetComponent<PhotonHullManager>().GetIntegrity() > 0)
            {
                Col.GetComponent<PhotonView>().RPC("TakeHullDamage", PhotonTargets.AllBufferedViaServer, damage, GetComponentInParent<PhotonView>());
            }*/
        }
    }
}
