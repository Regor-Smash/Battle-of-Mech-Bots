using UnityEngine;

public class SelfDestructed : MonoBehaviour
{
    void Update()
    {
        if (GetComponent<Respawn>().time > 0)    //Then we have self-destructed
        {
            Invoke("TriggerEvent", 3);
            GetComponent<Respawn>().time = 0;
        }
    }

    void TriggerEvent()
    {
        FindObjectOfType<TutorialTracker>().ConditionMet(7);
    }
}
