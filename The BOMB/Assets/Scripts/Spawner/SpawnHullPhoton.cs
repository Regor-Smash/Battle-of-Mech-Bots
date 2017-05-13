using UnityEngine;

public class SpawnHullPhoton : MonoBehaviour
{
	public static GameObject Hull;
	GameObject hullOverlay;
	
	void SpawnHull ()
    {
		//Debug.Log ("Hull is spawning");
		Hull = PhotonNetwork.Instantiate (SaveBot.CurrentPresetData.hull.prefabName, transform.position, transform.rotation,0);
		
        Hull.AddComponent<WaterOverlay> ();
		Hull.GetComponentInChildren<Camera> ().enabled = true;
		Hull.GetComponentInChildren<AudioListener> ().enabled = true;
        Hull.layer = LayerMask.NameToLayer("MyBot");

        hullOverlay = (GameObject)Instantiate(Resources.Load("Bot HUD"));
        Hull.GetComponent<PhotonHullManager>().HUD = hullOverlay;
    }
}
