using UnityEditor.Rendering.LookDev;
using UnityEngine;
using UnityEngine.AI;

[SelectionBase]    // (If you click any child objects in the inspector, it will automatically select the parent object which contains this script
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(ElementComponent))]
[RequireComponent(typeof(NavMeshAgent))]
public class EnemyController : MonoBehaviour
{
    // Enemy properties (public so the state classes can access them)
    [HideInInspector] public Animator animator;
    [HideInInspector] public NavMeshAgent navAgent;
    [HideInInspector] public Transform target;
    [HideInInspector] public ElementComponent enemyElement;
    public EnemySpawnManager spawnManager;
    public float closeAttackDistance = 1f;   // Closest enemy can get to the player and still have room to attack
    public float farAttackDistance = 2f;   // The furthest distance from the player that the enemy can attack and still connect
    public float hangBackDistance = 2f;    // Distance beyond the farAttackDistance to hang back if the player is already under attack

    // -----------------------------------------------------------
    // *** Not needed now we're not using CharacterController component...??
    //public float moveSpeed = 1f;    // Units per second
    //public float turnSpeed = 90f;   // Degrees per second
    //public float rotationFactorPerFrame;   // *** TESTING - may replace the turnSpeed variable above ***
    // -----------------------------------------------------------

    // State machine properties
    EnemyBaseState currentState;
    public EnemyIdleState IdleState = new EnemyIdleState();    // These 'potential' states need to be public so that the concrete State classes can refer to them when telling the Controller which state to switch to
    public EnemyMoveState MoveState = new EnemyMoveState();
    public EnemyAttackState AttackState = new EnemyAttackState();
    public EnemyBackPedalState BackPedalState = new EnemyBackPedalState();
    public EnemyGotHitState GotHitState = new EnemyGotHitState();
    public EnemyDieState DieState = new EnemyDieState();



    private void Awake()
    {
        // NavMeshAgent
        navAgent = GetComponent<NavMeshAgent>();
        target = GameObject.FindGameObjectWithTag("Player").transform;
        navAgent.isStopped = true;

        animator = GetComponent<Animator>();
        enemyElement = GetComponent<ElementComponent>();
        
        SwitchState(IdleState);
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


    public enum PlayerDistance
    {
        TooClose,
        Far,
        AttackRange,
    }


    public PlayerDistance CheckPlayerDistance()
    {
        // Test if the player is still in range
        Vector3 playerVector = target.position - transform.position;
        float playerDistance = playerVector.magnitude;

        PlayerDistance result;
        if (playerDistance > farAttackDistance)
        {
            result = PlayerDistance.Far;
        } else if (playerDistance <= farAttackDistance && playerDistance >= closeAttackDistance)
        {
            result = PlayerDistance.AttackRange;
        } else
        {
            result = PlayerDistance.TooClose;
        }

        return result;
    }


    [ContextMenu("Kill this enemy (debug)")]
    private void Debug_KillEnemy()
    {
        SwitchState(DieState);
    }
}
