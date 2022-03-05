using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

[RequireComponent(typeof(PostProcessVolume))]
public class NighVisionController : MonoBehaviour
{
    [SerializeField] Color defaultLC;
    [SerializeField] Color boostedtLC;

    public bool isNightVisionEnabled;

    PostProcessVolume volume;

    void Start()
    {
        isNightVisionEnabled = false;

        RenderSettings.ambientLight = defaultLC;

        volume = gameObject.GetComponent<PostProcessVolume>();
        volume.weight = 0;
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.N))
        {
            ToggleNighVision();
        }
    }

    void ToggleNighVision()
    {   
        isNightVisionEnabled = !isNightVisionEnabled;

        if(isNightVisionEnabled)
        {
            RenderSettings.ambientLight = boostedtLC;
            volume.weight = 1;
        }
        else
        {
            RenderSettings.ambientLight = defaultLC;
            volume.weight = 0;
        }
    }
}
