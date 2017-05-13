using UnityEngine;

public class ChangeActivePart : MonoBehaviour
{
    public int camPosNum;
    public enum PartTypes { Hull, Movement, Gadget, WeaponL, WeaponR }
    public PartTypes partType;

    SaveBot botSaver;
    CreatorCamera cam;

    void Start()
    {
        cam = FindObjectOfType<CreatorCamera>();
        botSaver = FindObjectOfType<SaveBot>();
    }

    public void ChangePartType()
    {
        cam.ChangeCameraView(camPosNum);
        botSaver.ChangeActivePartType(partType.ToString());
    }
}
