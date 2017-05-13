using UnityEngine;

public class LogVelocity : MonoBehaviour
{
    Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        if (rb == null)
        {
            Debug.LogError("Can't log velocity of '" + gameObject.name + "' because there is no rigidbody!", gameObject);
            Destroy(this);
        }
    }

    void FixedUpdate()
    {
        Debug.Log(rb.velocity);
    }
}
