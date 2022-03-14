using UnityEngine;
using UnityEngine.SceneManagement;

public class NoticeBoard : MonoBehaviour
{
    [SerializeField]
    GameObject InteractUI;

    [SerializeField]
    GameObject MissionListUI;

    bool CanInteract;

    void Start()
    {
        CanInteract = false;
        InteractUI.SetActive(false);
        MissionListUI.SetActive(false);
    }

    void Update()
    {
        if(CanInteract && Input.GetKeyDown(KeyCode.E))
        {
            Cursor.lockState = CursorLockMode.None;
            MissionListUI.SetActive(true);
            CanInteract = false;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            InteractUI.SetActive(true);
            CanInteract = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            InteractUI.SetActive(false);
            CanInteract = false;
        }
    }

    public void Back()
    {
        Cursor.lockState = CursorLockMode.Locked;
        MissionListUI.SetActive(false);
        CanInteract = true;
    }

    public void Training()
    {
        SceneManager.LoadScene("Act Two");
    }

    public void Drill()
    {
        SceneManager.LoadScene("ACT THREE");
    }
}
