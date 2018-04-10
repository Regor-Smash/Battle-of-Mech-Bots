using UnityEngine;
using UnityEngine.UI;

public class CreatorGadget : MonoBehaviour
{

    GameObject Gadget;
    Text statsText;

    void Awake()
    {
        CreatorManager.GadgetUpdate += UpdateGadget;
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
        CreatorManager.GadgetUpdate -= UpdateGadget;
    }
}
