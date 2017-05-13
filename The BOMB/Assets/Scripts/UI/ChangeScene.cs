using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
    public bool loadingMap;

    public void Scene(string sceneName)
    {
        if (PhotonNetwork.connected)
        {
            PhotonNetwork.Disconnect();
        }
        if (loadingMap)
        {
            SceneManager.LoadScene("Scenes/Maps/" + sceneName);
        }
        else if (!loadingMap)
        {
            SceneManager.LoadScene("Scenes/" + sceneName);
        }
    }
}
