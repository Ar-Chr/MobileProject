using UnityEngine;

public class RandomAnimationStart : MonoBehaviour
{
    [SerializeField] Animator animator;

    void Reset()
    {
        animator = GetComponent<Animator>();
    }

    void Start()
    {
        animator.Play(0, -1, Random.Range(0, 1));
    }
}
