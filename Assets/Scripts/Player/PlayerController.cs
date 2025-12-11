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

    private void Awake()
    {
        animator = GetComponent<Animator>();
        characterController = GetComponent<CharacterController>();
    }

    private void Start()
    {
        Init();
        Register();
    }
    private void OnValidate()
    {
        validTags.Syns();
    }

    private void Update()
    {
        playerMove.Move(cinemaCamera);
        playerJump.Jump();
    }
    private void OnDestroy()
    {
        UnRegister();
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
        playerJump.GetController(characterController);
        playerMove.GetController(characterController);
        playerAnim.GetAnimator(animator);
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
