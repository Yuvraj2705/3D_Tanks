using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SceneHandlerAT : MonoBehaviour
{
    [SerializeField]
    GameObject TextUI;

    [SerializeField]
    TextMeshProUGUI DisplayTextUI;

    [SerializeField]
    AudioSource popAlert;

    private float timer = 0;
    private float timebase = 5;

    RootNode rootNode;

    public Node.Status treeStatus = Node.Status.RUNNING;

    void Start()
    {
        TextUI.SetActive(false);

        rootNode = new RootNode();

        Sequence sh = new Sequence("SH");

        Leaf waitFor = new Leaf("Wait For", WaitFor);
        Leaf activateUI = new Leaf("Activate Ui", ActivateUI);
        Leaf disableUI = new Leaf("Activate Ui", DisableUI);
        Leaf textOne = new Leaf("text One", TextOne);
        Leaf textTwo = new Leaf("text Two", TextTwo);
        Leaf textThree = new Leaf("text Three", TextThree);
        Leaf textFour = new Leaf("text Four", TextFour);

        sh.AddChild(waitFor);
        sh.AddChild(activateUI);
        sh.AddChild(textOne);
        sh.AddChild(waitFor);
        sh.AddChild(textTwo);
        sh.AddChild(waitFor);
        sh.AddChild(textThree);
        sh.AddChild(waitFor);
        sh.AddChild(textFour);
        sh.AddChild(waitFor);
        sh.AddChild(disableUI);

        rootNode.AddChild(sh);
    }

    public Node.Status WaitFor()
    {
        timer += Time.deltaTime;
        if (timer > timebase)
        {
            timer = 0;
            return Node.Status.SUCCESS;
        }
        return Node.Status.RUNNING;
    }

    public Node.Status ActivateUI()
    {
        TextUI.SetActive(true);
        return Node.Status.SUCCESS;
    }

    public Node.Status DisableUI()
    {
        TextUI.SetActive(false);
        return Node.Status.SUCCESS;
    }

    public Node.Status TextOne()
    {
        DisplayTextUI.text = "Welcome to your first Drill Session, go to the near table and pick-up any weapon.";
        popAlert.Play();
        return Node.Status.SUCCESS;
    }

    public Node.Status TextTwo()
    {
        DisplayTextUI.text = "There are three types of target: idle, moving and instant. Shoot them down and get the perfect score.";
        popAlert.Play();
        return Node.Status.SUCCESS;
    }

    public Node.Status TextThree()
    {
        DisplayTextUI.text = "Go to the Start and follow the arrows.";
        popAlert.Play();
        return Node.Status.SUCCESS;
    }
    public Node.Status TextFour()
    {
        DisplayTextUI.text = "Once completed, you can press K to restart the session.";
        popAlert.Play();
        return Node.Status.SUCCESS;
    }

    void Update()
    {
        if (treeStatus == Node.Status.RUNNING)
            treeStatus = rootNode.Process();
    }
}
