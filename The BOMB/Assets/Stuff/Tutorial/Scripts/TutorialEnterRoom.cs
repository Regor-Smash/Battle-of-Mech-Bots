using UnityEngine;

public class TutorialEnterRoom : MonoBehaviour
{
    public int eventNum;

    void Start()
    {
        if (!GetComponent<Collider>())
        {
            Debug.Log("You need to add a collider to " + gameObject.name + ".");
        }
        else if (!GetComponent<Collider>().isTrigger)
        {
            Debug.Log("You need to change the collider on " + gameObject.name + " to a trigger.");
        }
    }

    void OnTriggerEnter(Collider col)
    {
        FindObjectOfType<TutorialTracker>().ConditionMet(eventNum);
        Destroy(this);
    }
}
