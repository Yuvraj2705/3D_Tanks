using UnityEngine;

public class TSSpawner : MonoBehaviour
{
    #region Private Variables

    //timer
    float uniTimer = 0;
    float uniTimeBase = 5;

    bool CanSpawn;

    GameObject[] carCounter;

    RootNode rootNode;

    public Node.Status treeStatus = Node.Status.RUNNING;

    #endregion

    #region Public and Serialize Variables

    //Spawn Pont and End Point
    [SerializeField] Transform Spawn;
    [SerializeField] Transform End;

    //Spawn time
    [SerializeField] float minTime = 15;
    [SerializeField] float maxTime = 30;

    //Car Prefab
    [SerializeField] GameObject[] Cars;

    //Total Cars To Spawn
    static int TotalCars = 4;

    #endregion

    #region In-Built Functions

    void Start()
    {
        InitVariables();
    }

    void Update()
    {
        if(CanSpawn)
        {
            TrafficSystem();
        }

        if (treeStatus == Node.Status.RUNNING)
            treeStatus = rootNode.Process();
    }

    #endregion

    #region Custom Functions

    void InitVariables()
    {
        CanSpawn = true;
    }

    void TrafficSystem()
    {
        CanSpawn = false;

        treeStatus = Node.Status.RUNNING;

        rootNode = new RootNode();

        Sequence trafficAi = new Sequence("traffic System Ai");

        Leaf checkCarCount = new Leaf("Checking no of Cars", CheckCarCount);
        Leaf setSpawnTime = new Leaf("Setting Spawn Time", SetSpawnTime);
        Leaf spawnTime = new Leaf("Spawn Time", SpawnTime);
        Leaf instantiateCar = new Leaf("Instantiate the Car", InstantiateCar);

        trafficAi.AddChild(checkCarCount);
        trafficAi.AddChild(setSpawnTime);
        trafficAi.AddChild(spawnTime);
        trafficAi.AddChild(instantiateCar);

        rootNode.AddChild(trafficAi);
    }

    #endregion

    #region Node Functions

    public Node.Status CheckCarCount()
    {
        uniTimer += Time.deltaTime;
        if(uniTimer > uniTimeBase)
        {
            uniTimer = 0;
            carCounter = GameObject.FindGameObjectsWithTag("Car");
            if(carCounter.Length <= TotalCars - 1)
            {
                return Node.Status.SUCCESS;
            }
        }
        return Node.Status.RUNNING;
    }

    public Node.Status SetSpawnTime()
    {
        uniTimeBase = Random.Range(minTime,maxTime);
        return Node.Status.SUCCESS;
    }

    public Node.Status SpawnTime()
    {
        uniTimer += Time.deltaTime;
        if(uniTimer > uniTimeBase)
        {
            uniTimeBase = 5;
            return Node.Status.SUCCESS;
        }
        return Node.Status.RUNNING;
    }

    public Node.Status InstantiateCar()
    {
        var Instance = Instantiate(Cars[Random.Range(0,Cars.Length)], Spawn.position, Spawn.rotation);
        Instance.GetComponent<Car>().Destination = End.position;
        CanSpawn = true;
        return Node.Status.SUCCESS;
    }

    #endregion
}
