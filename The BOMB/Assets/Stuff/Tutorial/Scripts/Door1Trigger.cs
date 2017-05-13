using UnityEngine;

public class Door1Trigger : MonoBehaviour
{
    void Update()
    {
        if (GetComponent<MeshRenderer>().isVisible)
        {
            //Trigger a thing
            //FindObjectOfType<TutorialTracker>().Invoke("ConditionMet", 0.1f);
            FindObjectOfType<TutorialTracker>().ConditionMet(2);

            DestroyImmediate(this);
        }
    }
}
