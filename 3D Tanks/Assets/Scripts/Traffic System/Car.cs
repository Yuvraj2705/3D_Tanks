using UnityEngine;
using UnityEngine.AI;

public class Car : MonoBehaviour
{
    #region Private Variables

    //Player Collision Timer
    float pTimer = 0;
    float pTimeBase = 1;

    NavMeshAgent navvy;

    RootNode rootNode;

    #endregion

    #region Public and Serialize Variables

    [HideInInspector] public Vector3 Destination;

    public Node.Status treeStatus = Node.Status.RUNNING;

    #endregion

    #region In-Built Functions

    void Start()
    {
        InitVariables();

        rootNode = new RootNode();

        Sequence carAi = new Sequence("Car Ai");

        Leaf setDestination = new Leaf("Setting Destination", SetDestination);
        Leaf checkDistance = new Leaf("Checking Distance", CheckDistance);
        Leaf destroyCar = new Leaf("Destroying the Car", DestroyCar);

        carAi.AddChild(setDestination);
        carAi.AddChild(checkDistance);
        carAi.AddChild(destroyCar);

        rootNode.AddChild(carAi);
    }

    void Update()
    {

        if (treeStatus == Node.Status.RUNNING)
            treeStatus = rootNode.Process();
    }

    #endregion

    #region Custom Functions

    void InitVariables()
    {
        
        navvy = GetComponent<NavMeshAgent>();
        navvy.isStopped = false;
    }

    #endregion

    #region Node Functions

    public Node.Status SetDestination()
    {
        navvy.SetDestination(Destination);
        return Node.Status.SUCCESS;
    }

    public Node.Status CheckDistance()
    {
        if(Vector3.Distance(transform.position, Destination) < 5)
        {
            return Node.Status.SUCCESS;
        }
        return Node.Status.RUNNING;
    }

    public Node.Status DestroyCar()
    {
        Destroy(gameObject);
        return Node.Status.SUCCESS;
    }
    #endregion

    #region Collisions

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            if(navvy.isStopped)
            {
                return;
            }
            navvy.isStopped = true;
        }

        if(other.gameObject.tag == "Car")
        {
            if(navvy.isStopped)
            {
                return;
            }
            navvy.isStopped = true;
        }
    }

    void OnTriggerStay(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            pTimer += Time.deltaTime;
            if(pTimer > pTimeBase)
            {
                if(navvy.isStopped)
                {
                    return;
                }
                navvy.isStopped = true;
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            if(!navvy.isStopped)
            {
                return;
            }
            navvy.isStopped = false;
        }

        if(other.gameObject.tag == "Car")
        {
            if(!navvy.isStopped)
            {
                return;
            }
            navvy.isStopped = false;
        }
    }
    #endregion
}