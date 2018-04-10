using UnityEngine;

public class CreatorMovement : MonoBehaviour
{
    GameObject Move;

    void Awake()
    {
        CreatorManager.MoveUpdate += UpdateMovement;
    }

    void UpdateMovement()
    {
        if (Move != null)
        {
            Destroy(Move);
        }
        Move = (GameObject)Instantiate(SaveBot.CurrentPresetData.movement.partVisuals, gameObject.transform.position, gameObject.transform.rotation);
    }

    private void OnDestroy()
    {
        CreatorManager.MoveUpdate -= UpdateMovement;
    }
}