using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class HelpPartReporter : MonoBehaviour {

	public void ReportPart () {
		GetComponentInParent<HelpPartManager> ().ChangePart (GetComponentInChildren<Text> ().text);
	}
}
