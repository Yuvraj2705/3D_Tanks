using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetOne : ShootManager
{
    [SerializeField]
    Animator Targetanimator;

    [SerializeField]
    int MaxHits = 5;

    int currentHits;

    [SerializeField]
    GameObject DrillObject;

    DrillSession script;

    bool over;

    void Start()
    {
        over = true;
        currentHits = MaxHits;
        Targetanimator = GetComponent<Animator>();
        script = GetComponent<DrillSession>();
    }

    public override void Damage(int damage)
    {
        if(over)
        {
            currentHits -= damage;
            if (currentHits <= 0)
            {
                GameObject.FindGameObjectWithTag("DBMS").GetComponent<DrillSession>().enemyCount += 1;
                Targetanimator.SetBool("GoDown", true);
                over = false;
            }
        }
    }
}