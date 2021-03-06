using UnityEngine;
using System.Collections;

[CreateAssetMenu(fileName = "Weapon Data", menuName = "Data Templates/Weapon Template", order = 4)]
public class WeaponData : ScriptableObject {

	public string partName;
    public string prefabName;
    public GameObject partVisuals;
	public float integrityMultiplier;

	public float damage;
	public float RoF;
	public float range;
	public float force;

    public enum AmmoTypes {
        Standard, Energy
    }
    public AmmoTypes ammoType;
    public float ammoMax; //if use energy then this is cost per shot

	public float special;
    public string specialDesc;
	public float projectileSpeed;
	public float projectileLife;
}
