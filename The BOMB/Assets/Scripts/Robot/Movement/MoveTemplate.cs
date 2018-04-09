using UnityEngine;

public abstract class MoveTemplate : MonoBehaviour
{
    protected MovementData data;
    protected Rigidbody hullRG;

    protected float power;
    protected float jumpPower;
    protected float backMod;

    protected int capacity;
    protected Vector3 maxSpeed;
    protected Vector3 minSpeed;
    Vector3 speedLimitHolder;
    protected Vector3 speedLimit
    {
        get
        {
            return speedLimitHolder;
        }
        set
        {
            speedLimitHolder = value;

            Vector3 tempVelocity = currentVelocity;
            if (tempVelocity.z > speedLimitHolder.z || tempVelocity.z < -speedLimitHolder.z * backMod)   //If going faster than new speed limit
            {
                hullRG.AddRelativeForce(0, 0, speedLimit.z - tempVelocity.z, ForceMode.VelocityChange);
            }
        }
    }

    protected Vector3 currentVelocity
    {
        get
        {
            return transform.InverseTransformDirection(hullRG.velocity);
        }
    }

    protected Ray ray;
    protected RaycastHit hit;

    protected bool isJump;
    protected bool grounded;
    protected float groundRange;

    //Calibration Vars
    public bool testRange;
    GameObject rangeIndicatorHolder;
    protected GameObject rangeIndicator
    {
        get
        {
            if (rangeIndicatorHolder == null)
            {
                rangeIndicatorHolder = (GameObject)Instantiate(Resources.Load("Simple Plane"));
            }
            return rangeIndicatorHolder;

        }
    }
    public bool Calibrating;
    protected int TimeA = 0;

    protected virtual void Awake()
    {
        if (gameObject.GetComponent<PhotonView>().isMine)
        {
            enabled = true;

            data = SaveBot.CurrentPresetData.movement;
            power = data.power;
            jumpPower = data.jumpPower;
            backMod = data.backwardsMod;
            capacity = data.capacity;
            maxSpeed = data.maxSpeed;
            minSpeed = data.minSpeed;
            //moveSize = data.moveSize;

            PhotonMoveIntegrity.BreakMove += BreakMove;
            PhotonMoveIntegrity.ResetMove += ResetMove;
        }
    }

    protected virtual void Start()
    {
        hullRG = transform.parent.GetComponent<Rigidbody>();
        groundRange = GetComponent<Collider>().bounds.size.y + 1;

        ResetMove();
    }

    public virtual void ResetMove()
    {
        if (hullRG.mass > capacity) //Over capacity
        {
            speedLimit = (maxSpeed + minSpeed) / 2;
        }
        else
        {
            speedLimit = maxSpeed;
        }
    }

    public virtual void BreakMove()
    {
        speedLimit = minSpeed;
    }

    protected void Calibration()
    {
        if (Input.GetButton("Forward") && transform.InverseTransformDirection(hullRG.velocity).z > (maxSpeed.z - 1))
        {
            Debug.Log(TimeA);
        }
        else if (Input.GetButton("Forward"))
        {
            Debug.Log("Accelerating");
            TimeA = TimeA + 1;
        }
        else
        {
            TimeA = 0;
        }
    }
}
