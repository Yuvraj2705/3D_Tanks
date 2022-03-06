using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SPlashScreen : MonoBehaviour
{
    [SerializeField] GameObject ButtonsUI;
    [SerializeField] GameObject AnyKeyUi;

    void Start()
    {
        ButtonsUI.SetActive(false);
        AnyKeyUi.SetActive(true);
    }

    void Update()
    {
        if(Input.anyKeyDown)
        {
            ButtonsUI.SetActive(true);
            AnyKeyUi.SetActive(false);
            gameObject.SetActive(false);
        }
    }
}
