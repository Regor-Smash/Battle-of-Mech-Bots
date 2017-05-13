using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class TutorialTracker : MonoBehaviour
{
    public List<AudioClip> voices;
    public GameObject[] uiHints;
    public AudioClip firingInstruct;

    public RaiseDoor door1;
    public RaiseDoor door2;
    public RaiseDoor targetsDoor;
    [HideInInspector] public TutorialEnabler enabler;

    private AudioSource speaker;
    private int voiceIndex = 1;
    private bool hasTriggeredNew = false;
    
    void Awake()
    {
        speaker = GetComponent<AudioSource>();
        if (speaker.clip != voices[0])
        {
            speaker.clip = voices[0];
            speaker.Play();
        }
    }
    
    void Update()
    {
        if (!speaker.isPlaying && !hasTriggeredNew) //Perform an action
        {
            //Debug.Log("Last played voice #" + voiceIndex);
            foreach (GameObject go in uiHints)
            {
                go.SetActive(false);
            }

            switch (voiceIndex)
            {
                case 1: //greeting and looking
                    enabler.EnableComps("looking");
                    uiHints[0].SetActive(true);

                    uiHints[0].transform.parent.gameObject.SetActive(true);
                    break;
                case 2: //movement
                    enabler.EnableComps("moving");
                    uiHints[1].SetActive(true);
                    door1.Raise();
                    break;
                case 3: //firing
                    enabler.EnableComps("firing");
                    uiHints[2].SetActive(true);

                    speaker.clip = firingInstruct;
                    speaker.Play();
                    break;
                case 4: //pairs
                    enabler.EnableComps("pairs");
                    uiHints[3].SetActive(true);
                    targetsDoor.Raise();
                    break;
                case 5: //go to next room
                    uiHints[4].SetActive(true);
                    door2.Raise();
                    break;
                case 6: //self-destruct
                    uiHints[5].SetActive(true);
                    break;
                case 7: //end
                    BackToMM();
                    break;
                default:
                    Debug.LogError("No matching actions for end of voice #" + voiceIndex + " found!");
                    break;
            }

            hasTriggeredNew = true;
        }
    }

    public void ConditionMet(int index)
    {
        //Debug.Log("You did the thing");
        hasTriggeredNew = false;
        if (voiceIndex < index)
        {
            voiceIndex = index;
        }
        speaker.clip = voices[voiceIndex - 1];
        speaker.Play();
    }

    void BackToMM()
    {
        SceneManager.LoadScene("Main Menu");
    }
}
