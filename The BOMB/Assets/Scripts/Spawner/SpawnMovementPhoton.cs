using UnityEngine;

public class SpawnMovementPhoton : MonoBehaviour
{

    public Vector3 posOffset;
    public Quaternion rotOffset;

    GameObject Move;

    void Awake()
    {
        if (gameObject.GetComponent<PhotonView>().isMine)
        {
            enabled = true;
        }
    }

    void Start()
    {
        SpawnMovement();
    }

    void SpawnMovement()
    {
        Move = PhotonNetwork.Instantiate(SaveBot.CurrentPresetData.movement.prefabName, Vector3.zero, Quaternion.identity, 0) as GameObject;
        Move.transform.parent = gameObject.transform;
        Move.transform.localScale = new Vector3(1, 1, 1);
        Move.transform.localPosition = posOffset;
        Move.transform.localRotation = rotOffset;
        Move.layer = LayerMask.NameToLayer("MyBot");
    }
}
