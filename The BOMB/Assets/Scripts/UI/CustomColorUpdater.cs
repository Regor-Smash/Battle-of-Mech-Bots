using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CustomColorUpdater : MonoBehaviour {

	public Material customMat;

	void Start () {
		UpdateColor (customMat.color);
	}

	public void UpdateColor (Color newColor) {

		customMat.color = newColor;
		GetComponent<Image> ().color = newColor;
	}

	public void OpenColorSelect () {
//		Debug.Log ("Opening the color selector.");
	}
}
