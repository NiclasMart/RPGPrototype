using System.Collections;
using UnityEngine;
using RPG.Combat;
using RPG.Core;
using RPG.Movement;
using RPG.Stats;

namespace RPG.Control
{
  public class AIController : MonoBehaviour
  {
    [SerializeField] float chaseDistance;
    [SerializeField] float chaseSpeed = 3f;
    [SerializeField] float suspicionTime;
    [SerializeField] int collisionLayer;

    [Header("Patrol Parameters")]
    [SerializeField] Path patrolPath;
    [SerializeField] float patrolSpeed = 2f;
    [SerializeField] float checkpointTolerance = 1f;
    [SerializeField] float checkpointDwellTime = 3f;

    Fighter fighter;
    Mover mover;
    Health health;
    GameObject player;

    Vector3 guardPosition;
    int currentWaypoint;
    float lastSeenPlayerTime = -Mathf.Infinity;

    private void Awake()
    {
      fighter = GetComponent<Fighter>();
      mover = GetComponent<Mover>();
      health = GetComponent<Health>();
    }

    private void Start()
    {
      guardPosition = transform.position; //potential canidate for lazy loading in awake
      player = PlayerInfo.GetPlayer();
      if (patrolPath != null) currentWaypoint = patrolPath.GetClosestWaypoint(transform.position);
    }

    //RESTRUCTURE: need state storage for performance reason
    private void Update()
    {
      if (health.IsDead) return;

      if (CheckForAttackabelPlayer())
      {
        lastSeenPlayerTime = Time.time;
        AttackBehaviour();
      }
      else if (lastSeenPlayerTime + suspicionTime > Time.time)
      {
        SuspicionBehaviour();
      }
      else
      {
        GuardBehaviour();
      }
    }

    private void GuardBehaviour()
    {
      Vector3 nextPosition = guardPosition;
      mover.SetMovementSpeed(patrolSpeed);

      if (patrolPath) nextPosition = FindNextWaypoint();

      mover.MoveTo(nextPosition);
    }

    bool pausePatrolIsActive = false;
    private Vector3 FindNextWaypoint()
    {
      bool reachedCurrentCheckpoint = Vector3.Distance(transform.position, patrolPath.GetCurrentWaypoint(currentWaypoint)) <= checkpointTolerance;

      if (reachedCurrentCheckpoint && !pausePatrolIsActive) StartCoroutine(PausePatrol());

      return patrolPath.GetCurrentWaypoint(currentWaypoint);
    }


    IEnumerator PausePatrol()
    {
      pausePatrolIsActive = true;
      yield return new WaitForSeconds(checkpointDwellTime);
      currentWaypoint = (currentWaypoint + 1) % patrolPath.WaypointCount;
      pausePatrolIsActive = false;
    }

    private void SuspicionBehaviour()
    {
      GetComponent<ActionScheduler>().CancelCurrentAction();
    }

    private void AttackBehaviour()
    {
      fighter.SetCombatTarget(player, collisionLayer);
      mover.SetMovementSpeed(chaseSpeed);
    }

    private bool CheckForAttackabelPlayer()
    {
      if (player == null) return false;

      bool playerInRange = Vector3.Distance(player.transform.position, transform.position) <= chaseDistance;
      bool playerIsAlive = !player.GetComponent<Health>().IsDead;

      if (playerInRange && playerIsAlive) return true;
      else return false;
    }

    //called by unity
    private void OnDrawGizmosSelected()
    {
      Gizmos.color = Color.blue;
      Gizmos.DrawWireSphere(transform.position, chaseDistance);
    }
  }
}
