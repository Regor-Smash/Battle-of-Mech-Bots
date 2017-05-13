using UnityEngine;

public class CameraColor : MonoBehaviour {

	public ColorData colorMang;

	string sceneName;

	void Awake () {
		if (gameObject.GetComponentInParent <PhotonView> ().isMine) {
			enabled = true;
		}
	}

	void Start () {
		sceneName = SceneManagerHelper.ActiveSceneName;

		switch (sceneName) {
		case "Crater":
			GetComponent<Camera> ().backgroundColor = colorMang.dayColor;
			GetComponent<DayChange> ().enabled = true;
			break;
		case "City":
			GetComponent<Camera> ().backgroundColor = colorMang.dayColor;
			GetComponent<DayChange> ().enabled = true;
			break;
		case "Shooting Range":
			GetComponent<Camera> ().backgroundColor = colorMang.dayColor;
			break;
		case "Field":
			GetComponent<Camera> ().clearFlags = CameraClearFlags.Skybox;
			break;
		case "Dubstep Cube":
			GetComponent<Camera> ().clearFlags = CameraClearFlags.Skybox;
			break;
		}
	}
}
