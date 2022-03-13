using UnityEngine;

public class TargetTwo : ShootManager
{
    [SerializeField] Transform[] checkPoint;

    [SerializeField]
    Animator Targetanimator;

    [SerializeField]
    int MaxHits = 5;

    int currentHits;

    int counter;
    [SerializeField] float speed = 5;

    bool isDead;

    [SerializeField]
    GameObject DrillObject;

    DrillSession script;

    void Start()
    {
        isDead = false;
        counter = 0;
        currentHits = MaxHits;
        Targetanimator = GetComponent<Animator>();
        script = GetComponent<DrillSession>();
    }

    void Update()
    {
        if (!isDead)
        {
            Motion();
        }
    }

    void Motion()
    {
        transform.position = Vector3.MoveTowards(transform.position, checkPoint[counter].position, speed * Time.deltaTime);
        if (Vector3.Distance(transform.position, checkPoint[counter].position) < 0.5)
        {
            if (counter == checkPoint.Length - 1)
            {
                counter = 0;
            }
            else
            {
                counter++;
            }
        }
    }

    public override void Damage(int damage)
    {
        currentHits -= damage;
        if (currentHits <= 0)
        {
            script.enemyCount += 1;
            isDead = true;
            Targetanimator.SetBool("GoDown", true);
        }
    }
}