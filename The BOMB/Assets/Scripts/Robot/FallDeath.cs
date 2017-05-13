using UnityEngine;

public class FallDeath : MonoBehaviour
{

    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.GetComponent<IntegrityInterface>() != null)
        {
            col.gameObject.GetComponent<IntegrityInterface>().Die(null);
        }
        /*if (col.gameObject.GetComponent <PhotonHullManager> () ) {
			col.gameObject.GetComponent <PhotonHullManager> ().TakeHullDamage (col.gameObject.GetComponent <PhotonHullManager> ().GetIntegrity(), null);
		} else if (col.gameObject.GetComponent <PhotonIntegrity> () ) {
			col.gameObject.GetComponent <PhotonIntegrity> ().TakeDamage(col.gameObject.GetComponent <PhotonIntegrity> ().integrity);
		}*/
        else if (col.gameObject.GetComponent<PhotonView>())
        {
            PhotonNetwork.Destroy(col.gameObject);
        }
        else
        {
            Destroy(col.gameObject, 0);
        }
    }
}
