using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrafficControl : MonoBehaviour
{
    #region private Variables
    // Timer Settings
    private float timer = 0;
    private float timeBase = 2;
    private float timeinterval = 15;

    #endregion

    #region Public Variables
    [Header("Spawn Points")]
    [SerializeField] private Transform SpawnPointRight;
    [SerializeField] private Transform SpawnPointLeft;

    [Header("End Points")]
    [SerializeField] public Transform EndPointRight;
    [SerializeField] public Transform EndPointLeft;

    [Header("Car Prefab")]
    [SerializeField] private GameObject Car;

    #endregion

    #region In-Built Functions

    void Update()
    {
        CarSpawner();
    }

    #endregion

    #region Node Function
    #endregion

    #region Custom Functions

    void CarSpawner()
    {
        timer += Time.deltaTime;
        if(timer > timeBase)
        {
            SpawnCarAtLeft();
            SpawnCarAtRight();
            timeBase = timer + timeinterval;
        }
    }

    void SpawnCarAtLeft()
    {
        var Instance = Instantiate(Car, SpawnPointLeft.position, SpawnPointLeft.rotation);
        Instance.GetComponent<Car>().LocationCount = 2;
    }

    void SpawnCarAtRight()
    {
        var Instance = Instantiate(Car, SpawnPointRight.position, SpawnPointRight.rotation);
        Instance.GetComponent<Car>().LocationCount = 1;
    }

    #endregion
}
