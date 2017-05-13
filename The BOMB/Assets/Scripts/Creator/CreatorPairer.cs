using UnityEngine;
using UnityEngine.UI;

public class CreatorPairer : MonoBehaviour
{
    public int currentPair { get; private set; }
    public static int indexAdjust { get; private set; }
    public Text pairText;
    
    void Awake()
    {
        SaveBot.PairUpdate += UpdatePair;
    }

    void UpdatePair()
    {
        //Debug.Log("Update Pair");
        if (currentPair > (SaveBot.CurrentPresetData.hull.maxPairs - 1))
        {
            currentPair = 0;
        }
        indexAdjust = currentPair * 2;

        pairText.text = "Pair: " + (currentPair + 1) + "/" + SaveBot.CurrentPresetData.hull.maxPairs;

        SaveBot.WeaponUpdates();
    }

    public void ChangePair(bool increase)
    {
        currentPair = Helper.ChangeIndexNum(SaveBot.CurrentPresetData.hull.maxPairs, currentPair, increase);
        UpdatePair();
    }

    private void OnDestroy()
    {
        SaveBot.PairUpdate -= UpdatePair;
    }
}
