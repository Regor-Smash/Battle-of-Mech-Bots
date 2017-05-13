using UnityEngine;

public class MoveLev : MoveTemplate
{
    protected override void Start()
    {
        base.Start();
        hullRG.useGravity = false;
    }

    void FixedUpdate()
    {
        if (!MultiplayerPause.isPaused)
        {
            Vector3 tempVelocity = currentVelocity;

            //Move up/down
            if ((Input.GetButton("Jump") || Input.GetButton("Crouch")) && tempVelocity.y < speedLimit.y && tempVelocity.y > -speedLimit.y)
            {
                hullRG.AddRelativeForce(0, jumpPower * (Input.GetAxis("Jump") - Input.GetAxis("Crouch")), 0, ForceMode.Acceleration);
            }

            //Move forward/backward
            if (Input.GetButton("Forward") && tempVelocity.z < speedLimit.z && tempVelocity.z > -speedLimit.z)
            {
                hullRG.AddRelativeForce(0, 0, power * Input.GetAxis("Forward"), ForceMode.Acceleration);
            }

            //Move left/right
            if (Input.GetButton("Strafe") && tempVelocity.x < speedLimit.x && tempVelocity.x > -speedLimit.x)
            {
                hullRG.AddRelativeForce(power * Input.GetAxis("Strafe"), 0, 0, ForceMode.Acceleration);
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