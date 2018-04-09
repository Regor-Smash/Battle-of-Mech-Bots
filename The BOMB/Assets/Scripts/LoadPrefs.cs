using UnityEngine;

public class LoadPrefs : MonoBehaviour
{
#if UNITY_EDITOR
    public bool resetPlayerPrefs = false;
#endif

    void Start()
    {
#if UNITY_EDITOR
        if (resetPlayerPrefs)
        {
            PlayerPrefs.DeleteAll();
        }
#endif

        //Load username
        PhotonNetwork.player.NickName = Helper.Prefs.GetString("Username");

        //Load bot presets
        SaveBot.LoadPresets();
        if (SaveBot.CurrentPresetData == null)
        {
            SaveBot.SaveAllPresets();
        }

        //Load options
        Options.sensitivity = Helper.Prefs.GetFloat("Sensitivity");
        Options.invertY = Helper.Prefs.GetInt("Invert Y");
    }
}
