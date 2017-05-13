using UnityEngine;
using System.Collections;

public class FindResume : MonoBehaviour {
	
	public void BotResume () {
		SpawnHullPhoton.Hull.GetComponent <MultiplayerPause> ().Resume ();
	}
}
