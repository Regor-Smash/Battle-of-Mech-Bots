using UnityEngine;
using System.Collections;

public class TakeScreenshot : MonoBehaviour {

	static int PicNum = 0;

	void Update () {
		if (Input.GetButtonDown ("Screenshot")) {
			ScreenCapture.CaptureScreenshot ("BOMB-screenshot" + PicNum + ".png");
			PicNum = PicNum + 1;
			Debug.Log ("Screenshot saved");
		}
	}
}
