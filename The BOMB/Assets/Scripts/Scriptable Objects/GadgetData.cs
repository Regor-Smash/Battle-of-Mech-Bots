using UnityEngine;
using System.Collections;

[CreateAssetMenu(fileName = "Gadget Data", menuName = "Data Templates/Gadget Template", order = 6)]
public class GadgetData : ScriptableObject {

	public string partName;
//	public Mesh partMesh;	//May not need

	public enum GadgetTypes {
		Buff, Trap
	}
	public GadgetTypes gadgetType;
}
