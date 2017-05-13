using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{

    public Text counter;
    public int startTime;
    int currentTime;

    void Start()
    {
        currentTime = startTime;
        counter.text = currentTime.ToString();
        InvokeRepeating("DecreaseTime", 1, 1);
    }
    
    void DecreaseTime()
    {
        if (currentTime == 0)
        {
            Debug.Log("Time is up on " + name);
            //Do something
        }
        currentTime--;
        counter.text = currentTime.ToString();
    }
}
