using UnityEngine;
using UnityEngine.UI;

public class CreatorCamera : MonoBehaviour
{

    public Transform[] camPositions;
    public int currentPosition;
    public static bool isFullView;

    private float transitionTemp;
    public float transitionSpeed;

    public GameObject fullViewSelecter;
    public Button[] specificViewButtons;

    private CreatorManager creatorMang;

    private void Awake()
    {
        creatorMang = FindObjectOfType<CreatorManager>();
    }

    public void ChangeCameraView(int view)
    {
        currentPosition = view;
        transitionTemp = 0;

        if (currentPosition == 0)
        {
            fullViewSelecter.SetActive(true);

            foreach (Button butt in specificViewButtons)
            {
                butt.interactable = false;
            }

            creatorMang.ChangeActivePartType("none");
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
        transform.position = Vector3.Lerp(transform.position, camPositions[currentPosition].position, transitionTemp);
        transform.rotation = Quaternion.Lerp(transform.rotation, camPositions[currentPosition].rotation, transitionTemp);

        if (transitionTemp >= 1)
        {
            //Debug.Log("Reseting camera transition.");
            CancelInvoke("TrainsitionTransform");
            transitionTemp = 0;
        }
        else
        {
            transitionTemp += transitionSpeed;
        }
    }
}
