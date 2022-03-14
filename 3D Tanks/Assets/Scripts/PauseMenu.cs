using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] GameObject PAuseMEnu;

    [SerializeField] KeyCode PAuseMEnuKEy;
    [SerializeField] KeyCode BACKKEy;

    [SerializeField] int sceneNumber = 0;

    bool canPAuse;
    bool canBAck;
    void Start()
    {
        canPAuse = true;
        canBAck = false;
        PAuseMEnu.SetActive(false);
        Time.timeScale = 1;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(PAuseMEnuKEy) && canPAuse)
        {
            PAuseMEnu.SetActive(true);
            Cursor.lockState = CursorLockMode.None;
            canPAuse = false;
            canBAck = true;
            Time.timeScale = 0;
        }

        if(Input.GetKeyDown(BACKKEy) && canBAck)
        {
            Back();
            Time.timeScale = 1;
        }
    }

    public void Back()
    {
        PAuseMEnu.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        canPAuse = true;
        canBAck = false;
    }

    public void Resume()
    {
        Time.timeScale = 1;
        PAuseMEnu.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        canPAuse = true;
        canBAck = false;
    }

    public void RestartGame()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(sceneNumber);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void GoToMainMenu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("Menu");
    }
}
