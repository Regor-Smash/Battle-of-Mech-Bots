using UnityEngine;
using System.Collections;

public class ElectroViewGlitch : MonoBehaviour {

	public int magnitude;
	public int maxTime;
	public float duration;
	//Camera cam;

	public float randTimer;
	public float yMod;
	public float zMod;

	Quaternion restoreRot;

	void Awake () {
		if (gameObject.GetComponentInParent <PhotonView> ().isMine) {
			enabled = true;
		}
	}

	void Start () {
		//cam = gameObject.GetComponent<Camera> ();
		InvokeRepeating ("Timer", maxTime, 1);
	}

	void Timer () {
		if (randTimer <= 0){
			Glitch();
			randTimer = duration + (int) Random.Range (1, maxTime);
		} else if (randTimer > 0) {
			randTimer = randTimer - 1;
		}
	}

	void Glitch () {
		yMod = Random.Range (-1.0f,1.0f);
		zMod = Random.Range (-1.0f,1.0f);
		restoreRot = gameObject.transform.localRotation;
		gameObject.transform.localRotation =
			new Quaternion (restoreRot.x, magnitude * yMod/360, magnitude * zMod/360, restoreRot.w);
		Invoke ("Restore", duration);
	}

	void Restore () {
		gameObject.transform.localRotation = restoreRot;
	}
}
