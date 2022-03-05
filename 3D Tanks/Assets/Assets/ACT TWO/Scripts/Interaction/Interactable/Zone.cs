using UnityEngine;
using TMPro;

public class Zone : SelectionManager
{

    [SerializeField] 
    int ZoneState = 0;

    [SerializeField]
    TextMeshProUGUI ZoneText; 

    [SerializeField]
    GameObject StartButton;

    ZoneImplementer zoneImplementorScript;

    public bool canChange;

    void Awake()
    {
        canChange = true;

        zoneImplementorScript = StartButton.GetComponent<ZoneImplementer>();

        if(ZoneState == 0)
        {
            ZoneText.text = "PISTOL TRAINING 50 YARDS";
            zoneImplementorScript.ZoneToEnable = 0;
        }
        else if(ZoneState == 1)
        {
            ZoneText.text = "SMG TRAINING 100 YARDS";
            zoneImplementorScript.ZoneToEnable = 1;
        }
        else if(ZoneState == 2)
        {
            ZoneText.text = "ASSAULT TRAINING 200 YARDS";
            zoneImplementorScript.ZoneToEnable = 2;
        }
        else if(ZoneState == 3)
        {
            ZoneText.text = "SNIPER TRAINING 400 YARDS";
            zoneImplementorScript.ZoneToEnable = 3;
        }
    }

    public override void ImplementCode()
    {
        if(canChange)
        {
            ZoneState += 1;
            if(ZoneState > 3)
            {
                ZoneState = 0;
            }

            if(ZoneState == 0)
            {
                ZoneText.text = "PISTOL TRAINING 50 YARDS";
                zoneImplementorScript.ZoneToEnable = 0;
            }
            else if(ZoneState == 1)
            {
                ZoneText.text = "SMG TRAINING 100 YARDS";
                zoneImplementorScript.ZoneToEnable = 1;
            }
            else if(ZoneState == 2)
            {
                ZoneText.text = "ASSAULT TRAINING 200 YARDS";
                zoneImplementorScript.ZoneToEnable = 2;
            }
            else if(ZoneState == 3)
            {
                ZoneText.text = "SNIPER TRAINING 400 YARDS";
                zoneImplementorScript.ZoneToEnable = 3;
            }
        }
    }
}
