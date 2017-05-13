using UnityEngine;
using System.Collections;

public class NetworkUpdate : Photon.MonoBehaviour {

	Vector3 realPos;
	Quaternion realRot;
	bool hasRecieved = false;

	void Start () {
		realPos = gameObject.transform.position;
		realRot = gameObject.transform.rotation;
	}

	void Update () {
		if (!photonView.isMine && hasRecieved) {
			transform.position = Vector3.Lerp (transform.position, realPos, 0.2f);
			transform.rotation = Quaternion.Lerp (transform.rotation, realRot, 0.2f);
		}
	}

	void OnPhotonSerializeView (PhotonStream stream, PhotonMessageInfo info){
		if (stream.isWriting) {
			stream.SendNext (transform.position);
			stream.SendNext (transform.rotation);
		} else {
			realPos = (Vector3) stream.ReceiveNext ();
			realRot = (Quaternion) stream.ReceiveNext ();
			if (!hasRecieved) {
				hasRecieved = true;
			}
		}
	}
}
