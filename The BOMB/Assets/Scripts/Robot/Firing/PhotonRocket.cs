using UnityEngine;
using System.Collections;

public class PhotonRocket : MonoBehaviour
{
    WeaponData data;
    PhotonView myView;

    float rocketSpeed;
    float lifeTime;
    float radius;
    float force;
    float damage;

    public float upMod;
    Rigidbody rd;

    void Awake()
    {
        myView = GetComponent<PhotonView>();
        if (myView.isMine)
        {
            enabled = true;

            data = Resources.Load<WeaponData>("Part Database/Weapons/Rocket Apparatus");
            damage = data.damage;
            radius = data.special;
            force = data.force;
            rocketSpeed = data.projectileSpeed;
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
        rd.AddForce(transform.forward * rocketSpeed, ForceMode.VelocityChange);

        if (GetComponent<PhotonView>().isMine)
        {
            Invoke("Explode", lifeTime);
        }
    }

    void OnCollisionEnter(Collision Col)
    {
        if (GetComponent<PhotonView>().isMine)
        {
            Explode();
        }
    }

    void Explode()
    {
        PhotonNetwork.Instantiate("Explosion Fire", transform.position, transform.rotation, 0);
        Collider[] colliders = Physics.OverlapSphere(transform.position, radius);
        foreach (Collider Col in colliders)
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

            /*if (Col.GetComponent<PhotonIntegrity> ()) {
				if (Col.GetComponent<PhotonIntegrity> ().GetIntegrity () - damage <= 0 && Col.GetComponent<PhotonIntegrity> ().GetIntegrity () > 0) {
					ScoreKeeper.Score = ScoreKeeper.Score + ScoreKeeper.destroyPoints;
				}
				if (Col.GetComponent<PhotonIntegrity> ().GetIntegrity () > 0) {
					Col.GetComponent<PhotonView> ().RPC ("TakeDamage",PhotonTargets.AllBufferedViaServer, damage);
				}
			} else if (Col.GetComponent <PhotonHullManager> ()) {
				if (Col.GetComponent<PhotonHullManager> ().GetIntegrity () > 0) {
					Col.GetComponent<PhotonView> ().RPC ("TakeHullDamage",PhotonTargets.AllBufferedViaServer, damage, GetComponent <PhotonView> ());
				}
			}*/
            if (Col.GetComponent<Rigidbody>())
            {
                Col.GetComponent<Rigidbody>().AddExplosionForce(force, transform.position, radius, upMod, ForceMode.Impulse);
            }
        }
        if (GetComponent<PhotonView>().isMine)
        {
            PhotonNetwork.Destroy(gameObject);
        }
    }
}
