using System.Collections;
using UnityEngine;
using RPG.Combat;
using RPG.Core;
using RPG.Movement;
using RPG.Stats;
using System;

namespace RPG.Control
{
  enum BehaviourState
  {
    Idle,
    Attack,
    Chase
  }

  public class AIController : MonoBehaviour
  {
    const float stateChangeCooldown = 0.5f;

    [SerializeField] float visionRange;
    [SerializeField] float chaseSpeed = 3f;
    [SerializeField] int collisionLayer;

    Attacker attacker;
    Mover mover;
    Health health;
    Health player;
    CharacterStats stats;

    Vector3 guardPosition;
    float lastStateChange = Mathf.NegativeInfinity;
    BehaviourState lastBehaviour = BehaviourState.Idle;

    private void Awake()
    {
      attacker = GetComponent<Attacker>();
      mover = GetComponent<Mover>();
      health = GetComponent<Health>();
      stats = GetComponent<CharacterStats>();
    }

    private void Start()
    {
      guardPosition = transform.position;
      player = PlayerInfo.GetPlayer().GetComponent<Health>();
    }

    private void Update()
    {
      if ((health.IsDead))
      {
        GetComponentInChildren<TargetDetector>()?.Toggle(false);
        return;
      }
      if (attacker.isAttacking) return;

      float playerDistance = Vector3.Distance(player.transform.position, transform.position);

      if (CheckForPlayerInVisionRange(playerDistance)) AggressionBehaviour(playerDistance);
      else IdleBehavior();
    }

    private bool CheckForPlayerInVisionRange(float playerDistance)
    {
      if (player.IsDead) return false;

      return playerDistance <= visionRange;
    }

    private void AggressionBehaviour(float playerDistance)
    {
      if (playerDistance > stats.GetStat(Stat.AttackRange)) ChasePlayer();
      else AttackBehaviour(playerDistance);
    }

    private void ChasePlayer()
    {
      if (!CheckIfStateIsAllowed(BehaviourState.Chase)) return;

      mover.SetMovementSpeed(chaseSpeed);
      mover.StartMoveAction(player.transform.position);
    }

    //insures that the states change not to fast so that the enemy flickers between different states
    private bool CheckIfStateIsAllowed(BehaviourState state)
    {
      if (lastBehaviour == state) return true;

      if (lastStateChange + stateChangeCooldown > Time.time) return false;

      lastStateChange = Time.time;
      lastBehaviour = state;
      return true;
    }

    private void AttackBehaviour(float playerDistance)
    {
      if (!CheckIfStateIsAllowed(BehaviourState.Attack)) return;

      mover.AdjustDirection(player.transform.position);
      attacker.Attack(player, collisionLayer);
    }

    private void IdleBehavior()
    {
      if (!CheckIfStateIsAllowed(BehaviourState.Idle)) return;

      mover.MoveTo(transform.position);
    }

    //called by unity
    private void OnDrawGizmosSelected()
    {
      Gizmos.color = Color.blue;
      Gizmos.DrawWireSphere(transform.position, visionRange);
    }
  }
}
