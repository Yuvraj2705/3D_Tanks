using UnityEngine;
using TMPro;

public class DrillSession : MonoBehaviour
{
    #region Private Variable

    //Timer
    float timer = 0;

    //enemy count
    public float enemyCount = 0;
    //float totalEnemies = 28;

    RootNode rootNode;

    #endregion

    #region Serialize and Public Variable

    [Header("Prop Components")]
    [SerializeField] GameObject finishWall;

    [Header("UI Components")]
    [SerializeField] TextMeshProUGUI timerText;
    [SerializeField] TextMeshProUGUI enemyText;

    //Bool
    [HideInInspector] public bool started;
    [HideInInspector] public bool finished;

    [Header("Tree Status")]
    public Node.Status treeStatus = Node.Status.RUNNING;

    #endregion

    #region In-Built Functions

    void Start()
    {
        InitVariables();

        rootNode = new RootNode();

        Sequence drillHandler = new Sequence("Handling Drill");

        Leaf canStartDrill = new Leaf("Starting Drill Mech", CanStartDrill);
        Leaf collectingData = new Leaf("Collecting Data", CollectingData);
        Leaf givingScores = new Leaf("Giving Scores", GivingScore);

        drillHandler.AddChild(canStartDrill);
        drillHandler.AddChild(collectingData);
        drillHandler.AddChild(givingScores);

        rootNode.AddChild(drillHandler);
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

    public Node.Status CanStartDrill()
    {
        if(started)
        {
            return Node.Status.SUCCESS;
        }
        return Node.Status.RUNNING;
    }

    public Node.Status CollectingData()
    {
        //calculations
        timer += Time.deltaTime;

        //UI Calculations
        timerText.text = timer.ToString();
        enemyText.text = enemyCount.ToString();

        if(finished)
        {
            finishWall.SetActive(false);
            return Node.Status.SUCCESS;
        }
        return Node.Status.RUNNING;
    }

    public Node.Status GivingScore()
    {
        Debug.Log(timer);
        Debug.Log(enemyCount);
        return Node.Status.SUCCESS;
    }

    #endregion

    #region Custom Functions

    void InitVariables()
    {
        started = false;
        finished = false;
        finishWall.SetActive(true);
    }

    #endregion
}
