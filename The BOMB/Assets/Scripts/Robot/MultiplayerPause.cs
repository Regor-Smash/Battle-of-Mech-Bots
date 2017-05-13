using UnityEngine;
using System.Collections;

public class MultiplayerPause : MonoBehaviour {

	public GameObject PMenu;
	GameObject tempPMenu;
	public static bool isPaused = false;

	void Awake () {
		if (gameObject.GetComponent <PhotonView> ().isMine) {
			enabled = true;
		}
	}

	void Start () {
		tempPMenu = Instantiate (PMenu);
		Cursor.lockState = CursorLockMode.Locked;
		Cursor.visible = false;
		tempPMenu.SetActive (false);
		//Time.timeScale = 1;
	}

	void Update () {
		if (Input.GetButtonDown ("Pause") && !isPaused) {
			Pause();
		} else if (Input.GetButtonDown ("Pause") && isPaused) {
			Resume();
		}
	}

	void OnDestroy () {
		Cursor.lockState = CursorLockMode.None;
		Cursor.visible = true;
		Destroy (tempPMenu);
	}
	public void Pause () {
		tempPMenu.SetActive (true);
		Cursor.lockState = CursorLockMode.None;
		Cursor.visible = true;
		isPaused =true;
	}

	public void Resume () {
		tempPMenu.SetActive (false);
		Cursor.lockState = CursorLockMode.Locked;
		Cursor.visible = false;
		isPaused = false;
	}
}
