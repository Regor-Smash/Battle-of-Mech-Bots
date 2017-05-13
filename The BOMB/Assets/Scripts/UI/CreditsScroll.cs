using UnityEngine;
using UnityEngine.SceneManagement;

public class CreditsScroll : MonoBehaviour
{
    public float scrollSpeed;

    void Start()
    {
        transform.localPosition = Vector3.zero; //new Vector3(transform.localPosition.x, 0, transform.localPosition.z);
    }

    void Update()
    {
        transform.Translate(0, scrollSpeed, 0);

        if (Input.anyKeyDown || transform.localPosition.y > GetComponent<RectTransform>().sizeDelta.y)
        {
            SceneManager.LoadScene("Main Menu");
        }
    }
}
