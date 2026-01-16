using UnityEngine;
using UnityEngine.AI;

public class NPCBehavior : MonoBehaviour
{
    [SerializeField] private StateMachine fsm;
    [SerializeField] private FsmContext context;
    [SerializeField] private Transform chairPos;
    [SerializeField] private NavMeshAgent agent;
    [SerializeField] private Animator animator;
    private void Awake()
    {
        Init();
    }
    private void Start()
    {
        fsm.ChangeState(NameAction.Move);
    }

    private void Update()
    {
        fsm.Tick();
    }
    private void Init()
    {
        fsm = new StateMachine();
        NavMeshMover mover = new(agent);
        AnimatorController controller = new(animator);
        TranformRotate tranformRotate = new(this.transform,chairPos);
        context = new FsmContext();
        context.RegisterIdentity(mover);
        context.RegisterIdentity(controller);
        context.RegisterIdentity(tranformRotate);
        //context = new FsmContext(mover,controller,tranformRotate);
        fsm.Register(NameAction.Move,new MoveAction(context,chairPos,this.transform));
        fsm.Register(NameAction.Idle, new IdleAction());
    }
}
