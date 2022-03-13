using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetThree : ShootManager
{
    public Animator Targetanimator;

    [SerializeField]
    int MaxHits = 5;

    int currentHits;

    void Start()
    {
        currentHits = MaxHits;
        Targetanimator = GetComponent<Animator>();
    }

    public override void Damage(int damage)
    {
        currentHits -= damage;
        if (currentHits <= 0)
        {
            Targetanimator.SetBool("GoDown", true);
        }
    }
}