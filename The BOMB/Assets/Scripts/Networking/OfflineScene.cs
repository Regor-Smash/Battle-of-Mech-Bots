using UnityEngine;
using System.Collections;

public class OfflineScene : MonoBehaviour {
	
	void Start () {
		PhotonNetwork.offlineMode = true;
	}

	void OnDestroy () {
		PhotonNetwork.offlineMode = false;
	}
}
