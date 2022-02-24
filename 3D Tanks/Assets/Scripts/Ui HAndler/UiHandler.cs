using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UiHandler : MonoBehaviour
{
    [SerializeField] GameObject Minimap;
    bool minimapCheck;

    void Start()
    {
        minimapCheck = true;
        Minimap.SetActive(false);
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Tab))
        {
            if(minimapCheck)
            {
                Minimap.SetActive(true);
                minimapCheck = false;
            }
            else
            {
                Minimap.SetActive(false);
                minimapCheck = true;
            }
        }
    }
}
