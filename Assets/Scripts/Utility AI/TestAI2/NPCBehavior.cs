using UnityEngine;
using UnityEngine.AI;

public class NPCBehavior : MonoBehaviour
{
    [SerializeField] private string nameAction;
    private StateMachine<NPCBehavior> fsm;
    [SerializeField] private Transform chairPos;
    [SerializeField] private NavMeshAgent agent;
    [SerializeField] private Animator animator;
    private void Awake()
    {
        Init();
    }
    private void Start()
    {
        fsm.OnChangeState += OnGetNameState;
        SetRef();
        fsm.RequestAction(NameAction.Move);
    }
    private void OnDestroy()
    {
        fsm.OnChangeState -= OnGetNameState;
    }
    private void Update()
    {
        fsm.Tick();
    }
    private void Init()
    {
        fsm = new StateMachine<NPCBehavior>();
        fsm.Register(NameAction.Move, new MoveAction(this, fsm));
        fsm.Register(NameAction.Idle, new IdleAction(this, fsm));
    }
    private void SetRef()
    {
        if (fsm.GetState(NameAction.Move) is MoveAction moveAction)
        {
            moveAction.Init(agent, chairPos, animator);
        }
    }   
    private void OnGetNameState(string name)
    {
        nameAction = name;
    }    

}
