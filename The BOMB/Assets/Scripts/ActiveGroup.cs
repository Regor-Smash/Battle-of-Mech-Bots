using UnityEngine;

public class ActiveGroup : MonoBehaviour
{
    public string groupID;
    private static bool starting = true;

    delegate void ActivateMember(string id, ActiveGroup item);
    static event ActivateMember UpdateMember;

    private void Awake()
    {
        UpdateMember += CheckGroup;
        if (starting)
        {
            starting = false;
            Invoke("SetMember",0.01f);
        }
    }

    public void SetMember()
    {
        UpdateMember(groupID, this);
    }

    void CheckGroup(string id, ActiveGroup item)
    {
        if (string.IsNullOrEmpty(groupID))
        {
            Debug.LogError("Group ID for the ActiveGroup on " + gameObject.name + " is empty!", this);
        }
        else if (id == groupID)
        {
            if (item != this)
            {
                gameObject.SetActive(false);
            }
            else
            {
                gameObject.SetActive(true);
            }
        }
    }

    private void OnDestroy()
    {
        UpdateMember -= CheckGroup;

        if (!starting)
        {
            starting = true;
        }
    }
}
