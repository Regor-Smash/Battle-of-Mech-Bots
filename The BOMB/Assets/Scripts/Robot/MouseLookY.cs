using UnityEngine;

public class MouseLookY : MonoBehaviour
{
    //CAMERA

    public float lookSpeed;// = 800f;
    public int CamMax = 300;
    public int CamMin = 25;
    public bool printDebug;

    void Awake()
    {
        if (gameObject.GetComponentInParent<PhotonView>().isMine)
        {
            enabled = true;

            lookSpeed = Options.sensitivity * Options.invertY;
            if (lookSpeed == 0)
            {
                Options.sensitivity = Helper.Prefs.GetFloat("Sensitivity");
                lookSpeed = Options.sensitivity * Options.invertY;
            }
        }
    }

    void Update()
    {
        float LookY = Input.GetAxis("Mouse Y") * -lookSpeed;
        float RotX = transform.eulerAngles.x;
        if (!MultiplayerPause.isPaused)
        {
            transform.Rotate(LookY, 0, 0);
        }
        if (180 > RotX && RotX > CamMin + 1)
        {
            transform.eulerAngles = new Vector3(CamMin, transform.eulerAngles.y, transform.eulerAngles.z);
        }
        else if (CamMax - 1 > RotX && RotX > 180)
        {
            transform.eulerAngles = new Vector3(CamMax, transform.eulerAngles.y, transform.eulerAngles.z);
        }
        //transform.localRotation = Quaternion.Euler (transform.rotation.eulerAngles.x, 0, 0);
    }
}
