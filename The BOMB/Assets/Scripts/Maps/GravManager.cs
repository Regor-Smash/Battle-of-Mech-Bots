using UnityEngine;
using System.Collections;

public class GravManager : MonoBehaviour {

	public int[] sideGravScenes;
	bool sideGrav = false;
	Collider gravTrigger;
	public bool isRotate;
	Quaternion gravRot;

	public float grav;
	//int sceneNum;
	Rigidbody hullrb;

	void Awake () {
		if (GetComponent <PhotonView> ().isMine) {
			enabled = true;
		}
	}

	void Start () {
		//sceneNum = Application.loadedLevel;
		hullrb = GetComponent<Rigidbody> ();
		/*foreach (int a in sideGravScenes) {
			if (a == sceneNum) {
				sideGrav = true;
			}
		}*/
		if (!sideGrav) {
			hullrb.useGravity = true;
			enabled = false;
		} else if (sideGrav) {
			hullrb.useGravity = false;
			Debug.Log ("alt gravity detected");
		}
	}

	void FixedUpdate () {
		//Debug.Log ("no gravity detected, applying manually at "+grav);
		hullrb.AddRelativeForce (0, -grav, 0, ForceMode.Acceleration);

		if (isRotate) {
			Debug.Log ("rotating manual gravity");
			if (gravTrigger.name == "Z+ Trig" || gravTrigger.name == "Z- Trig") {
				// change to Z
				gravRot = new Quaternion (gravTrigger.transform.rotation.x, transform.rotation.y, gravTrigger.transform.rotation.z, transform.rotation.w);
			} else if (gravTrigger.name == "X+ Trig" || gravTrigger.name == "X- Trig") {
				// change to X
				gravRot = new Quaternion (gravTrigger.transform.rotation.x, transform.rotation.y, gravTrigger.transform.rotation.z, transform.rotation.w);
			} else if (gravTrigger.name == "Y+ Trig" || gravTrigger.name == "Y- Trig") {
				// change to Y
				gravRot = new Quaternion (gravTrigger.transform.rotation.x, transform.rotation.y, gravTrigger.transform.rotation.z, transform.rotation.w);
			} 
			transform.rotation = Quaternion.Lerp (transform.rotation, gravRot, 0.8f);
			if (transform.rotation == gravRot) {
				isRotate = false;
			}
		}
	}

	void OnTriggerEnter (Collider col) {
		if (col.tag == "GravTrigger") {
			gravTrigger = col;
		}
	}

	void OnTriggerExit (Collider col) {
		if (sideGrav && col.tag == "GravTrigger") {
			//Debug.Log ("exit trigger" + col.name);
			//GravRot ();
			isRotate = true;
		}
	}

	void GravRot () {
		//Debug.Log ("GravRot " + gravTrigger.name);

	}
}
