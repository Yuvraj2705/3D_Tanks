using UnityEngine;

public class TTCollider : MonoBehaviour
{
    [SerializeField] GameObject target;

    TargetThree script;

    void Start()
    {
        script = target.GetComponent<TargetThree>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            script.Targetanimator.SetBool("GoUp", true);
            gameObject.SetActive(false);
        }
    }
}