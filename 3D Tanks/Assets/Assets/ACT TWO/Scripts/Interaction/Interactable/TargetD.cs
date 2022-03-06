using UnityEngine;

public class TargetD : ShootManager
{
    [SerializeField] 
    Animator Targetanimator;

    [SerializeField]
    public int MaxHits = 5;

    [HideInInspector]
    public int currentHits;

     void Awake()
    {
        currentHits = MaxHits;
        Targetanimator = GetComponent<Animator>();
    }

    public override void Damage(int damage)
    {
        currentHits -= damage;
        if(currentHits <= 0)
        {
            currentHits = MaxHits;
            Targetanimator.SetBool("GoDown",true);
            Targetanimator.SetBool("GoUp",false);
        }
    }
}
