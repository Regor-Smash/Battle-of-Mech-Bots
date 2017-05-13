using UnityEngine;
using System.Collections;

public class WheelRotate : MonoBehaviour {

//	Rigidbody hull;
	public float spinSpeed;

//	void Start () {
//		hull = GetComponentInParent <Rigidbody> ();
//	}

	void FixedUpdate () {
		spinSpeed = spinSpeed * Input.GetAxis ("Forward");

//		Debug.Log (transform.InverseTransformDirection (hull.velocity).z);
		transform.Rotate (/*transform.InverseTransformDirection (hull.velocity).z*/ spinSpeed, 0, 0);
	}
}
