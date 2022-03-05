using UnityEngine;
using TMPro;

public class ZoneImplementer : SelectionManager
{
    #region Serialize and Public Variables

    [Header("Settings")]
    [SerializeField]
    int Status = 0;

    [Header("Components")]
    [SerializeField]
    TextMeshProUGUI StatusText;

    [HideInInspector]
    public int ZoneToEnable = 0;

    [SerializeField]
    GameObject[] TrainingSystems; 

    [SerializeField]
    Material OnMat;

    [SerializeField]
    Material OffMat;

    MeshRenderer meshRenderer;

    #endregion

    #region In-Built Functions

    void Awake()
    {
        meshRenderer = GetComponent<MeshRenderer>();

        if(Status == 0)
        {
            StatusText.text = "R";
            meshRenderer.material = OnMat;
        }
        else if(Status == 1)
        {
            StatusText.text = "S";
            meshRenderer.material = OffMat;
        }
    }

    #endregion

    #region Abstract Methods

    public override void ImplementCode()
    {
        Status += 1;
        if(Status > 1)
        {
            Status = 0;
        }

        //Start Button
        if(Status == 0)
        {
            StatusText.text = "R";
            meshRenderer.material = OnMat;

            if(ZoneToEnable == 0)
            {
                TrainingSystems[ZoneToEnable].GetComponent<ShootingSessions>().canStart = true;
                //Debug.Log("Starting Pistol training");
            }
            else if(ZoneToEnable == 1)
            {
                TrainingSystems[ZoneToEnable].GetComponent<ShootingSessions>().canStart = true;
                //Debug.Log("Starting SMG training");
            }
            else if(ZoneToEnable == 2)
            {
                TrainingSystems[ZoneToEnable].GetComponent<ShootingSessions>().canStart = true;
                //Debug.Log("Starting AR training");
            }
            else if(ZoneToEnable == 3)
            {
                TrainingSystems[ZoneToEnable].GetComponent<ShootingSessions>().canStart = true;
                //Debug.Log("Starting Sniper training");
            }

        }
        //Reset Button Button
        else if(Status == 1)
        {
            TrainingSystems[ZoneToEnable].GetComponent<ShootingSessions>().canGoDown = true;

            StatusText.text = "S";
            meshRenderer.material = OffMat;
            //Debug.Log("Reset Training");
        }
    }

    #endregion
}
