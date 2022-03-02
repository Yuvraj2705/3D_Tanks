using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimControl : MonoBehaviour
{
    public Animator anim;
    [SerializeField] float actionID;

    void Start()
    {
        anim = GetComponent<Animator>();
        anim.SetFloat("VelX", actionID);
    }
}
