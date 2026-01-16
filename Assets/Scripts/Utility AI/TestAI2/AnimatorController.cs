using UnityEngine;

public class AnimatorController : IAnimationController , IFsmIdentity
{
    private readonly Animator animator;

    public AnimatorController(Animator animator)
    {
        this.animator = animator;
    }
    public void SetRun(bool value)
    {
        animator.SetBool("Run", value);
    }

    public void SetSit(bool value)
    {
        animator.SetBool("Sit", value);
    }
}
