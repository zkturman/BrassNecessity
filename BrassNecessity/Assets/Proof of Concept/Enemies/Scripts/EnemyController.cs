using UnityEngine;


[SelectionBase]    // (If you click any child objects in the inspector, it will automatically select the parent object which contains this script)
[RequireComponent(typeof(CharacterController))]
[RequireComponent(typeof(Animator))]
public class EnemyController : MonoBehaviour
{
    // Enemy properties (public so the state classes can access them)
    [HideInInspector] public CharacterController charaController;
    [HideInInspector] public Animator animator;
    [HideInInspector] public GameObject target;
    public float moveSpeed = 1f;    // Units per second
    public float turnSpeed = 90f;   // Degrees per second
    public float rotationFactorPerFrame;   // *** TESTING - may replace the turnSpeed variable above ***
    public float attackDistance = 1.5f;   // How close to move to the player before standing still and hitting
    public EnemySpawnManager spawnManager;


    // State machine properties
    EnemyBaseState currentState;
    public EnemyIdleState IdleState = new EnemyIdleState();    // These 'potential' states need to be public so that the concrete State classes can refer to them when telling the Controller which state to switch to
    public EnemyMoveState MoveState = new EnemyMoveState();
    public EnemyAttackState AttackState = new EnemyAttackState();
    public EnemyDieState DieState = new EnemyDieState();


    private void Awake()
    {
        currentState = MoveState;
        charaController= GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
        target = GameObject.FindGameObjectWithTag("Player");
    }


    private void Update()
    {
        // Triggers the UpdateState function each frame on whichever state is current
        currentState.UpdateState(this);
    }


    public void SwitchState(EnemyBaseState newState)
    {
        // This is public because it will be called from whichever concrete State class is the 'currentState'.
        // currentState will pass in a reference to one of this EnemyController's other public state properties (e.g. MoveState or AttackState)
        currentState = newState;
        currentState.EnterState(this);
    }


    private void OnCollisionEnter(Collision collision)
    {
        // The EnemyController receives the OnCollisionEntered message because it is a monobehaviour, but the concrete states don't
        // So when the EnemyController receives a collision, it triggers a separate method in the current concrete class to process it.
        currentState.CollisonEntered(this, collision);
    }


    public void AnimationFinished(string animName)
    {
        // This gets called by an AnimationEvent on relevant AnimationClips (e.g. after an attack animation has completed)
        // This should be fed to the currentState to see whether the state needs to respond to the animation finishing
        currentState.AnimationClipFinished(this, animName);
    }


    [ContextMenu("Kill enemy (debug)")]
    private void Debug_KillEnemy()
    {
        SwitchState(DieState);
    }
}
