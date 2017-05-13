using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ConnectStatus : MonoBehaviour {

	Text status;

	void Start () {
		status = gameObject.GetComponent<Text>();
	}

	void Update () {
		status.text = PhotonNetwork.connectionStateDetailed.ToString()+"...";
	}
}
