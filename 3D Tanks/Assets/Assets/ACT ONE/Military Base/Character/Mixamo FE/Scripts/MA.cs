using UnityEngine;

public class MA : MonoBehaviour
{
    private Animator ani;
    [SerializeField] float AnimationId;

    void Start()
    {
        ani = GetComponent<Animator>();
        ani.SetFloat("Blend", AnimationId);
    }
}
