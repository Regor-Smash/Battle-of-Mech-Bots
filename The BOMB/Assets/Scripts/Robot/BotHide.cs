using UnityEngine;
using System.Collections;

public class BotHide : MonoBehaviour {

	public bool ignoreRay;
	public bool hide;

	void Awake () {
		if (gameObject.GetComponentInParent <PhotonView> ().isMine || gameObject.GetComponent <PhotonView> ().isMine) {
			if (ignoreRay) {
				gameObject.layer = 2;
			}
			if (hide) {
				gameObject.GetComponent <MeshRenderer> ().enabled = false;
			}
		}
	}
}
