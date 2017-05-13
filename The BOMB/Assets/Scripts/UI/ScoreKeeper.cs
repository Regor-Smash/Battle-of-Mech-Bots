using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ScoreKeeper : MonoBehaviour {

	public static float Score;
	public static float killPoints = 1;
	public static float destroyPoints = 0.1f;
	Text ScoreText;

	void Awake () {
		ScoreText = gameObject.GetComponent <Text>();
		Score = 0.0f;
	}

	void Update () {
		ScoreText.text = "" + Score;
	}
}
