using UnityEngine;

public class CreatorHull : MonoBehaviour
{
    GameObject Hull;
    
    void Awake()
    {
        SaveBot.HullUpdate += UpdateHull;
    }

    void UpdateHull()
    {
        if (Hull != null)
        {
            Destroy(Hull);
        }
        Hull = (GameObject)Instantiate(SaveBot.CurrentPresetData.hull.partVisuals, gameObject.transform.position, gameObject.transform.rotation);
    }

    private void OnDestroy()
    {
        SaveBot.HullUpdate -= UpdateHull;
    }
}