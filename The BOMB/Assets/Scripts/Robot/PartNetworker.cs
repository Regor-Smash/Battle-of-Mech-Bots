using UnityEngine;

public class PartNetworker : MonoBehaviour
{
    void Start()
    {
        if (GetComponent<PhotonView>().isMine)
        {
            GetComponent<PhotonView>().RPC("ShareParent", PhotonTargets.OthersBuffered, transform.parent.gameObject);
        }
        else if (!GetComponent<PhotonView>().isMine)
        {

        }
    }

    [PunRPC]
    public void ToggleActive(bool active)
    {
        gameObject.SetActive(active);
    }

    [PunRPC]
    public void ShareParent(GameObject parentGO)
    {
        transform.SetParent(parentGO.transform);
    }
}
