using UnityEngine;

public class MoveRocketFly : MoveTemplate
{
    //int groundMod;
    //float floatAxis;

    void Update()
    {
        ray = new Ray(transform.position, -transform.up);

        if (Physics.Raycast(ray, out hit, groundRange) && hit.collider.gameObject != gameObject)
        {
            grounded = true;
        }
        else
        {
            grounded = false;
        }

        if (testRange)
        {
            rangeIndicator.transform.position = ray.GetPoint(groundRange);
        }
    }

    void FixedUpdate()
    {
        if (!MultiplayerPause.isPaused)
        {
            Vector3 tempVelocity = currentVelocity;

            if (Input.GetButton("Jump") && tempVelocity.y < speedLimit.y)
            {
                hullRG.AddRelativeForce(0, jumpPower * Input.GetAxis("Jump"), 0, ForceMode.Force);
            }

            if (!grounded)
            {
                if (Input.GetButton("Forward") && tempVelocity.z < speedLimit.z && tempVelocity.z > -speedLimit.z * backMod)
                {
                    hullRG.AddRelativeForce(0, 0, power * Input.GetAxis("Forward"), ForceMode.Force);
                }

                if (Input.GetButton("Strafe") && tempVelocity.x < speedLimit.x && tempVelocity.x > -speedLimit.x)
                {
                    hullRG.AddRelativeForce(power * Input.GetAxis("Strafe"), 0, 0, ForceMode.Force);
                }
            }
            else if (hit.collider.CompareTag("Bounce"))
            {
                hullRG.AddForce(0, 50, 0, ForceMode.VelocityChange);
            }
        }
        
        if (Calibrating)
        {
            Calibration();
        }
    }
}
