using UnityEngine;

public class MouseLookX : MonoBehaviour
{
    //HULL

    private float lookSpeed;// = 800f;
    //Rigidbody rb;

    void Awake()
    {
        if (gameObject.GetComponent<PhotonView>().isMine)
        {
            enabled = true;
            //rb = transform.GetComponent <Rigidbody> ();

            lookSpeed = Options.sensitivity;
            if (lookSpeed == 0)
            {
                Options.sensitivity = Helper.Prefs.GetFloat("Sensitivity");
                lookSpeed = Options.sensitivity;
            }
        }
    }

    void Update()
    {
        float x = Input.GetAxis("Mouse X") * lookSpeed;
        if (!MultiplayerPause.isPaused)
        {
            transform.Rotate(0, x, 0);
            //rb.AddTorque (0,x,0, ForceMode.VelocityChange);
        }
    }
}
