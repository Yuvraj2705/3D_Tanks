using UnityEngine;

public class ShootingSessions : MonoBehaviour
{
    #region Private Variables

    float timer = 0;
    float timeBase = 5;

    RootNode rootNode;

    #endregion

    #region Public and Serialize Variables

    [Header("Settings")]
    [SerializeField]
    float TimeBeforeComeDown = 5;

    [Header("Componenets")]
    [SerializeField]
    Animator[] Targets; 

    [HideInInspector] 
    public bool canGoDown;

    [HideInInspector]
    public bool canStart;

    [HideInInspector]
    public bool limiter;

    [SerializeField]
    GameObject ZoneSwitch;

    Zone zoneScript;

    [Header("Tree Status")]
    public Node.Status treeStatus = Node.Status.RUNNING;

    #endregion

    #region In-Built Functions

    void Start()
    {
        zoneScript = ZoneSwitch.GetComponent<Zone>();
        canGoDown = false;
        canStart = false;
        limiter = true;
        timeBase = TimeBeforeComeDown;
    }

    void Update()
    {
        if(limiter)
        {
            TargetAi();
        }

        if (treeStatus == Node.Status.RUNNING)
        {
            treeStatus = rootNode.Process();
        }

    }

    #endregion

    #region Node Functions

    public Node.Status CanStartTheLoop()
    {
        timer += Time.deltaTime;
        if(timer > 1)
        {
            if(canStart)
            {
                timer = 0;
                zoneScript.canChange = false;
                canStart = false;
                return Node.Status.SUCCESS;
            }
            return Node.Status.RUNNING;
        }
        return Node.Status.RUNNING;
    }

    public Node.Status WaitTime()
    {
        timer += Time.deltaTime;
        if(timer > timeBase)
        {
            timer = 0;
            return Node.Status.SUCCESS;
        }
        return Node.Status.RUNNING;
    }

    public Node.Status TargetsGoUp()
    {
        foreach(var target in Targets)
        {
            target.SetBool("GoUp",true);
            target.SetBool("GoDown",false);
        }
        return Node.Status.SUCCESS;
    }

    public Node.Status CanGoDown()
    {
        if(canGoDown)
        {
            foreach(var target in Targets)
            {
                target.GetComponent<TargetD>().currentHits = target.GetComponent<TargetD>().MaxHits;
                target.SetBool("GoUp",false);
                target.SetBool("GoDown",true);
                zoneScript.canChange = true;
            }
            canGoDown = false;
            limiter = true;
            return Node.Status.SUCCESS;
        }
        return Node.Status.RUNNING;
    }

    #endregion

    #region Custom Functions

    public void Settings()
    {
        canGoDown = false;
    }

    public void TargetAi()
    {
        //Debug.Log("Playing");

        limiter = false;

        treeStatus = Node.Status.RUNNING;

        rootNode = new RootNode();

        Sequence ai = new Sequence("Ai");

        Leaf waitTime = new Leaf("wait Time", WaitTime);
        Leaf targetGoUp = new Leaf("TargetGoingUp", TargetsGoUp);
        Leaf canGoDown = new Leaf("Can Go Down", CanGoDown);
        Leaf canStartSession = new Leaf("Can Start Level", CanStartTheLoop);

        ai.AddChild(canStartSession);
        ai.AddChild(waitTime);
        ai.AddChild(targetGoUp);
        ai.AddChild(canGoDown);

        rootNode.AddChild(ai);
    }
    #endregion
}
