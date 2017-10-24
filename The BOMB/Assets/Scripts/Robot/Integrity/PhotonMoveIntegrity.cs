using UnityEngine;
using System.Collections;

[AddComponentMenu("Integrity/Movement Integrity", 2)]
public class PhotonMoveIntegrity : MonoBehaviour, IntegrityInterface
{
    MovementData data;

    public float integrity { get; protected set; }

    bool isBroken = true;
    public delegate void MoveEvent();
    public static event MoveEvent BreakMove;
    public static event MoveEvent ResetMove;

    void Start()
    {
        data = SaveBot.CurrentPresetData.movement;
        
        isBroken = false;
    }

    private IEnumerator OnTransformParentChanged()
    {
        while (integrity == 0)
        {
            if(data != null)
            {
                integrity = data.integrityMultiplier * GetComponentInParent<PhotonHullManager>().integrity;
            }

            yield return new WaitForEndOfFrame();
        }

        HealthManager.moveHealthMax = integrity;
        HealthManager.moveHealth = integrity;
    }

    public void Die(PhotonPlayer killer)  //Don't actually die, just break the movement (reduce speed)
    {
        if (killer != null && killer.isLocal)
        {
            ScoreKeeper.Score += ScoreKeeper.destroyPoints;
        }

        isBroken = true;
        BreakMove();
    }

    [PunRPC]
    public void TakeDamage(float dam, PhotonPlayer player)
    {
        if (!isBroken)
        {
            integrity -= dam;
            HealthManager.moveHealth = integrity;

            if (integrity <= 0)
            {
                Die(player);
            }
        }
    }
}
