using UnityEngine;
using UnityEngine.SceneManagement;

public class DayNightTrigger : MonoBehaviour
{

    public static string isDay = "null";

    void OnTriggerEnter(Collider Col)
    {
        if (Col.tag == "Day")
        {
            isDay = "dawn";
        }

        if (Col.tag == "Night")
        {
            isDay = "sunset";
        }
    }

    void OnTriggerExit(Collider Col)
    {
        if (Col.tag == "Day")
        {
            isDay = "day";
        }

        if (Col.tag == "Night")
        {
            isDay = "night";
        }
    }

    void OnDestroy()
    {
        isDay = "null";
    }
}
