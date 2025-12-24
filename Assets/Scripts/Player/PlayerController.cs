using UnityEngine;
using UnityEngine.InputSystem;



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

    private CoreContext context;

    public struct OnEventClick { }

    private void Awake()
    {
        animator = GetComponent<Animator>();
        characterController = GetComponent<CharacterController>();
        playerInteract = GetComponent<PlayerInteract>();
        Register.RegisterRef<PlayerController>(this);
    }

    private void Start()
    {
        Init();
        Subscrision();
    }

    private void OnValidate()
    {
        validTags.Syns();
    }

    private void Update()
    {
        playerMove.Move(cinemaCamera);
        playerJump.Jump();
        if (playerInteract != null)
        {
            playerInteract.ShootRaycast();
        }

    }
    private void OnDestroy()
    {
        UnSubscrision();
    }

   

    public void OnInject(CoreContext context)
    {
        this.context = context;
    }

    private void Init()
    {
        if (characterController != null)
        {
            playerJump.GetController(characterController);
            playerMove.GetController(characterController);
        }
        if (animator != null)
        {
            playerAnim.GetAnimator(animator);
        }

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
    public void OnPick()
    {
        if (playerInteract != null)
        {
            playerInteract.Pickup();
        }
    }
    public void OnClick()
    {
        if (playerInteract.IsInteractingNPC)
        {
            context.Events.Publish<PlayerController, OnEventClick>();
        }
    }
    #endregion
    #region Event Methods

    private void Subscrision()
    {
        playerMove.OnPlayerMove += UpdateAnim;
    }

    private void UnSubscrision()
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
    #region Another Methods
    private void UpdateAnim()
    {
        playerAnim.UpdateAnimMove(playerMove.IsMoving);
    }

    #endregion
}
