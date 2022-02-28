using System.Reflection;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelicSpawner : MonoBehaviour
{
    #region Private Variables

    //timer
    float timer = 0;
    float timeBase = 5;

    RootNode rootNode;
    
    #endregion

    #region Serialize and Public Variables

    [HideInInspector]public bool PlayerCheck;
    [HideInInspector]public bool CanSpawn;

    //Prefabs
    [SerializeField] GameObject helicopter;

    //Spawn and End Points
    [SerializeField] Transform spawnPT;
    [SerializeField] Transform helipadPT;
    [SerializeField] Transform abovePT;
    [SerializeField] Transform endPT;

    [SerializeField] float WaitTime = 420;
    
    public Node.Status treeStatus = Node.Status.RUNNING;

    #endregion

    #region In-built Functions

    void Start()
    {
        CanSpawn = true;
    }

    void Update()
    {

        if(CanSpawn)
        {
            SpawningAi();
        }


        if (treeStatus == Node.Status.RUNNING)
        { 
            treeStatus = rootNode.Process();
        }
    }

    #endregion

    #region Node Functions
    public Node.Status SpawnHelicopter()
    {
        var Instance = Instantiate(helicopter, spawnPT.position, spawnPT.rotation);
        Instance.GetComponent<HelicBehaviour>().aboveVEC = abovePT.position;
        Instance.GetComponent<HelicBehaviour>().helipadVEC = helipadPT.position;
        Instance.GetComponent<HelicBehaviour>().finishPT = endPT.position;
        timeBase = WaitTime;
        return Node.Status.SUCCESS;
    }

    public Node.Status WaitFor()
    {
        timer += Time.deltaTime;
        if(timer > timeBase)
        {
            timer = 0;
            return Node.Status.SUCCESS;
        }
        return Node.Status.RUNNING;
    }
    #endregion

    #region Custom Functions

    void SpawningAi()
    {
        CanSpawn = false;

        treeStatus = Node.Status.RUNNING;

        rootNode = new RootNode();

        Sequence spawner = new Sequence("Helicopter Spawner");

        Leaf waitFor = new Leaf("Waiting Time", WaitFor);
        Leaf heliSpawn = new Leaf("Spawning Helicopter", SpawnHelicopter);
        
        spawner.AddChild(waitFor);
        spawner.AddChild(heliSpawn);

        rootNode.AddChild(spawner);
    }

    #endregion
    
    #region Collisions
    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            PlayerCheck = true;
        }
    }
    void OnTriggerExit(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            PlayerCheck = false;
        }
    }

    #endregion
}
