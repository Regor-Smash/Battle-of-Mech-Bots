using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UIRotate : MonoBehaviour {

	public float spin;
	
	void Update () {
		gameObject.transform.Rotate (0, spin, 0);
	}
}
