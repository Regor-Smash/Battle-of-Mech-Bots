using UnityEngine;

public class FakeGravity : MonoBehaviour
{
    public float terminalVel;
    public Collider bottomCollider;

    private float physicsTime;
    private bool falling = true;

    private Vector3 currentVel;
    private Vector3 acceleration;
    private Vector3 grav;

    private void Awake()
    {
        physicsTime = Time.fixedDeltaTime;
        grav = Physics.gravity;

        acceleration = grav * physicsTime;
        
        bottomCollider.gameObject.AddComponent<FloorCheck>();
        bottomCollider.GetComponent<FloorCheck>().HitFloor += StopFall;
        bottomCollider.GetComponent<FloorCheck>().LeftFloor += StartFall;
    }

    
    private void FixedUpdate()
    {
        if (Helper.Abs(currentVel.magnitude) < Helper.Abs(terminalVel) && falling) //Hasn't reached terminal velocity, keep accelerating
        {
            currentVel += acceleration;
        }
        else   //Has reached terminal velocity, stop accelerating
        {
            
        }

        if (falling)
        {
            transform.Translate(currentVel * physicsTime);
        }
    }

    private void StopFall()
    {
        falling = false;
    }

    private void StartFall()
    {
        falling = true;
    }

    private void OnDestroy()
    {
        bottomCollider.GetComponent<FloorCheck>().HitFloor -= StopFall;
        bottomCollider.GetComponent<FloorCheck>().LeftFloor -= StartFall;
    }
}
