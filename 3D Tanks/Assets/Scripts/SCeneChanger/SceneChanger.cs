using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    public void GoToActOne()
    {
        SceneManager.LoadScene("Act One");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
