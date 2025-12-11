using System;
using UnityEngine;

[Serializable]
public class PlayerAnim
{
    [SerializeField] private Animator animator;

    public void GetAnimator(Animator animator)
    {
        if(animator == null)
        {
            Debug.LogError("Animator is null!");
            return;
        }
        this.animator = animator;
    }

    public void UpdateAnimMove(bool isMoving)
    {
        animator.SetBool("Run", isMoving);
    }
}
