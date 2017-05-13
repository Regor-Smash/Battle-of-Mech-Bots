using UnityEngine;
using System.Collections;

public class MakeLightning : MonoBehaviour
{
    LineRenderer lightning;
    AudioSource thunder;

    public int numberOfPoints;
    public float height;
    public float maxDist;

    public bool strike;

	void Start()
	{
        lightning = GetComponent<LineRenderer>();
        thunder = GetComponent<AudioSource>();

        LightningBolt();
    }

    void Update ()
    {
        if (strike)
        {
            LightningBolt();
            strike = false;
        }
    }
	
	public void LightningBolt()
	{
        lightning.positionCount = numberOfPoints + 1;
        float segHeight = height / numberOfPoints;  //segment height

        lightning.enabled = true;
        for (int a = 0; a <= numberOfPoints; a++)
        {
            float xOffset = Random.Range(-maxDist / (a + 1), maxDist / (a + 1));
            float zOffset = Random.Range(-maxDist / (a + 1), maxDist / (a + 1));
            lightning.SetPosition(a, new Vector3(xOffset, (-segHeight * a), zOffset));
        }
        thunder.Play();

        RaycastHit hit;
        if (Physics.Raycast(transform.position, -transform.up, out hit, height))
        {
            if (hit.collider.GetComponent<PhotonHullManager>())
            {
                hit.collider.GetComponent<PhotonHullManager>().UseEnergy(-100);
            }
        }
        Invoke("LightningOff", 0.1f);
    }

    void LightningOff ()
    {
        lightning.enabled = false;
    }
}
