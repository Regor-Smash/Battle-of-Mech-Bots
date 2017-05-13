using UnityEngine;
using System.Collections;

public class Selfdestruct : MonoBehaviour {

	public float life;

	void Start () {
		Invoke ("DestroyOb", life);
	}

	void DestroyOb () {
		if (GetComponent <PhotonView>().isMine){
			PhotonNetwork.Destroy (gameObject);
		} else if (!GetComponent <PhotonView>()) {
			Destroy (gameObject, 0);
		}
	}
}
