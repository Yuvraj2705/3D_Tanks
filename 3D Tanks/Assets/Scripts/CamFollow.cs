using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamFollow : MonoBehaviour
{
    [SerializeField] Transform Player;
    [SerializeField] Vector3 offset;

    void FixedUpdate()
    {
        transform.position = offset + Player.transform.position;
    }
}
