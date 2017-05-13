using UnityEngine;

public class MoveRAM : MoveTemplate
{
    void FixedUpdate()
    {
        if (!MultiplayerPause.isPaused && Input.GetButton("Forward"))
        {
            Vector3 tempVelocity = currentVelocity;

            if (tempVelocity.z < speedLimit.z && tempVelocity.z > -speedLimit.z * backMod)   //Less than max forward/backward speed
            {
                hullRG.AddRelativeForce(0, 0, power * Input.GetAxis("Forward"), ForceMode.Acceleration);
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
