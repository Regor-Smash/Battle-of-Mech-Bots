using UnityEngine;

public class MoveTank : MoveTemplate
{
    //float airMod = 0.5f;

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

        if (Input.GetButtonDown("Jump") && grounded)
        {
            isJump = true;
        }
    }

    void FixedUpdate()
    {
        if (!MultiplayerPause.isPaused)     //Add in-air modifier back?
        {
            Vector3 tempVelocity = currentVelocity;

            //Move forward/backward
            if (Input.GetButton("Forward") /*&& grounded */&& tempVelocity.z < speedLimit.z && tempVelocity.z > -speedLimit.z * backMod)
            {
                hullRG.AddRelativeForce(0, 0, power * Input.GetAxis("Forward"), ForceMode.Force);
            }

            //Move left/right
            if (Input.GetButton("Strafe") /*&& grounded */&& tempVelocity.x < speedLimit.x && tempVelocity.x > -speedLimit.x)
            {
                hullRG.AddRelativeForce(power * Input.GetAxis("Strafe"), 0, 0, ForceMode.Force);
            }

            //Jump
            if (isJump)
            {
                hullRG.AddRelativeForce(0, jumpPower, 0, ForceMode.Impulse);
                isJump = false;
            }

            //Bounce on trampoline
            if (grounded && hit.collider.CompareTag("Bounce"))
            {
                hullRG.AddForce(0, 50, 0, ForceMode.VelocityChange);
            }
        }
        
        if (Calibrating)
        {
            Calibration();
        }
    }

    public override void ResetMove()
    {
        speedLimit = maxSpeed;
    }
}
