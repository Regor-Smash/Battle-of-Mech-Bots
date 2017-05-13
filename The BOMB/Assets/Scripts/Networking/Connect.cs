using UnityEngine;
using System.Collections;

public class Connect : MonoBehaviour {

	public static string Version() {
		return "A3.0";
	}

	void Start () {
		if (PhotonNetwork.offlineMode || Options.isOffline) {
			PhotonNetwork.offlineMode = true;
			OnConnectedToMaster ();
		} else {
			PhotonNetwork.ConnectUsingSettings (Version());
		}
	}

	void OnConnectedToMaster () {
		PhotonNetwork.JoinRandomRoom();
	}

	void OnPhotonRandomJoinFailed () {
		PhotonNetwork.CreateRoom ("Room");
	}
}
