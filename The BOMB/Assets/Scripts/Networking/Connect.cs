using UnityEngine;

public class Connect : MonoBehaviour
{
    public static string Version
    {
        get
        {
            return "A3.0";
        }
    }

    void Start()
    {
        if (PhotonNetwork.offlineMode || Options.isOffline)
        {
            PhotonNetwork.offlineMode = true;
            OnConnectedToMaster();
        }
        else
        {
            PhotonNetwork.ConnectUsingSettings(Version);
        }
    }

    private void OnPhotonFailedToConnectToMaster() //NOT WORKING YET
    {
        Debug.LogError("Could not connect to server. Going offline.");

        PhotonNetwork.offlineMode = true;
        OnConnectedToMaster();
    }

    void OnConnectedToMaster()
    {
        PhotonNetwork.JoinRandomRoom();
    }

    void OnPhotonRandomJoinFailed()
    {
        PhotonNetwork.CreateRoom("Room");
    }
}
