using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class DrillSession : MonoBehaviour
{
    #region Private Variable

    //Timer
    float timer = 0;

    int graderOne;
    int graderTwo;
    int FinalGrade = 0;

    //enemy count
    
    //float totalEnemies = 28;

    RootNode rootNode;

    #endregion

    #region Serialize and Public Variable

    [Header("Settings")]
    public float enemyCount = 0;
    [SerializeField] KeyCode RestartKey;

    [Header("Prop Components")]
    [SerializeField] GameObject startWall;
    [SerializeField] GameObject finishWall;
    [SerializeField] GameObject endScreen;
    [SerializeField] GameObject playScreen;

    [Header("UI Components")]
    [SerializeField] TextMeshProUGUI timerText;
    [SerializeField] TextMeshProUGUI enemyText;

    [SerializeField] TextMeshProUGUI resultTime;
    [SerializeField] TextMeshProUGUI resultEH;
    [SerializeField] TextMeshProUGUI resultGrade;

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

        RestartingMech();
    }

    #endregion

    #region Node Functions

    public Node.Status CanStartDrill()
    {
        if(started)
        {
            playScreen.SetActive(true);
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
            startWall.SetActive(true);
            playScreen.SetActive(false);
            return Node.Status.SUCCESS;
        }
        return Node.Status.RUNNING;
    }

    public Node.Status GivingScore()
    {
        endScreen.SetActive(true);
        resultTime.text = timer.ToString();
        resultEH.text = enemyCount.ToString();
        //resultGrade.text = "S";
        CalculationsPlus();
        return Node.Status.SUCCESS;
    }

    #endregion

    #region Custom Functions

    void InitVariables()
    {
        started = false;
        finished = false;
        startWall.SetActive(false);
        finishWall.SetActive(true);
        endScreen.SetActive(false);
        playScreen.SetActive(false);
    }

    void RestartingMech()
    {
        if(Input.GetKeyDown(RestartKey))
        {
            SceneManager.LoadScene("ACT THREE");
        }
    }

    void CalculationsPlus()
    {
        if(timer < 60)
        {
            graderOne = 50;
        }
        else if(timer > 60 && timer < 130)
        {
            graderOne = 35;
        }
        else
        {
            graderOne = 15;
        }

        if(enemyCount >= 25)
        {
            graderTwo = 50;
        }
        else if(enemyCount < 25 && enemyCount <= 15)
        {
            graderTwo = 35;
        }
        else
        {
            graderTwo = 15;
        }

        FinalGrade = graderOne + graderTwo;

        if(FinalGrade >= 90)
        {
            resultGrade.text = "S";
        }
        else if(FinalGrade < 90 && FinalGrade >= 50)
        {
            resultGrade.text = "A";
        }
        else
        {
            resultGrade.text = "B";
        }
    }

    #endregion
}
