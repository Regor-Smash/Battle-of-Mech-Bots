using UnityEngine;

[AddComponentMenu("Integrity/Default Integrity", 0)]
public class PhotonIntegrity : MonoBehaviour, IntegrityInterface
{
    public float startIntegrity;
    public float integrity { get; protected set; }
    public bool scoreable;
    public bool destroyRoot;

    void Awake ()
    {
        integrity = startIntegrity;
    }

    public void Die(PhotonPlayer killer)
    {
        if (scoreable && killer != null && killer.isLocal)
        {
            ScoreKeeper.Score += ScoreKeeper.destroyPoints;
        }

        if (!destroyRoot)
        {
            if (GetComponent<PhotonView>().isMine)
            {
                PhotonNetwork.Destroy(gameObject);
            }
            else if (!GetComponent<PhotonView>())
            {
                Destroy(gameObject);
            }
        }
        else if (destroyRoot)
        {
            if (transform.root.GetComponent<PhotonView>().isMine)
            {
                PhotonNetwork.Destroy(transform.root.gameObject);
            }
            else if (!transform.root.GetComponent<PhotonView>())
            {
                Destroy(transform.root.gameObject);
            }
        }
    }

    [PunRPC]
    public void TakeDamage(float dam, PhotonPlayer player)
    {
        integrity -= dam;
        if (integrity <= 0)
        {
            Die(player);
        }
    }
}
