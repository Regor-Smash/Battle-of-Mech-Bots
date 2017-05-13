using UnityEngine;
using System.Collections;

public class LineNetworker : MonoBehaviour {

	LineRenderer line;
	bool on;

	void Awake () {
		line = GetComponentInChildren <LineRenderer> ();
	}

	void Update () {
		if (gameObject.GetComponent<PhotonView> ().isMine) {
			//on = line.enabled;
		} else {
			//line.enabled = on;
		}
	}

	void OnPhotonSerializeView (PhotonStream stream, PhotonMessageInfo info){
		if (stream.isWriting) {
			stream.SendNext (line.enabled);
			//stream.SendNext (line.);
			//Debug.Log ("write line = "+on);
		} else {
			line.enabled = (bool) stream.ReceiveNext ();
			//line.SetPosition (1, (Vector3) stream.ReceiveNext ());
			//Debug.Log ("read line = "+line.enabled);
		}
	}
}
