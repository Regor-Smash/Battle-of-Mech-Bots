using UnityEngine;
using System.Collections;

[CreateAssetMenu(fileName = "Movement Data", menuName = "Data Templates/Movement Template", order = 5)]
public class MovementData : ScriptableObject {

	public string partName;
    public string prefabName;
    public GameObject partVisuals;
	public float integrityMultiplier;

	public int power;
	public int jumpPower;
    public float backwardsMod;

    public int capacity;
    public Vector3 maxSpeed;
    public Vector3 minSpeed;

    public float moveSize;	//Used to detect if grounded
}
