using UnityEngine;
using System.Collections;

public class RaiseDoor : MonoBehaviour
{
    public float maxY;
    public float speed;

    private float lerper = 0;
    private bool isRaising = false;

    private Vector3 startPos;
    private Vector3 endPos;

    public void Raise()
    {
        isRaising = true;
        lerper = 0;
    }
    
    void Start()
    {
        startPos = transform.position;
        endPos = new Vector3(transform.position.x, maxY, transform.position.z);
    }
    
    void FixedUpdate()
    {
        if (isRaising)
        {
            if (lerper < 1)
            {
                transform.position = Vector3.Lerp(startPos, endPos, lerper);
                lerper += speed;
            }
            else
            {
                transform.position = endPos;
                lerper = 1;
                isRaising = false;
            }
        }
    }
}
