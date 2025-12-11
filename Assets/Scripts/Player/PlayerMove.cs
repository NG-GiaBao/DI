using System;
using UnityEngine;

[Serializable]
public class PlayerMove 
{
    
    [SerializeField] private CharacterController characterController;
    [SerializeField] private float moveSpeed; // tốc độ di chuyển
    [SerializeField] private Vector2 playerInput;
    [field: SerializeField] public bool IsMoving { get; private set; }
    public Action OnPlayerMove;



    public void Move(Transform camera)
    {
        if (!IsMoving) return;
       
        Vector3 camForward = camera.forward;
        camForward.y = 0;
        camForward.Normalize();

        Vector3 camRight = camera.right;
        camRight.y = 0;
        camRight.Normalize();
        Vector3 moveDirection = camForward * playerInput.y + camRight * playerInput.x;
        characterController.Move( moveSpeed * Time.deltaTime * moveDirection);
       
    }
    public void SetPlayerInput(Vector2 input)
    {
        playerInput = input;
        CheckIsMoving();
        OnPlayerMove?.Invoke();
    }
    public void GetController(CharacterController controller)
    {
        if(controller == null)
        {
            Debug.LogError("CharacterController is null!");
            return;
        }
        characterController = controller;
    }

    public void CheckIsMoving()
    {
        IsMoving = playerInput != Vector2.zero;
    }

}
