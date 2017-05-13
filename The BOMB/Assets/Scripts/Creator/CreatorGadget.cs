using UnityEngine;
using UnityEngine.UI;

public class CreatorGadget : MonoBehaviour
{

    GameObject Gadget;
    Text statsText;

    void Awake()
    {
        SaveBot.GadgetUpdate += UpdateGadget;
    }

    void UpdateGadget()
    {
        /*
        if (Gadget != null)
        {
            Destroy(Gadget);
        }
        Gadget = (GameObject)Instantiate(SaveBot.CurrentPresetData().gadget, gameObject.transform.position, gameObject.transform.rotation);*/
    }

    private void OnDestroy()
    {
        SaveBot.GadgetUpdate -= UpdateGadget;
    }
}
