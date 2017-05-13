using UnityEngine;
using System.Collections;

public class WaterOverlay : MonoBehaviour {

	GameObject tempWater;

	void OnTriggerEnter (Collider Col) {
		if (Col.tag == "Water") {
			if (tempWater != null) {
				Destroy (tempWater,0);
			}
			tempWater = (GameObject) Instantiate (Resources.Load ("Water Overlay"));
		}
	}

	void OnTriggerExit (Collider Col) {
		if (Col.tag == "Water") {
			Destroy (tempWater,0);
		}
	}

	void OnDestroy () {
		Destroy (tempWater);
	}
}
