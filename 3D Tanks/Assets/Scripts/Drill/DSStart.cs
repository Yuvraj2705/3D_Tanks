using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DSStart : MonoBehaviour
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
            drillSessionScript.started = true;
            gameObject.SetActive(false);
        }
    }
}
