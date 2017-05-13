using UnityEngine;
using System.Collections;

[CreateAssetMenu(fileName = "Hull Data", menuName = "Data Templates/Hull Template", order = 3)]
public class HullData : ScriptableObject {

	public string partName;
    public string prefabName;
	public GameObject partVisuals;

	public int maxIntegrity;
	public int maxEnergy;
	public int mass;
    public float size;

	public Vector3 movePosOffset;
	public Vector3 moveRotOffset;

	public int maxPairs;
	public Vector3 weapon1PosOffset;
	public Vector3 weapon1RotOffset;
	public Vector3 weapon2PosOffset;
	public Vector3 weapon2RotOffset;
}
