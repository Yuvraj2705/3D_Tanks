using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SceneHandlerAO : MonoBehaviour
{
    [SerializeField]
    GameObject TextUI;

    [SerializeField]
    TextMeshProUGUI DisplayTextUI;

    private float timer =  0;
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
        Leaf textTwo = new Leaf("text One", TextTwo);
        Leaf textThree = new Leaf("text One", TextThree);

        sh.AddChild(waitFor);
        sh.AddChild(activateUI);
        sh.AddChild(textOne);
        sh.AddChild(waitFor);
        sh.AddChild(textTwo);
        sh.AddChild(waitFor);
        sh.AddChild(textThree);
        sh.AddChild(waitFor);
        sh.AddChild(disableUI);

        rootNode.AddChild(sh);
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
        return Node.Status.SUCCESS;
    }

    public Node.Status DisableUI()
    {
        TextUI.SetActive(false);
        return Node.Status.SUCCESS;
    }

    public Node.Status TextOne()
    {
        DisplayTextUI.text = "CASERN, a Military Base, welcomes you to play our campaign. Please visit our notice board to access the campaign.";
        return Node.Status.SUCCESS;
    }

    public Node.Status TextTwo()
    {
        DisplayTextUI.text = "To move around the base use W, A, S, D keys, to look around use Mouse and hold LeftShift to run.";
        return Node.Status.SUCCESS;
    }

    public Node.Status TextThree()
    {
        DisplayTextUI.text = "Press Tab to enable or disable the Minimap, Press M to enable or disable the Music Player.";
        return Node.Status.SUCCESS;
    }

    void Update()
    {
        if (treeStatus == Node.Status.RUNNING)
            treeStatus = rootNode.Process();
    }
}
