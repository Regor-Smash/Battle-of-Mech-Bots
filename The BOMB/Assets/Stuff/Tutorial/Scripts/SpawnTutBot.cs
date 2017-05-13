using UnityEngine;

public class SpawnTutBot : MonoBehaviour
{
    private GameObject tutBot;

    void Start()
    {
        PhotonNetwork.offlineMode = true;
        PhotonNetwork.JoinOrCreateRoom("Tutorial", new RoomOptions(), new TypedLobby());

        tutBot = PhotonNetwork.Instantiate("Tutorial Bot", transform.position, transform.rotation, 0);
        tutBot.GetComponent<PhotonHullManager>().HUD = (GameObject)Instantiate(Resources.Load("Bot HUD"));
    }
}
