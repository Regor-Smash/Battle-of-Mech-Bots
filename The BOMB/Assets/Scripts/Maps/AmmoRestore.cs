using UnityEngine;
using System.Collections;

public class AmmoRestore : MonoBehaviour
{
    bool canPickup = true;
    public int scrapRestoreTime;    //Time in seconds

    public Material scrapsFull;
    public Material scrapsEmpty;

    void OnTriggerEnter(Collider col)
    {
        if (canPickup && col.GetComponentInParent<PhotonHullManager>())
        {
            col.SendMessageUpwards("AddAmmoHull", SendMessageOptions.RequireReceiver);
            canPickup = false;
            GetComponent<MeshRenderer>().materials[0] = scrapsEmpty;

            Invoke("RestoreScraps", scrapRestoreTime);
        }
    }

    void RestoreScraps ()
    {
        canPickup = true;
        GetComponent<MeshRenderer>().materials[0] = scrapsFull;
    }
}
