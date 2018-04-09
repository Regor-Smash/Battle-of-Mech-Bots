using UnityEngine;

[CreateAssetMenu(fileName = "Gadget Data", menuName = "Data Templates/Gadget Template", order = 6)]
public class GadgetData : ScriptableObject {

	public string partName;
    public float cost;
//	public Mesh partMesh;	//May not need

	public enum GadgetTypes
    {
		Buff, Trap
	}
	public GadgetTypes gadgetType;

    public int duration;
    public int magnitutde;
}
