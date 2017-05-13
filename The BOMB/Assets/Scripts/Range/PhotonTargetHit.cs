using UnityEngine;
using System.Collections;

public class PhotonTargetHit : MonoBehaviour {
	
	public float lifeTime;

	void Start () {
		//Invoke ("Die", lifeTime);
	}

	void OnDestroy () {
		if (PhotonNetwork.isMasterClient) {
			PhotonNetwork.Instantiate ("Poof", gameObject.transform.position, gameObject.transform.rotation,0);
			PhotonNetwork.Destroy (gameObject);
		}
	}

	void Die () {
		if (PhotonNetwork.isMasterClient) {
			PhotonNetwork.Destroy (gameObject);
		}
	}
}
