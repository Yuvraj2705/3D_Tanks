using UnityEngine;

public class Spawner : MonoBehaviour
{
    #region Private Variables
    //timer
    float uniTimer = 0;
    float uniTimeBase = 5;

    bool SpawnState;

    GameObject TrainHolder;

    RootNode rootNode;

    #endregion

    #region Serialize Variables and Public Variables

    [Header("Spawnpoints and EndPoints")]
    [SerializeField] Transform SpawnPoint;
    [SerializeField] Transform EndPoint;

    [Header("Train Prefab")]
    [SerializeField] GameObject Train;

    [Header("Blocker Prefab")]
    [SerializeField] Animator[] Blockers;

    [Header("Spawning Settings")]
    [SerializeField] float SpawnCoolDownABD = 5;
    [SerializeField] float SpawningCoolDown = 10;

    [Header("Tree Status Debug")]
    public Node.Status treeStatus = Node.Status.RUNNING;

    #endregion

    #region In-Built Functions

    void Start()
    {
        SpawnState = true;
        uniTimeBase = SpawnCoolDownABD;
    }

    void Update()
    {
        if(SpawnState)
        {
            RailwaySystemBehavior();
            SpawnState = false;
        }

        if (treeStatus == Node.Status.RUNNING)
            treeStatus = rootNode.Process();
    }

    #endregion

    #region Custom Functions

    void RailwaySystemBehavior()
    {
        treeStatus = Node.Status.RUNNING;

        rootNode = new RootNode();

        Sequence railwaySystemAI = new Sequence("Railway System AI");

        Leaf blockersDown = new Leaf("Blockers Down", BlockersDown);
        Leaf waitToSpawn = new Leaf("Wait for Spawn", WaitToSpawn);
        Leaf spawnTrain = new Leaf("Spawning Train", SpawnTrain);
        Leaf blockerUpMechanism = new Leaf("Checking Train so that we can Put The Blocker Up", BlockersUpAi);
        Leaf spawnCoolDown = new Leaf("Spawn Cool Down", SpawnCoolDown);

        railwaySystemAI.AddChild(blockersDown);
        railwaySystemAI.AddChild(waitToSpawn);
        railwaySystemAI.AddChild(spawnTrain);
        railwaySystemAI.AddChild(blockerUpMechanism);
        railwaySystemAI.AddChild(spawnCoolDown);

        rootNode.AddChild(railwaySystemAI);
    }

    #endregion

    #region Node Functions

    public Node.Status BlockersDown()
    {
        foreach(var blocker in Blockers)
        {
            blocker.SetInteger("UAD", 1);
        }
        return Node.Status.SUCCESS;
    }

    public Node.Status WaitToSpawn()
    {
        uniTimer += Time.deltaTime;
        if(uniTimer > uniTimeBase)
        {
            uniTimer = 0;
            return Node.Status.SUCCESS;
        } 
        return Node.Status.RUNNING;
    }

    public Node.Status SpawnTrain()
    {
        uniTimeBase = 5;
        var Instance = Instantiate(Train, SpawnPoint.position, Quaternion.identity);
        Instance.GetComponent<Train>().Destination = EndPoint.position;
        return Node.Status.SUCCESS;
    }

    public Node.Status BlockersUpAi()
    {
        uniTimer += Time.deltaTime;
        if(uniTimer > uniTimeBase)
        {
            TrainHolder = GameObject.FindGameObjectWithTag("Train");
            if(TrainHolder != null)
            {
                uniTimer = 0;
                return Node.Status.RUNNING;
            }
            else
            {
                uniTimer = 0;
                uniTimeBase = SpawningCoolDown;
                foreach(var blocker in Blockers)
                {
                    blocker.SetInteger("UAD", 2);
                }
                return Node.Status.SUCCESS;
            }
        }
        return Node.Status.RUNNING;
    }

    public Node.Status SpawnCoolDown()
    {
        uniTimer += Time.deltaTime;
        if(uniTimer > uniTimeBase)
        {
            uniTimer = 0;
            uniTimeBase = SpawnCoolDownABD;
            SpawnState = true;
            return Node.Status.SUCCESS;
        }
        return Node.Status.RUNNING;
    }
    #endregion
}
