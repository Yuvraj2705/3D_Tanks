using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NVSession : MonoBehaviour
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
            SceneHandler.GetComponent<SceneHandlerSceneTwo>().StartNightVisionSession = true;
            gameObject.SetActive(false);
        }
    }
}
