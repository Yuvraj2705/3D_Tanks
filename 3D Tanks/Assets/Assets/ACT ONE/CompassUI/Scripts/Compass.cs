using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Compass : MonoBehaviour
{
    public Camera myCamera;

    public RawImage compassImage;

    private void Update()
    {
        compassImage.uvRect = new Rect(myCamera.transform.rotation.eulerAngles.y / 360f, 0f, 1f, 1f);
    }
}
