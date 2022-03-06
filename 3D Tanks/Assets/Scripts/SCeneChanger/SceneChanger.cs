using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{

    void Start()
    {
        Cursor.lockState = CursorLockMode.None;
    }

    public void GoToActOne()
    {
        SceneManager.LoadScene("Act One");
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void Training()
    {
        SceneManager.LoadScene("Act Two");
    }
}
