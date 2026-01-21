using UnityEngine;

public class AnimatorController : IAnimationController, IFsmIdentity
{
    private readonly Animator animator;

    public AnimatorController(Animator animator)
    {
        this.animator = animator;
    }

    public bool IsAnimFinished(string name)
    {
        AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);

        if (stateInfo.IsName(name) && stateInfo.normalizedTime >= 1f)
        {
            Debug.Log($"Animation {name} finished.".ToColor(this, Color.cyan));
            return true;
        }
        return false;
    }

    public void SetBool(string name, bool value)
    {
        animator.SetBool(name, value);
    }

    public void SetTrigger(string name)
    {
        animator.SetTrigger(name);
    }
}
