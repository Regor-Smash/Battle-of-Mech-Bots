using UnityEngine;

public class Respawn : MonoBehaviour
{
    [HideInInspector]
    public int time = 0;
    GameObject[] hullSpawns;
    int x;

    void Start()
    {
        hullSpawns = GameObject.FindGameObjectsWithTag("Respawn");
    }

    void FixedUpdate()
    {
        if (time > 0)
        {
            x = Random.Range(0, hullSpawns.Length);
            hullSpawns[x].GetComponent<SpawnHullPhoton>().Invoke("SpawnHull", time);
            Invoke("CamOff", time);
            time = 0;
        }
    }

    void CamOff()
    {
        Cursor.lockState = CursorLockMode.Locked;
        gameObject.GetComponent<Camera>().enabled = false;
        gameObject.GetComponent<AudioListener>().enabled = false;
    }
}
