using UnityEngine;

public class MovePogo : MoveTemplate
{
    //minSpeed.x = sideways horizontal power modifier
    //minSpeed.y = min vertical/jump power
    //minSpeed.z = min horizontal power

    //Normal (Horizontal) power variables
    private float maxPower;
    private float minPower;
    private float currentPower;
    private float sideMod;

    //Jump (Vertical) power variables
    private float maxJPower;
    private float minJPower;
    private float currentJPower;

    protected override void Awake()
    {
        base.Awake();

        maxPower = power;
        minPower = minSpeed.z;
        sideMod = minSpeed.x;

        maxJPower = jumpPower;
        minJPower = minSpeed.y;
    }

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

        if (Input.GetButtonDown("Jump") && grounded)
        {
            isJump = true;
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
            if (isJump)
            {
                isJump = false;

                hullRG.AddRelativeForce(currentPower * sideMod * Input.GetAxis("Strafe"), currentJPower,
                    currentPower * Input.GetAxis("Forward"), ForceMode.Impulse);
            }

            if (grounded && hit.collider.CompareTag("Bounce"))
            {
                hullRG.AddForce(0, 700, 0, ForceMode.Impulse);
            }
        }
        
        if (Calibrating)
        {
            Calibration();
        }
    }

    public override void ResetMove()
    {
        if (hullRG.mass > capacity) //Over capacity
        {
            currentPower = (maxPower + minPower) / 2;
            currentJPower = (maxJPower + minJPower) / 2;
        }
        else
        {
            currentPower = maxPower;
            currentJPower = maxJPower;
        }
    }

    public override void BreakMove()
    {
        currentPower = minPower;
        currentJPower = minJPower;
    }
}
