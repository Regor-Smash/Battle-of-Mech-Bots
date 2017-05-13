using UnityEngine;
using System.Collections;

public class StartSpawn : MonoBehaviour {

	public SpawnHullPhoton initialSpawn;

	void OnJoinedRoom () {
		initialSpawn.Invoke ("SpawnHull", 0);
		Cursor.lockState = CursorLockMode.Locked;
		Destroy (gameObject, 0);
	}
}
