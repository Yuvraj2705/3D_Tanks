using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrouchSession : MonoBehaviour
{
    [SerializeField] GameObject SceneHandler;

    SceneHandlerSceneTwo sh;

    void Start()
    {
        sh = SceneHandler.GetComponent<SceneHandlerSceneTwo>();
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            SceneHandler.GetComponent<SceneHandlerSceneTwo>().StartCrouchingSession = true;
            gameObject.SetActive(false);
        }
    }
}
