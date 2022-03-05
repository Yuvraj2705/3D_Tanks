using UnityEngine;

public class TargetD : ShootManager
{
    [SerializeField] 
    Animator Targetanimator;

    [SerializeField]
    int MaxHits = 5;

    int currentHits;

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
            Targetanimator.SetBool("GoDown",true);
            Targetanimator.SetBool("GoUp",false);
        }
    }
}
