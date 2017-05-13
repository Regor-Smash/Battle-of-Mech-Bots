using UnityEngine;
using System.Collections;

public class TargetSpawnerPhoton : MonoBehaviour {

	Vector3 spawnPoint;
	public int maxX;
	public int minX;
	public int maxZ;
	public int minZ;
	public float hieght;
	int timer;

	void Start () {
		timer = 400;
	}
	
	void FixedUpdate () {
		timer--;
		if (timer <= 0 && PhotonNetwork.isMasterClient){
			float x = Random.Range(minX, maxX);
			float z = Random.Range(minZ, maxZ);
			spawnPoint = new Vector3 (x, hieght, z);
			PhotonNetwork.Instantiate ("PhotonTarget", spawnPoint, Quaternion.identity , 0);
			timer = 200;
		}
	}
}
