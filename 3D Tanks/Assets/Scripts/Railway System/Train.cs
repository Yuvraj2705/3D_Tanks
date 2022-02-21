using UnityEngine;
using UnityEngine.AI;

public class Train : MonoBehaviour
{
    #region Private Variables

    float DistanceToCover;
    float StoppingDistance;
    float maxSpeed = 100;
    int NoOfGoods;
    int counter = 0;

    //timer
    float timer = 0;
    float timeBase;
    

    NavMeshAgent navvy;

    RootNode rootNode;

    #endregion

    #region Public Variables and Serialize fields

    [Header("Train Movement Settings")]
    [HideInInspector] public Vector3 Destination;
    [SerializeField] float SpeedTime = 1f;
    [SerializeField] float StoppingTime = 0.5f;
    [SerializeField] float StartUnloadingAt = 3;
    [SerializeField] float UnloadTime = 5;
    [SerializeField] float TimeToWaitAfterUnloading = 5;

    [Header("Train Goods Section")]
    [SerializeField] GameObject[] goodsOne;
    [SerializeField] GameObject[] goodsTwo;

    [Header("Tree Status Debug")]
    public Node.Status treeStatus = Node.Status.RUNNING;

    #endregion

    #region In-Built Functions

    void Start()
    {
        InitVariables();

        rootNode = new RootNode();

        Sequence trainAi = new Sequence("Train Ai System");
        Leaf doCalculations = new Leaf("Calcis", Calculations);
        Leaf setDestination = new Leaf("Setting destination", SetDestination);
        Leaf stopping = new Leaf("Activating the Stopping Mechanism", StoppingMechanism);
        Leaf unloading = new Leaf("Unloading", UnloadingMech);
        Leaf waitAfterUnLoading = new Leaf("Wait", WaitAfterUnloading);
        Leaf trainAcc = new Leaf("Accelarating", TrainAcc);
        Leaf despawnTrain = new Leaf("Despawning Train", DespawnTrain);

        trainAi.AddChild(doCalculations);
        trainAi.AddChild(setDestination);
        trainAi.AddChild(stopping);
        trainAi.AddChild(unloading);
        trainAi.AddChild(waitAfterUnLoading);
        trainAi.AddChild(trainAcc);
        trainAi.AddChild(despawnTrain);

        rootNode.AddChild(trainAi);
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
        timeBase = StartUnloadingAt;
    }

    #endregion

    #region Node Functions

    public Node.Status Calculations()
    {
        DistanceToCover = Vector3.Distance(transform.position, Destination);
        StoppingDistance = DistanceToCover - (DistanceToCover/3.3f);
        NoOfGoods = goodsOne.Length;
        return Node.Status.SUCCESS;
    }

    public Node.Status SetDestination()
    {
        navvy.SetDestination(Destination);
        return Node.Status.SUCCESS;
    }

    public Node.Status StoppingMechanism()
    {
        if(Vector3.Distance(transform.position, Destination) < StoppingDistance)
        {
            navvy.speed = Mathf.Lerp(navvy.speed, 0, StoppingTime * Time.deltaTime);
            if(navvy.speed < 2)
            {
                navvy.isStopped = true;
                navvy.speed = 0;
                return Node.Status.SUCCESS;
            }
        }
        return Node.Status.RUNNING;
    }

    public Node.Status UnloadingMech()
    {
        timer += Time.deltaTime;
        if(timer > timeBase)
        {
            Destroy(goodsOne[counter].gameObject);
            Destroy(goodsTwo[counter].gameObject);
            counter++;
            timeBase = timer + UnloadTime;
        }
        if(counter == NoOfGoods)
        {
            timer = 0;
            timeBase = TimeToWaitAfterUnloading;
            return Node.Status.SUCCESS;
        }
        return Node.Status.RUNNING;
    }

    public Node.Status WaitAfterUnloading()
    {
        timer += Time.deltaTime;
        if(timer > timeBase)
        {
            navvy.isStopped = false;
            navvy.speed = 0;
            return Node.Status.SUCCESS;
        }
        return Node.Status.RUNNING;
    }

    public Node.Status TrainAcc()
    {
        navvy.speed = Mathf.Lerp(navvy.speed, maxSpeed, SpeedTime * Time.deltaTime);
        if(navvy.speed > maxSpeed - 2)
        {
            navvy.speed = maxSpeed;
            return Node.Status.SUCCESS;
        }
        return Node.Status.RUNNING;
    }

    public Node.Status DespawnTrain()
    {
        if(Vector3.Distance(transform.position, Destination) <= 10)
        {
            Destroy(gameObject);
        }
        return Node.Status.RUNNING;
    }

    #endregion   
}