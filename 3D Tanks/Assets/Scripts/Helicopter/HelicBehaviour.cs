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

    #endregion

    #region  Serialized and Public Variables

    //Helicopter Settings
    [SerializeField] float speed = 10f;

    //Animations
    [SerializeField] Animator prop;
    [SerializeField] Animator rotor;

    //Sound Effects
    [SerializeField] AudioSource flying;
    [SerializeField] AudioSource slowing;

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

        Sequence helicopterAi = new Sequence("Helicopter AI");

        prop = GetComponentInChildren<Animator>();
        rotor = GetComponentInChildren<Animator>();

        flying = GetComponent<AudioSource>();
        slowing = GetComponent<AudioSource>();

        Leaf towardsAbove = new Leaf("Moving towards above point",MoveTowardsAbovePT);
        Leaf towardsHelipad = new Leaf("Moving towards helipad", TowardsHelipad);
        Leaf towardsAboveAgain = new Leaf("Moving upwards to above point", MoveAbove);
        Leaf towardsEnd = new Leaf("Moving towards end point", MoveTowardsEnd);
        Leaf death = new Leaf("Moving above points", Death);
        Leaf timeWait = new Leaf("Waiting for time", WaitTime);
        Leaf playerCheck = new Leaf("Checking Helipad", CheckHelipad);

        helicopterAi.AddChild(towardsAbove);
        helicopterAi.AddChild(playerCheck);
        helicopterAi.AddChild(towardsHelipad);
        helicopterAi.AddChild(timeWait);
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

    public Node.Status MoveTowardsAbovePT()
    {
        transform.position = Vector3.MoveTowards(transform.position, aboveVEC, speed * Time.deltaTime );
        //transform.LookAt(new Vector3(aboveVEC.x, transform.rotation.y , transform.rotation.z) * Time.deltaTime * 5);
        if(Vector3.Distance(transform.position, aboveVEC) < 0.1f)
        {
            prop.SetFloat("VelX", 0);
            rotor.SetFloat("VelX", 0);
            flying.Play();
            slowing.Stop();
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
            prop.SetFloat("VelX", 1);
            rotor.SetFloat("VelX", 1);
            flying.Stop();
            slowing.Play();
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
            prop.SetFloat("VelX", 0);
            rotor.SetFloat("VelX", 0);
            flying.Play();
            slowing.Stop();
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
            prop.SetFloat("VelX", 0);
            rotor.SetFloat("VelX", 0);
            flying.Play();
            slowing.Stop();
            return Node.Status.SUCCESS;
        }
        return Node.Status.RUNNING;
    }
    public Node.Status Death()
    {
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
