using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarWheels : MonoBehaviour
{
    [SerializeField] GameObject SuspensionsOne;
    [SerializeField] GameObject SuspensionsTwo;

    [SerializeField] float rotSpeed;

    [SerializeField] public bool wheelRotCheck;

    void Start()
    {
        wheelRotCheck = true;
    }

    void Update()
    {
        if(wheelRotCheck)
        {
            SuspensionsOne.transform.Rotate(new Vector3(1,0,0),5 * rotSpeed * Time.deltaTime);
            SuspensionsTwo.transform.Rotate(new Vector3(1,0,0),5 * rotSpeed * Time.deltaTime);
        }
    }
}
