using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelicSpawner : MonoBehaviour
{
    #region Private Variables

    RootNode rootNode;
    
    #endregion

    #region Serialize and Public Variables

    [HideInInspector]public bool PlayerCheck;

    //Prefabs
    [SerializeField] GameObject helicopter;

    //Spawn and End Points
    [SerializeField] Transform spawnPT;
    [SerializeField] Transform helipadPT;
    [SerializeField] Transform abovePT;
    [SerializeField] Transform endPT;
    
    public Node.Status treeStatus = Node.Status.RUNNING;

    #endregion

    #region In-built Functions

    void Start()
    {
        rootNode = new RootNode();

        Sequence spawner = new Sequence("Helicopter Spawner");

        Leaf heliSpawn = new Leaf("Spawning Helicopter", SpawnHelicopter);

        spawner.AddChild(heliSpawn);

        rootNode.AddChild(spawner);
    }

    void Update()
    {
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
        return Node.Status.SUCCESS;
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
