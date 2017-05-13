using UnityEngine;

public class ClickCreatorOutlines : MonoBehaviour
{
    public Material invis;
    public Material highlighted;

    ChangeActivePart typeChanger;
    MeshRenderer[] renders;

    void Start()
    {
        typeChanger = GetComponent<ChangeActivePart>();
        renders = GetComponentsInChildren<MeshRenderer>();
    }

    void OnMouseDown()
    {
        typeChanger.ChangePartType();
    }

    void OnMouseEnter()
    {
        foreach (MeshRenderer mesh in renders)
        {
            mesh.enabled = true;
        }
    }

    void OnMouseExit()
    {
        foreach (MeshRenderer mesh in renders)
        {
            mesh.enabled = false;
        }
    }
}
