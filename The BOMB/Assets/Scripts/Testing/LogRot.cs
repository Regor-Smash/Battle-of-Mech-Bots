using UnityEngine;
using System.Collections;

public class LogRot : MonoBehaviour {

	public bool LogOnStart;

	void Start () {
		if (LogOnStart) {
			Debug.Log (gameObject.name + "'s rotation is" + gameObject.transform.rotation);		
		}
	}

	void Update () {
		if (!LogOnStart) {
			Debug.Log (gameObject.name + "'s rotation is" + gameObject.transform.rotation);		
		}
	}
}
