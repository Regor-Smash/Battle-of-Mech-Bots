using UnityEngine;
using System.Collections.Generic;
using System;

public class TutorialEnabler : MonoBehaviour
{
    public Dictionary<string, CompSet> allCompSets = new Dictionary<string, CompSet>();
    string[] compSetsNames = new string[] { "looking", "moving", "firing", "pairs" };
    public CompSet[] compSets;
    
    void Start()
    {
        FindObjectOfType<TutorialTracker>().enabler = this;

#if UNITY_EDITOR
        if (compSets.Length != compSetsNames.Length)
        {
            Debug.LogError("Component Sets do not align with Component Set Names! " +
                "Sets: " + compSets.Length + ", Names: " + compSetsNames.Length);
            return;
        }
#endif
        for (int a = 0; a < compSets.Length; a++)
        {
            allCompSets.Add(compSetsNames[a], compSets[a]);
            foreach (MonoBehaviour comp in compSets[a].compsToEnable)
            {
                comp.enabled = false;
            }
        }
    }

    public void EnableComps(string setname)
    {
        if (allCompSets[setname].includeGOs)
        {
            foreach (GameObject go in allCompSets[setname].GOsToEnable)
            {
                go.SetActive(true);
            }
        }

        foreach (MonoBehaviour comp in allCompSets[setname].compsToEnable)
        {
            comp.enabled = true;
        }
    }
}

[Serializable]
public class CompSet
{
    public MonoBehaviour[] compsToEnable;
    public bool includeGOs;
    public GameObject[] GOsToEnable;
}
