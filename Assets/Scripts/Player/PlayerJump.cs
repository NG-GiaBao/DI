using System;
using UnityEngine;

[Serializable]
public class PlayerJump 
{
    [field: SerializeField] public bool IsGrounded { get; private set; }
    [SerializeField] private CharacterController controller;
    [SerializeField] private float verticalVelocity; // Vận tốc theo trục Y
    [SerializeField] private float jumpHeight; // Độ cao nhảy
    [SerializeField] private float fallMultiplier; // Hệ số tăng tốc độ rơi


    public void Jump()
    {
        if (IsGrounded && verticalVelocity < 0)
        {
            verticalVelocity = -2f; // Reset to a small negative value to keep grounded
        }
        // Apply gravity
        verticalVelocity += Physics.gravity.y * fallMultiplier * Time.deltaTime;
        // Move the character controller
        Vector3 move = new(0, verticalVelocity, 0);
        controller.Move(move * Time.deltaTime);
    }

    public void SetGroundedState(bool grounded)
    {
        IsGrounded = grounded;
    }
    public void PerformJump()
    {
        if (IsGrounded)
        {
            verticalVelocity = Mathf.Sqrt(jumpHeight * -2f * Physics.gravity.y);
        }
    }
    public void GetController(CharacterController controller)
    {
        if(controller == null)
        {
            Debug.LogError("CharacterController is null!");
            return;
        }
        this.controller = controller;
    }

}
