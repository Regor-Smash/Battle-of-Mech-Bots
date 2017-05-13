using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class VersionDisplay : MonoBehaviour {

	void Start () {
		GetComponent<Text> ().text = Connect.Version();
	}
}
