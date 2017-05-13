using UnityEngine;
using UnityEngine.UI;

public class Hyperlink : MonoBehaviour
{
    //Put on a button
    Text link;

    void Start()
    {
        link = GetComponentInChildren<Text>();
        link.fontStyle = FontStyle.Italic;
        link.color = Color.blue;
    }

    public void OpenLink()
    {
        Application.OpenURL(link.text);
    }
}
