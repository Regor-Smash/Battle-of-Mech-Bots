using UnityEngine;

public class Orbit : MonoBehaviour
{
    public Transform center;
    public float orbitSpeed;

    void Update()
    {
        transform.LookAt(center);
        if (0 <= transform.rotation.eulerAngles.y && transform.rotation.eulerAngles.y < 180)
        {
            transform.Translate(0, -orbitSpeed, 0);
        }
        else if (180 <= transform.rotation.eulerAngles.y && transform.rotation.eulerAngles.y < 360)
        {
            transform.Translate(0, orbitSpeed, 0);
        }

    }
}
