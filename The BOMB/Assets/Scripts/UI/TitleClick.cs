using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class TitleClick : MonoBehaviour {

	void Update (){
		if (Input.anyKeyDown) {
			SceneManager.LoadScene ("Main Menu");
		}
	}
}
