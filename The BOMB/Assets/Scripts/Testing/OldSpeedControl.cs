using UnityEngine;

public class OldSpeedControl : MonoBehaviour
{
    //Limits velocity under any circumstances
    //Discarded to allow more flexibility with outside forces

    public Vector3 maxSpeed;
    public float backMod;

    Rigidbody hullRG;

    void Start()
    {
        hullRG = transform.parent.GetComponent<Rigidbody>();
    }

    void MaxSpeedControl()
    {
        if (transform.InverseTransformDirection(hullRG.velocity).x >= maxSpeed.x)
        {
            hullRG.AddRelativeForce(maxSpeed.x - transform.InverseTransformDirection(hullRG.velocity).x, 0, 0, ForceMode.VelocityChange);
            //Debug.Log ("Hit max right speed");
        }
        else if (transform.InverseTransformDirection(hullRG.velocity).x <= -maxSpeed.x)
        {
            hullRG.AddRelativeForce(-maxSpeed.x - transform.InverseTransformDirection(hullRG.velocity).x, 0, 0, ForceMode.VelocityChange);
            //Debug.Log ("Hit max left speed");
        }

        if (transform.InverseTransformDirection(hullRG.velocity).z >= maxSpeed.z)
        {
            hullRG.AddRelativeForce(0, 0, maxSpeed.z - transform.InverseTransformDirection(hullRG.velocity).z, ForceMode.VelocityChange);
            //Debug.Log ("Hit max forward speed");
        }
        else if (transform.InverseTransformDirection(hullRG.velocity).z <= -maxSpeed.z * backMod)
        {
            hullRG.AddRelativeForce(0, 0, -maxSpeed.z * backMod - transform.InverseTransformDirection(hullRG.velocity).z, ForceMode.VelocityChange);
            //Debug.Log ("Hit max backwards speed");
        }

        if (transform.InverseTransformDirection(hullRG.velocity).y >= maxSpeed.y)
        {
            hullRG.AddRelativeForce(0, maxSpeed.y - transform.InverseTransformDirection(hullRG.velocity).y, 0, ForceMode.VelocityChange);
            //Debug.Log ("Hit max upwards speed");
        }
        else if (transform.InverseTransformDirection(hullRG.velocity).y <= -maxSpeed.y)
        {
            hullRG.AddRelativeForce(0, -maxSpeed.y - transform.InverseTransformDirection(hullRG.velocity).y, 0, ForceMode.VelocityChange);
            //Debug.Log ("Hit max downwards speed");
        }
    }
}
