using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Car : MonoBehaviour
{
    #region Private Variables

    private Vector3 Destination;
    private bool vehicleAhead;

    private NavMeshAgent navvy;

    #endregion

    #region Public Variables

    //Variable to Hide
    public float LocationCount = 0;

    #endregion

    #region In-built Functions

    void Start()
    {
        InitVariables();
        InitSettings();
        GoToDestination();
    }

    void Update()
    {
        CheckDistance();
    }

    #endregion

    #region Node Functions
    #endregion

    #region Custom Functions

    void InitVariables()
    {
        navvy = GetComponent<NavMeshAgent>();

        vehicleAhead = false;
    }

    void InitSettings()
    {
        if(LocationCount == 1)
        {
            Destination = GameObject.FindGameObjectWithTag("TS").GetComponent<TrafficControl>().EndPointLeft.position;
            Debug.Log("LeftPosition Set");
        }
        else if(LocationCount == 2)
        {
            Destination = GameObject.FindGameObjectWithTag("TS").GetComponent<TrafficControl>().EndPointRight.position;
            Debug.Log("RightPosition Set");
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void GoToDestination()
    {
        navvy.SetDestination(Destination);
    }

    void CheckDistance()
    {
        if(Vector3.Distance(transform.position, Destination) < 1)
        {
            Destroy(gameObject);
        }
    }

    #endregion

    #region Collision Handler

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            navvy.isStopped = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            navvy.isStopped = false;
        }
    }
    #endregion
}
