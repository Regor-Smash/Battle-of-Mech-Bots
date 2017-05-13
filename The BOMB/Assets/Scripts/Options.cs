using UnityEngine;
using UnityEngine.UI;

public class Options : MonoBehaviour
{
    //Network Options
    public InputField NameInput;
    public Toggle Offline;

    //Gameplay Options
    public Toggle invertToggle;
    public Slider sensitivityBar;
    public Text sensitivityVal;

    //Variables
    public static bool isOffline;
    public static float sensitivity;
    public static int invertY = 1;

    void Start()
    {
        NameInput.text = PhotonNetwork.playerName;
        Offline.isOn = isOffline;

        if(invertY == 1)
        {
            invertToggle.isOn = false;
        }
        else if (invertY == -1)
        {
            invertToggle.isOn = true;
        }
        else if (invertY == 0)
        {
            invertY = 1;
            invertToggle.isOn = false;
            PlayerPrefs.SetInt("Invert Y", invertY);
        }

        if (sensitivity == 0)
        {
            sensitivity = Helper.Prefs.GetFloat("Sensitivity");
            if (sensitivity== 0)
            {
                Debug.Log("Sensitivity at 0!");
                sensitivity = sensitivityBar.minValue;
                PlayerPrefs.SetFloat("Sensitivity", sensitivity);
            }
        }
        sensitivityBar.value = sensitivity;
    }

    public void OfflineToggle(bool offline)
    {
        isOffline = offline;
        //Debug.Log("Offline Mode = " + isOffline);
    }

    public void Username(string name)
    {
        PhotonNetwork.playerName = name;
        PlayerPrefs.SetString("Username", name);
        //Debug.Log (NameText.text);
    }

    public void SetSensitivity(float value)
    {
        sensitivity = value;
        sensitivityVal.text = ((int)sensitivity).ToString();

        PlayerPrefs.SetFloat("Sensitivity", sensitivity);
    }

    public void SetInverted (bool isInvert)
    {
        if (isInvert)
        {
            invertY = -1;
        }
        else
        {
            invertY = 1;
        }
        PlayerPrefs.SetInt("Invert Y", invertY);
    }
}
