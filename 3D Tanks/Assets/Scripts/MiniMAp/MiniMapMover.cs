using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniMapMover : MonoBehaviour
{
    [SerializeField] GameObject player;

    [SerializeField] GameObject cam;

    [SerializeField] Vector3 offset;

    void LateUpdate()
    {
        transform.position = player.transform.position + offset;

        Vector3 rot = new Vector3(90, cam.transform.eulerAngles.y , 0);

        transform.rotation = Quaternion.Euler(rot);
    }
}
