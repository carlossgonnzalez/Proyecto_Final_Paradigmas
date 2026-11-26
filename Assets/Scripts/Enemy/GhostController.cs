
using UnityEngine;

[RequireComponent(typeof(GhostMovement))]
[RequireComponent(typeof(GhostPerception))]
[RequireComponent(typeof(GhostAttack))]
public class GhostController : MonoBehaviour, IStunnable
{
    [SerializeField] private MazeGridPreview mazePreview;

    [Header("Patrol")]
    [SerializeField] private float patrolWaitTime = 1f;
    [SerializeField] private float patrolSpeed = 3f;

    [SerializeField]
    private Vector2Int[] patrolPoints =
    {
        new Vector2Int(2, 3),
        new Vector2Int(6, 3),
        new Vector2Int(6, 7),
        new Vector2Int(2, 7)
    };

    [Header("Chase")]
    [SerializeField] private float chaseSpeed = 4f;
    [SerializeField] private float repathInterval = 0.25f;
    [SerializeField] private float losePlayerTime = 2f;

    [Header("Search")]
    [SerializeField] private float searchSpeed = 3f;
    [SerializeField] private float searchWaitTime = 0.75f;

    private GhostMovement movement;
    private GhostPerception perception;
    private GhostAttack attack;

    private GhostStateMachine stateMachine;

    private PatrolState patrolState;
    private ChaseState chaseState;
    private SearchState searchState;
    private AttackState attackState;
    private StunnedState stunnedState;

    private MazeGrid grid;

    private Vector2Int lastKnownPlayerCell;
    private float lostTimer;

    private void Start()
    {
        movement = GetComponent<GhostMovement>();
        perception = GetComponent<GhostPerception>();
        attack = GetComponent<GhostAttack>();

        if (mazePreview == null || perception.Player == null)
        {
            Debug.LogError("Missing Maze Preview or Player.");
            return;
        }

        if (!attack.Initialize(perception.Player))
            return;

        if (patrolPoints == null || patrolPoints.Length < 2)
        {
            Debug.LogError("Not enough patrol points.");
            return;
        }

        grid = mazePreview.CreateTestGrid();

        foreach (Vector2Int point in patrolPoints)
        {
            if (grid.GetCell(point) != MazeCellType.Floor)
            {
                Debug.LogError("Invalid patrol point: " + point);
                return;
            }
        }

        transform.position =
            grid.CellToWorld(patrolPoints[0]);

        movement.Initialize(grid);

        stateMachine = new GhostStateMachine();

        patrolState = new PatrolState(
            movement,
            patrolPoints,
            patrolWaitTime
        );

        chaseState = new ChaseState(
            movement,
            repathInterval
        );

        searchState = new SearchState(
            movement,
            grid,
            searchWaitTime
        );

        attackState = new AttackState(
            movement,
            attack
        );

        stunnedState = new StunnedState(movement);

        StartPatrol();
    }

    private void Update()
    {
        if (stateMachine == null)
            return;

        // Detener la IA cuando el jugador muere
        if (attack.IsPlayerDead || perception.Player == null)
        {
            stateMachine.ChangeState(null);
            movement.StopMovement();
            return;
        }

        if (stateMachine.CurrentState == stunnedState)
        {
            stateMachine.Tick();

            if (stunnedState.IsFinished)
            {
                StartPatrol();
            }

            return;
        }

        Vector2Int ghostCell =
            grid.WorldToCell(transform.position);

        Vector2Int playerCell =
            grid.WorldToCell(perception.Player.position);

        bool canSeePlayer =
            grid.GetCell(ghostCell) == MazeCellType.Floor &&
            grid.GetCell(playerCell) == MazeCellType.Floor &&
            perception.CanSeePlayer();

        if (canSeePlayer)
        {
            lostTimer = 0f;

            lastKnownPlayerCell = playerCell;

            chaseState.SetTarget(playerCell);

            if (attack.CanAttack())
            {
                if (stateMachine.CurrentState != attackState)
                {
                    StartAttack();
                }
            }
            else if (stateMachine.CurrentState != chaseState)
            {
                StartChase();
            }
        }
        else if (stateMachine.CurrentState == attackState)
        {
            chaseState.SetTarget(lastKnownPlayerCell);
            StartChase();
        }
        else if (stateMachine.CurrentState == chaseState)
        {
            lostTimer += Time.deltaTime;

            if (lostTimer >= losePlayerTime)
            {
                StartSearch();
            }
        }
        else if (stateMachine.CurrentState == searchState)
        {
            if (searchState.IsFinished)
            {
                StartPatrol();
            }
        }

        stateMachine.Tick();
    }

    private void StartPatrol()
    {
        lostTimer = 0f;

        movement.SetSpeed(patrolSpeed);

        stateMachine.ChangeState(patrolState);
    }

    private void StartChase()
    {
        lostTimer = 0f;

        movement.SetSpeed(chaseSpeed);

        stateMachine.ChangeState(chaseState);
    }

    private void StartSearch()
    {
        lostTimer = 0f;

        movement.SetSpeed(searchSpeed);

        searchState.SetTarget(lastKnownPlayerCell);

        stateMachine.ChangeState(searchState);
    }

    private void StartAttack()
    {
        stateMachine.ChangeState(attackState);
    }

    public void Stun(float duration)
    {
        if (stateMachine == null ||
            attack.IsPlayerDead ||
            duration <= 0f)
        {
            return;
        }

        stunnedState.SetDuration(duration);

        stateMachine.ChangeState(stunnedState);
    }
}
