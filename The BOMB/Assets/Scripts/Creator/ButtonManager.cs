using UnityEngine;
using System.Collections;

public class ButtonManager : MonoBehaviour {

	public int value;
	public int maxValue;

	public void ValueUp () {
		if (value != maxValue) {
			value++;
		} else {
			value = 0;
		}
		//Debug.Log (value);
	}

	public void ValueDown () {
		if (value > 0) {
			value--;
		} else {
			value = maxValue;	
		}
		//Debug.Log (value);
	}
}
