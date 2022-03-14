using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class SceneHandlerSceneTwo : MonoBehaviour
{
    [SerializeField]
    GameObject TextUI;

    [SerializeField]
    TextMeshProUGUI DisplayTextUI;

    private float timer =  0;
    private float timebase = 2;

    [HideInInspector] public bool StartCrouchingSession;
    [HideInInspector] public bool StartJumpingSession;
    [HideInInspector] public bool StartNightVisionSession;
    [HideInInspector] public bool StartGunSession;

    RootNode rootNode;

    public Node.Status treeStatus = Node.Status.RUNNING;

    void Start()
    {
        StartCrouchingSession = false;
        StartJumpingSession = false;
        StartNightVisionSession = false;

        TextUI.SetActive(false);

        rootNode = new RootNode();

        Sequence sh = new Sequence("SH");

        Leaf waitFor = new Leaf("Wait For", WaitFor);
        Leaf activateUI = new Leaf("Activate Ui", ActivateUI);
        Leaf disableUI = new Leaf("Activate Ui", DisableUI);
        Leaf textOne = new Leaf("text One", TextOne);
        Leaf textTwo = new Leaf("text One", TextTwo);
        Leaf textThree = new Leaf("text One", TextThree);
        Leaf textFour = new Leaf("text One", TextFour);
        Leaf textFive = new Leaf("text One", TextFive);
        Leaf textSix = new Leaf("text One", TextSix);

        Leaf crouchingSession = new Leaf("Crouch", CrouchSession);
        Leaf jumpingSession = new Leaf("Crouch", JumpSession);
        Leaf nvSession = new Leaf("NV", NVSession);
        Leaf gunSession = new Leaf("NV", GunSession);

        sh.AddChild(waitFor);
        sh.AddChild(activateUI);
        sh.AddChild(textOne);
        sh.AddChild(crouchingSession);
        sh.AddChild(textTwo);
        sh.AddChild(jumpingSession);
        sh.AddChild(textThree);
        sh.AddChild(nvSession);
        sh.AddChild(textFour);
        sh.AddChild(gunSession);
        sh.AddChild(textFive);
        sh.AddChild(waitFor);
        sh.AddChild(textSix);
        sh.AddChild(waitFor);
        //sh.AddChild(waitFor);
        sh.AddChild(disableUI);

        rootNode.AddChild(sh);
    }

    public Node.Status CrouchSession()
    {
        if(StartCrouchingSession)
        {
            return Node.Status.SUCCESS;
        }
        return Node.Status.RUNNING;
    }

    public Node.Status JumpSession()
    {
        if(StartJumpingSession)
        {
            return Node.Status.SUCCESS;
        }
        return Node.Status.RUNNING;
    }

    public Node.Status NVSession()
    {
        if(StartNightVisionSession)
        {
            return Node.Status.SUCCESS;
        }
        return Node.Status.RUNNING;
    }

    public Node.Status GunSession()
    {
        if(StartGunSession)
        {
            return Node.Status.SUCCESS;
        }
        return Node.Status.RUNNING;
    }

    public Node.Status WaitFor()
    {
        timer += Time.deltaTime;
        if(timer > timebase)
        {
            timer = 0;
            return Node.Status.SUCCESS;
        }
        return Node.Status.RUNNING;
    }

    public Node.Status ActivateUI()
    {
        TextUI.SetActive(true);
        timebase = 5;
        return Node.Status.SUCCESS;
    }

    public Node.Status DisableUI()
    {
        TextUI.SetActive(false);
        return Node.Status.SUCCESS;
    }

    public Node.Status TextOne()
    {
        DisplayTextUI.text = "Welcome to your first Training Session, use W A S D to move around and LEFT SHIFT to sprint.";
        return Node.Status.SUCCESS;
    }

    public Node.Status TextTwo()
    {
        DisplayTextUI.text = "To clear the first obstacle you need to crouch under the tires, Press LEFT CTRL to crouch.";
        return Node.Status.SUCCESS;
    }

    public Node.Status TextThree()
    {
        DisplayTextUI.text = "To clear the second obstacle you need to jump over the obstacles, Press SPACE to jump.";
        return Node.Status.SUCCESS;
    }

    public Node.Status TextFour()
    {
        DisplayTextUI.text = "OOPS! Lights are out, Press N to toggle the Night Vision Googles on and off.";
        return Node.Status.SUCCESS;
    }

    public Node.Status TextFive()
    {
        DisplayTextUI.text = "To use the Tablet, Press E to interact. Using tablet you can get access to various trainings.";
        return Node.Status.SUCCESS;
    }

    public Node.Status TextSix()
    {
        DisplayTextUI.text = "To pick up the gun press E and Q to drop, Press Left Mouse Button to Shoot and R to Reload, B to toggle between Modes";
        return Node.Status.SUCCESS;
    }

    void Update()
    {
        if (treeStatus == Node.Status.RUNNING)
            treeStatus = rootNode.Process();
    }
}
