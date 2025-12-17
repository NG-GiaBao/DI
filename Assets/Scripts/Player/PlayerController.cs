using System;
using UnityEngine;
using UnityEngine.InputSystem;

public struct Test
{
    public int a;
    public float b;
}
[Serializable]
public struct Test2
{
    public string message;
}

public class PlayerController : MonoBehaviour
{
    [SerializeField] private SerializableHashSet validTags;
    [Header("Player Behavior")]
    [SerializeField] private PlayerJump playerJump;
    [SerializeField] private PlayerMove playerMove;
    [SerializeField] private PlayerLook playerLook;
    [SerializeField] private PlayerAnim playerAnim;
    [Header("Components")]
    [SerializeField] private CharacterController characterController;
    [SerializeField] private Transform cinemaCamera;
    [SerializeField] private Animator animator;

    [Header("References")]
    [SerializeField] private PlayerInteract playerInteract;

    private EventBus _events;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        characterController = GetComponent<CharacterController>();
        playerInteract = GetComponent<PlayerInteract>();
    }

    private void Start()
    {
        Init();
        Register();
        Test t = new Test()
        {
            a = 5,
            b = 3.2f
        };
        Test2 test2 = new Test2()
        {
            message = "Hello from PlayerController"
        };
        _events.Publish<PlayerController,Test>(t);
    }
    private void OnValidate()
    {
        validTags.Syns();
    }

    private void Update()
    {
        playerMove.Move(cinemaCamera);
        playerJump.Jump();
        if(playerInteract != null)
        {
            playerInteract.ShootRaycast();
        }    
           
    }
    private void OnDestroy()
    {
        UnRegister();
    }

    public void Init(EventBus events)
    {
        _events = events;
    }

    #region Input Methods
    public void OnMove(InputValue value)
    {
        playerMove.SetPlayerInput(value.Get<Vector2>());
    }
    public void OnJump()
    {
        playerJump.PerformJump();
    }
    public void OnLook(InputValue value)
    {
        Vector2 valueInput = value.Get<Vector2>();
        playerLook.Look(valueInput, transform, cinemaCamera);
    }

    private void Init()
    {
        if(characterController != null)
        {
            playerJump.GetController(characterController);
            playerMove.GetController(characterController);
        }    
        if(animator != null)
        {
            playerAnim.GetAnimator(animator);
        }    
        
    }
    private void Register()
    {
        playerMove.OnPlayerMove += UpdateAnim;
    }

    private void UnRegister()
    {
        playerMove.OnPlayerMove -= UpdateAnim;
    }
    #endregion

    #region Collider Methods
    private void OnTriggerEnter(Collider other)
    {
        if (validTags.Contains(other.tag))
        {
            playerJump.SetGroundedState(true);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (validTags.Contains(other.tag))
        {
            playerJump.SetGroundedState(false);
        }
    }
    #endregion
    private void UpdateAnim()
    {
        playerAnim.UpdateAnimMove(playerMove.IsMoving);
    }
}
