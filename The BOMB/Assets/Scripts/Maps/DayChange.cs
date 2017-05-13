using UnityEngine;
using UnityEngine.SceneManagement;

public class DayChange : MonoBehaviour
{

    public static float changeTime = 10;
    string isDaytemp;
    public float changeRate = 0.01f;

    public ColorData colorMang;

    void Awake ()
    {
        SceneManager.sceneLoaded += SceneChanged;
    }

    void SceneChanged(Scene thing, LoadSceneMode other)
    {
        GetComponent<Camera>().backgroundColor = colorMang.dayColor;
        isDaytemp = "null";
    }

    void Update()
    {
        if (isDaytemp != DayNightTrigger.isDay)
        {
            isDaytemp = DayNightTrigger.isDay;
            changeTime = 0;
        }
    }

    void FixedUpdate()
    {
        if (changeTime < 1)
        {
            changeTime = changeTime + changeRate;
            switch (isDaytemp)
            {
                case "day":
                    GetComponent<Camera>().backgroundColor = Color.Lerp(colorMang.dawnColor, colorMang.dayColor, changeTime);
                    break;
                case "sunset":
                    GetComponent<Camera>().backgroundColor = Color.Lerp(colorMang.dayColor, colorMang.sunsetColor, changeTime);
                    break;
                case "night":
                    GetComponent<Camera>().backgroundColor = Color.Lerp(colorMang.sunsetColor, colorMang.nightColor, changeTime);
                    break;
                case "dawn":
                    GetComponent<Camera>().backgroundColor = Color.Lerp(colorMang.nightColor, colorMang.dawnColor, changeTime);
                    break;
            }
        }
        else if (changeTime > 1)
        {
            changeTime = 1;
        }
    }

    void OnDestroy()
    {
        SceneManager.sceneLoaded -= SceneChanged;
    }
}
