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
    [HideInInspector] public Transform playerTransform;
    [HideInInspector] public ElementComponent enemyElement;
    [HideInInspector] public EnemySpawnManager spawnManager;
    [HideInInspector] public EnemyWeapon enemyWeapon;
    [HideInInspector] public EnemyAttackersTracker attackersTracker;


    public float closeAttackDistance = 2f;
    public float farAttackDistance = 4f;
    public float hangBackDistance = 7f;
    public float enemyTurnSpeed = 5f;
    public float facingPlayerDegreesMargin = 10f;   // The plus/minus margin of error (in degrees) that the enemy can be facing to the left or right of the player but still be close enough to be considered 'facing the player'
    

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
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        navAgent.isStopped = false;

        animator = GetComponent<Animator>();
        enemyElement = GetComponent<ElementComponent>();
        enemyWeapon = GetComponentInChildren<EnemyWeapon>();

        // Check if attackersTracker has been assigned
        if (attackersTracker == null)
        {
            // If not, first check if the player has one, and add one if not.
            if (playerTransform.gameObject.GetComponent<EnemyAttackersTracker>() == null)
            {
                playerTransform.gameObject.AddComponent<EnemyAttackersTracker>();
            }

            // Then assign the player's EnemyAttackersTracker to the property
            attackersTracker = playerTransform.gameObject.GetComponent<EnemyAttackersTracker>();
        }


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


    public float DistanceToPlayer()
    {
        float distance = (playerTransform.position - this.transform.position).magnitude;
        //Debug.Log("Distance to player: " + distance.ToString());
        return distance;
    }


    public Vector3 FlatDirectionToPlayer()
    {
        // Returns the direction of the player on a flat plane i.e. with no up/down element
        Vector3 directionToPlayer = playerTransform.position - transform.position;
        Vector3 flatDirectionToPlayer = new Vector3(directionToPlayer.x, 0f, directionToPlayer.z);
        return flatDirectionToPlayer;

    }

    public Vector3 FlatEnemyFacingDirection()
    {
        Vector3 flatDirection = new Vector3(transform.forward.x, 0f, transform.forward.z);
        return flatDirection;
    }


    public bool EnemyIsFacingPlayer()
    {

        float angle = Vector3.Angle(FlatDirectionToPlayer(), FlatEnemyFacingDirection());
        bool isFacing = angle < facingPlayerDegreesMargin;
        
        //if (isFacing) {
        //    Debug.Log("Enemy is facing player");
        //} else
        //{
        //    Debug.Log("Enemy is --NOT-- facing player");
        //}

        return isFacing;
    }


    public void TurnTowardsPlayer()
    {
        Quaternion rotationToPlayer = Quaternion.LookRotation(FlatDirectionToPlayer());
        transform.rotation = Quaternion.RotateTowards(transform.rotation, rotationToPlayer, enemyTurnSpeed * Time.deltaTime);
        
    }


    public Vector3 PositionToMoveTo(float distanceFromPlayer)
    {
        Vector3 playerToEnemyDirection = (transform.position - playerTransform.position).normalized;
        Vector3 targetPosition = playerTransform.position + (playerToEnemyDirection * distanceFromPlayer);
        return targetPosition;
    }


    [ContextMenu("Kill this enemy (debug)")]
    private void Debug_KillEnemy()
    {
        SwitchState(DieState);
    }
}
