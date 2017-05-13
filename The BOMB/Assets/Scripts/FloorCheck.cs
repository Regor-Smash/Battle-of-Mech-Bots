using UnityEngine;

public class FloorCheck : MonoBehaviour
{
    public bool doCheck = true;

    private Collider myCol;
    private float lowestY;
    private bool onFloor;
    private Vector3 pos
    {
        get
        {
            return transform.position;
        }
    }

    public delegate void MoveCheck();
    public MoveCheck HitFloor;
    public MoveCheck LeftFloor;
    public MoveCheck MovingUp;

    private void Awake()
    {
        myCol = GetComponent<Collider>();
        lowestY = transform.InverseTransformPoint((myCol.ClosestPointOnBounds(pos + new Vector3 (0, -1, 0) - myCol.bounds.extents))).y;
    }

    private void FixedUpdate()
    {
        RaycastHit hit;
        if (Physics.Raycast(new Vector3(pos.x, pos.y, pos.z), Vector3.down, out hit, 0.01f + Helper.Abs(lowestY)))   //Ray hit either under or inside GO
        {
            float hitDist = Vector3.Distance(hit.point, pos);
            if (hitDist >= lowestY && !onFloor) //Hit under (GO is on or near the ground)
            {
                onFloor = true;
                HitFloor();
                //Debug.Log(gameObject.name + " hit the floor.");
            }
            else if (hitDist < lowestY && onFloor)  //GO is phasing through ground
            {
                MovingUp();
                //Debug.Log(gameObject.name + ": Floor is moving up and through me.");
            }
            else if (hitDist >= lowestY && onFloor)  //Still on floor, do nothing
            {

            }
            else
            {
                Debug.Log("I broke the FLoorCheck.");
            }
        }
        else if (onFloor)
        {
            onFloor = false;
            LeftFloor();
        }
    }

    /*private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Thing");
        if (doCheck)
        {
            foreach (ContactPoint cPoint in collision.contacts)
            {
                if (cPoint.point.y <= (pos.y + lowestY))  //Contact is below collider so collider is grounded
                {
                    HitFloor();
                    Debug.Log("Hit the floor.");
                    break;
                }
                else  //Contact is above lowest point which means it is inside collider and collider needs to move up
                {
                    MovingUp();
                    break;
                }
            }
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        foreach (ContactPoint cPoint in collision.contacts)
        {
            if (cPoint.point.y <= (pos.y + lowestY))  //Contact is below collider so collider was grounded
            {
                LeftFloor();
                break;
            }
        }
    }*/
}
