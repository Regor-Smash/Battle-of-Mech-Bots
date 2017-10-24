using UnityEngine;

public class RailBullet : MonoBehaviour
{
    WeaponData data;
    PhotonView myView;

    float damage;
    float force;
    float railSpeed;
    float lifeTime;

    Ray ray;
    RaycastHit hit;
    public float detectRange;
    Collider Col;

    Rigidbody rd;

    void Awake()
    {
        myView = GetComponent<PhotonView>();
        if (myView.isMine)
        {
            enabled = true;

            data = Resources.Load<WeaponData>("Part Database/Weapons/Rail Gun");
            damage = data.damage;
            force = data.force;
            railSpeed = data.projectileSpeed;
            lifeTime = data.projectileLife;
        }
        else
        {
            Destroy(this);
        }
    }

    void Start()
    {
        rd = GetComponent<Rigidbody>();
        rd.AddForce(transform.forward * railSpeed, ForceMode.VelocityChange);

        if (GetComponent<PhotonView>().isMine)
        {
            Invoke("DestroyRail", lifeTime);
        }
    }

    void Update()
    {
        ray = new Ray(transform.position, transform.forward);
        if (Physics.Raycast(ray, out hit, detectRange))
        {
            //Debug.Log ("Rail bullet hit!");
            Col = hit.collider;
            OnHit();
        }
    }

    void OnHit()
    {
        if (hit.rigidbody)
        {
            hit.rigidbody.AddForceAtPosition(transform.forward * force, hit.point, ForceMode.Impulse);
        }

        if (!Col.tag.Contains("My"))
        {
            DestroyRail();

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
                }
                if (Col.GetComponent<PhotonIntegrity>().GetIntegrity() > 0)
                {
                    Col.GetComponent<PhotonView>().RPC("TakeDamage", PhotonTargets.AllBufferedViaServer, damage);
                }
            }
            else if (Col.GetComponent<PhotonHullManager>())
            {
                if (Col.GetComponent<PhotonHullManager>().GetIntegrity() > 0)
                {
                    Col.GetComponent<PhotonView>().RPC("TakeHullDamage", PhotonTargets.AllBufferedViaServer, damage, GetComponent<PhotonView>());
                }
            }*/
        }
    }

    void DestroyRail()
    {
        if (GetComponent<PhotonView>().isMine)
        {
            PhotonNetwork.Destroy(gameObject);
        }
    }
}
