using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DSFinish : MonoBehaviour
{
    [SerializeField] GameObject DrillSession;
    DrillSession drillSessionScript;

    void Start()
    {
        drillSessionScript = DrillSession.GetComponent<DrillSession>(); 
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            drillSessionScript.finished = true;
            gameObject.SetActive(false);
        }
    }
}
