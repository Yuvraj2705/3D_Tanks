using UnityEngine;
using UnityEngine.AI;

public class WalkNStop : MonoBehaviour
{
    #region Private Variables

    //timer 
    float uniTimer = 0;
    float uniTimeBase = 5;

    Vector3 destination;

    int counter;
    int length;
    
    //animation
    float currentAniValue = 0;

    bool canPatrol;

    NavMeshAgent navvy;
    Animator ani;

    //RootNode
    RootNode rootNode;

    //Sequences
    Sequence ai;

    //Leafs
    Leaf setDestination;
    Leaf checkDistance;
    Leaf waitTime;

    #endregion

    #region Serialize and Public Variables 

    [SerializeField] Transform[] Paths;
    [SerializeField] float blendSpeed = 2;

    public Node.Status treeStatus = Node.Status.RUNNING;

    #endregion

    #region In-Built Functions

    void Start()
    {
        initVariables();
    }

    void Update()
    {
        AnimationHandler();

        if(canPatrol)
        {
            LMAi();
        }

        if (treeStatus == Node.Status.RUNNING)
            treeStatus = rootNode.Process();
    }

    #endregion

    #region Custom Functions

    void initVariables()
    {
        navvy = GetComponent<NavMeshAgent>();
        ani = GetComponent<Animator>();
        length = Paths.Length;
        canPatrol  = true;
    }

    void LMAi()
    {
        canPatrol = false;

        treeStatus = Node.Status.RUNNING;

        rootNode = new RootNode();

        ai = new Sequence("Artificial Intelligence");

        setDestination = new Leaf("Setting Destination", SetDestination);
        checkDistance = new Leaf("Check Distance between destination and player position", CheckDistance);
        waitTime = new Leaf("Wait Time", WaitTime);

        ai.AddChild(setDestination);
        ai.AddChild(checkDistance);
        ai.AddChild(waitTime);

        rootNode.AddChild(ai);
    }

    void AnimationHandler()
    {
        if(currentAniValue == ani.GetFloat("Blend"))
        {
            return;
        }
        else
        {
            ani.SetFloat("Blend", currentAniValue , blendSpeed ,Time.deltaTime);
        }
    }

    #endregion

    #region Node Functions

    public Node.Status SetDestination()
    {
        destination = Paths[counter].position;
        navvy.SetDestination(destination);
        currentAniValue = 0.5f;
        return Node.Status.SUCCESS;
    }

    public Node.Status CheckDistance()
    {
        if(Vector3.Distance(transform.position, destination) < 0.1)
        {
            counter++;
            if(counter == length)
            {
                counter = 0;
            }
            currentAniValue = 0;
            return Node.Status.SUCCESS;
        }
        return Node.Status.RUNNING;
    }

    public Node.Status WaitTime()
    {
        uniTimer += Time.deltaTime;
        if(uniTimer > uniTimeBase)
        {
            uniTimer = 0;
            canPatrol = true;
            return Node.Status.SUCCESS;
        }
        return Node.Status.RUNNING;
    }
    #endregion
}
