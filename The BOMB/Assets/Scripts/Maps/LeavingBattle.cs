using UnityEngine;
using UnityEngine.UI;

public class LeavingBattle : MonoBehaviour
{
    GameObject timerPanel;
    Text countdown;

    int startTimer = 5;
    int timeLeft;

    bool leftBattle;

    void Awake()
    {
        if (!GetComponent<PhotonView>().isMine)
        {
            Destroy(this);
        }
    }

    void Start()
    {
        timerPanel = GetComponent<PhotonHullManager>().HUD.transform.FindChild("Leaving Battlefield Panel").gameObject;
        countdown = timerPanel.transform.FindChild("Countdown Text").GetComponent<Text>();
    }

	void LeaveBattle()
	{
        timeLeft = startTimer;
        InvokeRepeating("TimeDown", 1, 1);

        countdown.text = timeLeft.ToString();
        timerPanel.SetActive(true);
    }
	
	void EnterBattle()
	{
        timeLeft = startTimer;
        CancelInvoke("TimeDown");

        timerPanel.SetActive(false);
    }

    void TimeDown()
    {
        if (timeLeft == 0)
        {
            GetComponent<PhotonHullManager>().Die(null);
        }
        else
        {
            timeLeft--;
            countdown.text = timeLeft.ToString();
        }
    }

    void OnTriggerExit(Collider col)
    {
        if (col.CompareTag("Battlefield") && !leftBattle)
        {
            LeaveBattle();
            leftBattle = true;
        }
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.CompareTag("Battlefield") && leftBattle)
        {
            EnterBattle(); ;
            leftBattle = false;
        }
    }
}
