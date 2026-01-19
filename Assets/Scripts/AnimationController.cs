using UnityEngine;

public class AnimationController : MonoBehaviour
{
    Animator animator;
    [SerializeField] private GameObject interactionParticlesPrefab;

    private void Start()
    {
        animator = GetComponent<Animator>();       
    }

    public void PetCat()
    {
        animator.SetTrigger("Pet");
    }

    public void SetWalking(bool value)
    {
        animator.SetBool("isWalking", value);
    }
}
