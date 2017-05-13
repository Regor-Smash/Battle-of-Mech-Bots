using UnityEngine;
using UnityEngine.UI;

public class CreatorCamera : MonoBehaviour
{

    public Transform[] camPositions;
    public int currentPosition;
    public static bool isFullView;

    float a;
    public float transitionSpeed;

    public GameObject fullViewSelecter;
    public Button[] specificViewButtons;

    public void ChangeCameraView(int view)
    {
        currentPosition = view;
        a = 0;

        if (currentPosition == 0)
        {
            fullViewSelecter.SetActive(true);

            foreach (Button butt in specificViewButtons)
            {
                butt.interactable = false;
            }

            FindObjectOfType<SaveBot>().ChangeActivePartType("none");
        }
        else
        {
            fullViewSelecter.SetActive(false);
            foreach (MeshRenderer mesh in fullViewSelecter.GetComponentsInChildren<MeshRenderer>())
            {
                mesh.enabled = false;
            }

            foreach (Button butt in specificViewButtons)
            {
                butt.interactable = true;
            }
        }
        
        InvokeRepeating("TrainsitionTransform", transitionSpeed, transitionSpeed);
    }

    void TrainsitionTransform()
    {
        transform.position = Vector3.Lerp(transform.position, camPositions[currentPosition].position, a);
        transform.rotation = Quaternion.Lerp(transform.rotation, camPositions[currentPosition].rotation, a);

        if (a >= 1)
        {
            //Debug.Log("Reseting camera transition.");
            CancelInvoke("TrainsitionTransform");
            a = 0;
        }
        else
        {
            a += transitionSpeed;
        }
    }
}
