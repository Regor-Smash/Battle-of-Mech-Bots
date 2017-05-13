using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerOverheadInfo : MonoBehaviour {

	static GameObject clientPlayer;
	bool mine;

	void Start () {
		mine = gameObject.GetComponent<PhotonView> ().isMine;
		if (mine) {
			gameObject.GetComponent<PhotonView> ().RPC ("SendName", PhotonTargets.OthersBuffered, PhotonNetwork.playerName);
			clientPlayer = gameObject;
			gameObject.SetActive (false);
		} else if (!mine) {

		}
	}
	
	[PunRPC]
	void SendName (string username) {
		GetComponentInChildren<Text> ().text = username;
	}

	void Update () {
		if (!mine) {
			//clientPlayer = 
			gameObject.transform.LookAt (clientPlayer.GetComponentInParent<Transform>());
		}
	}
}
