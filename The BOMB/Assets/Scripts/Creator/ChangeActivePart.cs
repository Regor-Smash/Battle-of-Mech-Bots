using UnityEngine;

public class ChangeActivePart : MonoBehaviour
{
    public int camPosNum;
    public enum PartTypes { Hull, Movement, Gadget, WeaponL, WeaponR }
    public PartTypes partType;

    CreatorManager creatorMang;
    CreatorCamera cam;

    void Start()
    {
        cam = FindObjectOfType<CreatorCamera>();
        creatorMang = FindObjectOfType<CreatorManager>();
    }

    public void ChangePartType()
    {
        cam.ChangeCameraView(camPosNum);
        creatorMang.ChangeActivePartType(partType.ToString());
    }
}
