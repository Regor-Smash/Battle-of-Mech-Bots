using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class ChangeCameraClick : MonoBehaviour {

	public Camera[] Cams;
	int CamNum;

	void Start () {
		CamNum = 0;
	}

	void Update () {
		if (Input.anyKeyDown) {
			Cams[CamNum].enabled = false;
			CamNum = CamNum + 1;
			if (CamNum >= Cams.Length) {
				SceneManager.LoadScene ("Main Menu");
			} else {
				Cams[CamNum].enabled = true;
			}
		}
	}
}
