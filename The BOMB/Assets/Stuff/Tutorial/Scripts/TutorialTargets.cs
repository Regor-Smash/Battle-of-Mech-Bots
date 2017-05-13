using UnityEngine;

public class TutorialTargets : MonoBehaviour
{
    public enum TargetTypes { A, B };
    public TargetTypes targetType;

    private static int targetsLeftA;
    private static int targetsLeftB;

    void Start()
    {
        if (targetType == TargetTypes.A)
        {
            targetsLeftA++;
        }
        if (targetType == TargetTypes.B)
        {
            targetsLeftB++;
        }
    }

    void OnDestroy()
    {
        if (targetType == TargetTypes.A)
        {
            targetsLeftA--;
            if (targetsLeftA == 0)
            {
                FindObjectOfType<TutorialTracker>().ConditionMet(4);
            }
        }
        if (targetType == TargetTypes.B)
        {
            targetsLeftB--;
            if (targetsLeftB == 0)
            {
                FindObjectOfType<TutorialTracker>().ConditionMet(5);
            }
        }
    }
}
