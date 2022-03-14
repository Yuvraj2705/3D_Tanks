using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetThree : ShootManager
{
    public Animator Targetanimator;

    [SerializeField]
    int MaxHits = 5;

    int currentHits;

    [SerializeField]
    GameObject DrillObject;

    DrillSession script;

    void Start()
    {
        currentHits = MaxHits;
        Targetanimator = GetComponent<Animator>();
        script = GetComponent<DrillSession>();
    }

    public override void Damage(int damage)
    {
        currentHits -= damage;
        if (currentHits <= 0)
        {
            //script.enemyCount += 1;
            Targetanimator.SetBool("GoDown", true);
        }
    }
}