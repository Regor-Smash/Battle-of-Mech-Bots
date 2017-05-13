using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class HelpPartManager : MonoBehaviour {

	public float spacing;
	float height;

	public enum PartTypes {
		Hull, Weapon, Movement, Gadget
	}
	public PartTypes partType;

	string pathName;
	PartList listAll;
	HullData hData;
	WeaponData wData;
	MovementData mData;
	GadgetData gData;

	List<GameObject> partButtons = new List<GameObject> ();
	public GameObject buttonBlank;

	public Text partTitle;
	public Text partStats;
	public Transform displayParent;
	GameObject partDisplay;

	void Start () {
		ChangePartType ("Hull");
	}

	public void ChangePartType (string newType) {
//		Debug.Log ("Part type changed to " + newType);
		foreach (PartTypes type in PartTypes.GetValues(typeof(PartTypes))) {
			if (newType == type.ToString ()) {
				partType = type;
			}
		}

		if (partButtons.Count != 0) {
			foreach (GameObject go in partButtons) {
				Destroy (go);
			}
			partButtons.Clear ();
		}

		/*pathName = "Part Database/" + partType.ToString () + "s/0" + partType.ToString () + " List";
		listAll = Resources.Load<PartList> (pathName);

		foreach (ScriptableObject so in listAll.allParts) {
			partButtons.Add ((GameObject)Instantiate (buttonBlank));

			int a = partButtons.Count - 1;
			height = partButtons [a].GetComponent<RectTransform> ().rect.height;

			partButtons [a].GetComponentInChildren<Text> ().text = (string) so.GetType().GetField("partName").GetValue(so);
			partButtons [a].transform.SetParent (transform, false);
			partButtons [a].GetComponent <RectTransform> ().anchoredPosition = new Vector3 (0, -(0.5f + a) * (spacing + height), 0);//
			GetComponent <RectTransform> ().sizeDelta = new Vector2 (0, (1 + a) * (spacing + height));

			if (a == 0) {
				ChangePart (partButtons [a].GetComponentInChildren<Text> ().text);
			}
		}*/
	}

	public void ChangePart (string partName) {
//		Debug.Log ("Part changed to " + partName);
		pathName = "Part Database/" + partType.ToString () + "s/" + partName;

		partTitle.text = partName;
		if (partDisplay != null) {
			Destroy (partDisplay);
		}

		if (partType == PartTypes.Hull) {
			hData = Resources.Load<HullData> (pathName);
			if (hData == null) {
				Debug.LogError ("No data found for " + partName);
			}
				
			//get all variables and add to statPanel
			partDisplay = Instantiate(hData.partVisuals);
			partDisplay.transform.SetParent (displayParent, false);

			partStats.text = "Integrity = " + hData.maxIntegrity +
				"\nEnergy = " + hData.maxEnergy +
				"\nMass = " + hData.mass +
				"\nPairs = " + hData.maxPairs;
		} else if (partType == PartTypes.Weapon) {
			wData = Resources.Load<WeaponData> (pathName);
			if (wData == null) {
				Debug.LogError ("No data found for " + partName);
			}

			//get all variables and add to statPanel
			partDisplay = Instantiate(wData.partVisuals);
			partDisplay.transform.SetParent (displayParent, false);

			partStats.text = "Integrity = " + wData.integrityMultiplier * 100 +
				"%\nDamage = " + wData.damage +
				"\nRate of Fire = " + wData.RoF +
				"\nRange = " + wData.range;
			if (wData.force > 0) {
				partStats.text += "\nForce = " + wData.force;
			}
			if (wData.projectileSpeed > 0) {
				partStats.text += "\nProjectile Speed = " + wData.projectileSpeed;
			}
		} else if (partType == PartTypes.Movement) {
			mData = Resources.Load<MovementData> (pathName);
			if (mData == null) {
				Debug.LogError ("No data found for " + partName);
			}

			//get all variables and add to statPanel
			partDisplay = Instantiate(mData.partVisuals);
			partDisplay.transform.SetParent (displayParent, false);

			string capacityText;
			if (mData.capacity > 0) {
				capacityText = mData.capacity.ToString ();
			} else {
				capacityText = "Infinte";
			}
			partStats.text = "Integrity = " + mData.integrityMultiplier * 100 +
				"%\nCapacity = " + capacityText +
				"\nPower = " + mData.power +
				"\nJump = " + mData.jumpPower +
				"\nMax Speed = " + mData.maxSpeed.z;
		} else if (partType == PartTypes.Gadget) {
			gData = Resources.Load<GadgetData> (pathName);
			if (gData == null) {
				Debug.LogError ("No data found for " + partName);
			}

			//get all variables and add to statPanel
			partStats.text = "TEST";
		}


	}
}
