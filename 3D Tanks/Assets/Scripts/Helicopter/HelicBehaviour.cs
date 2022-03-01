using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelicBehaviour : MonoBehaviour
{
    #region  Private Variables

    //Universal Timer
    float repeatTimer = 0;
    float waitingTime;
    Vector3 correctedRotation;
    Vector3 rot;

    RootNode rootNode;

    Sequence helicopterAi;

    Leaf towardsAbove;
    Leaf towardsHelipad;
    Leaf towardsAboveAgain;
    Leaf towardsEnd;
    Leaf death;
    Leaf timeWait;
    Leaf playerCheck;

    Leaf canStop;
    Leaf canStart;
    Leaf fullSpeed;

    #endregion

    #region  Serialized and Public Variables

    //Helicopter Settings
    [SerializeField] float speed = 10f;

    //Animations
    [SerializeField] Animator prop;
    [SerializeField] Animator rotor;

    //Time Settings
    [SerializeField] float beforeLanding = 5f;
    [SerializeField] float landingWait = 5f;
    [SerializeField] float endWait = 5f;
    [HideInInspector] public Vector3 finishPT;
    [HideInInspector] public Vector3 helipadVEC;
    [HideInInspector] public Vector3 aboveVEC;
    public Node.Status treeStatus = Node.Status.RUNNING;

    #endregion
    
    #region  In-built Functions

    void Start()
    {
        rootNode = new RootNode();

        helicopterAi = new Sequence("Helicopter AI");

        towardsAbove = new Leaf("Moving towards above point",MoveTowardsAbovePT);
        towardsHelipad = new Leaf("Moving towards helipad", TowardsHelipad);
        towardsAboveAgain = new Leaf("Moving upwards to above point", MoveAbove);
        towardsEnd = new Leaf("Moving towards end point", MoveTowardsEnd);
        death = new Leaf("Moving above points", Death);
        timeWait = new Leaf("Waiting for time", WaitTime);
        playerCheck = new Leaf("Checking Helipad", CheckHelipad);

        canStop = new Leaf("CAn Stop Anim", SlowingDown);
        canStart = new Leaf("Can Start Anim", SpeedingUp);
        fullSpeed = new Leaf("Full Speed", FullSpeed);

        helicopterAi.AddChild(towardsAbove);
        helicopterAi.AddChild(playerCheck);
        helicopterAi.AddChild(towardsHelipad);
        helicopterAi.AddChild(canStop);
        helicopterAi.AddChild(timeWait);
        helicopterAi.AddChild(canStart);
        helicopterAi.AddChild(timeWait);
        helicopterAi.AddChild(fullSpeed);
        helicopterAi.AddChild(towardsAboveAgain);
        helicopterAi.AddChild(timeWait);
        helicopterAi.AddChild(towardsEnd);
        helicopterAi.AddChild(death);

        rootNode.AddChild(helicopterAi);

        Calculations();
    }

    void Update()
    {
        if(treeStatus == Node.Status.RUNNING)
        {
            treeStatus = rootNode.Process();
        }
    }

    #endregion
    
    #region  Node Functions

    public Node.Status SlowingDown()
    {
        prop.SetBool("CanStop",true);
        return Node.Status.SUCCESS;
    }

    public Node.Status SpeedingUp()
    {
        prop.SetBool("CanStop",false);
        prop.SetBool("CanStart",true);
        waitingTime = 4;
        return Node.Status.SUCCESS;
    }

    public Node.Status FullSpeed()
    {
        prop.SetBool("CanStart",false);
        return Node.Status.SUCCESS;
    }

    public Node.Status MoveTowardsAbovePT()
    {
        transform.position = Vector3.MoveTowards(transform.position, aboveVEC, speed * Time.deltaTime );
        //transform.LookAt(new Vector3(aboveVEC.x, transform.rotation.y , transform.rotation.z) * Time.deltaTime * 5);
        if(Vector3.Distance(transform.position, aboveVEC) < 0.1f)
        {
            //prop.SetBool("IsSlowing", false);
            //rotor.SetBool("IsSlowing", false);
            waitingTime = beforeLanding;
            return Node.Status.SUCCESS;
        }
        return Node.Status.RUNNING;
    }

    public Node.Status TowardsHelipad()
    {
        transform.position = Vector3.MoveTowards(transform.position, helipadVEC, (speed/2) * Time.deltaTime);
        if(Vector3.Distance(transform.position, helipadVEC) < 0.1f)
        {
            //prop.SetBool("IsSlowing", true);
            //rotor.SetBool("IsSlowing", true);
            waitingTime = landingWait;
            return Node.Status.SUCCESS;
        }
        return Node.Status.RUNNING;
    }

    public Node.Status MoveAbove()
    {
        transform.position = Vector3.MoveTowards(transform.position, aboveVEC, (speed/2) * Time.deltaTime );
        if(Vector3.Distance(transform.position, aboveVEC) < 0.1f)
        {
            waitingTime = endWait;
            return Node.Status.SUCCESS;
        }
        return Node.Status.RUNNING;
    }

    public Node.Status MoveTowardsEnd()
    {
        transform.position = Vector3.MoveTowards(transform.position, finishPT, speed * Time.deltaTime );
        //transform.LookAt(new Vector3(transform.rotation.x ,correctedRotation.y, transform.rotation.z));
        if(Vector3.Distance(transform.position, finishPT) < 0.1f)
        {
            //prop.SetBool("IsSlowing", false);
            //rotor.SetBool("IsSlowing", false);
            return Node.Status.SUCCESS;
        }
        return Node.Status.RUNNING;
    }
    public Node.Status Death()
    {
        GameObject.FindGameObjectWithTag("HeliSpawn").GetComponent<HelicSpawner>().CanSpawn = true;
        Destroy(gameObject);
        return Node.Status.SUCCESS;
    }

    public Node.Status WaitTime()
    {
        repeatTimer += Time.deltaTime;
        if(repeatTimer > waitingTime)
        {
            repeatTimer = 0;
            return Node.Status.SUCCESS;
        }
        return Node.Status.RUNNING;
    }

    public Node.Status CheckHelipad()
    { 
        repeatTimer += Time.deltaTime;
        if(repeatTimer > waitingTime)
        {
            repeatTimer = 0;
            if(!GameObject.FindGameObjectWithTag("HeliSpawn").GetComponent<HelicSpawner>().PlayerCheck)
            {
                return Node.Status.SUCCESS;
            }
        }
        return Node.Status.RUNNING;
    }

    #endregion
    
    #region  Custom Functions

    void Calculations()
    {
        correctedRotation = new Vector3(0,finishPT.y,0) - new Vector3(0,aboveVEC.y,0);  
    }

    #endregion
}
