using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class VolumeBar : MonoBehaviour
{
    //Goes on the volume bar's title
    private Slider bar;
    public AudioMixer mixer;
    public string varName;
    
    void Start()
    {
        bar = GetComponentInChildren<Slider>();

        float tempVol;
        mixer.GetFloat(varName, out tempVol);
        bar.value = tempVol;
    }
    
    public void SetVolume(float vol)
    {
        mixer.SetFloat(varName, vol);
    }
}
