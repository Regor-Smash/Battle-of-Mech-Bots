using UnityEngine;

public class MovePlane : MoveTemplate 
{
    float autoPower;
    bool isFlying = true;
    ParticleSystem[] jets;
    
    float manualSpeedDif;

    protected override void Awake()
    {
        base.Awake();

        if (gameObject.GetComponent<PhotonView>().isMine)
        {
            autoPower = power;
            manualSpeedDif = maxSpeed.z * data.backwardsMod;

            jets = GetComponentsInChildren<ParticleSystem>();
        }
    }

    protected override void Start()
    {
        base.Start();
        hullRG.useGravity = false;
    }

    void Update ()
    {
        if (Input.GetButtonDown("Jump"))
        {
            isFlying = !isFlying;
            hullRG.useGravity = !isFlying;

            if (isFlying)
            {
                foreach (ParticleSystem pSys in jets)
                {
                    pSys.Play();
                }
            }
            else
            {
                foreach (ParticleSystem pSys in jets)
                {
                    pSys.Stop();
                }
            }
        }
    }

    void FixedUpdate()
    {
        if (!MultiplayerPause.isPaused && isFlying)
        {
            Vector3 tempVelocity = currentVelocity;

            float tempMaxSpeed = speedLimit.z + (manualSpeedDif * Input.GetAxis("Forward"));
            if (tempVelocity.z < tempMaxSpeed)
            {
                hullRG.AddRelativeForce(0, 0, autoPower, ForceMode.Force);
            }
        }

        if (Calibrating)
        {
            Calibration();
        }
    }
}
