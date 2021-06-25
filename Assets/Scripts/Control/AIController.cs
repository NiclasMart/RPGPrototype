using System.Collections;
using UnityEngine;
using RPG.Combat;
using RPG.Core;
using RPG.Movement;
using RPG.Stats;
using System;

namespace RPG.Control
{
  public class AIController : MonoBehaviour
  {
    [SerializeField] float visionRange;
    [SerializeField] float chaseSpeed = 3f;
    [SerializeField] int collisionLayer;

    Attacker attacker;
    Mover mover;
    Health health;
    Health player;
    CharacterStats stats;

    Vector3 guardPosition;

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
      if ((health.IsDead)) return;

      float playerDistance = Vector3.Distance(player.transform.position, transform.position);

      if (CheckForPlayerInVisionRange(playerDistance)) AggressionBehaviour(playerDistance);
      else IdeleBehavior();
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
      mover.SetMovementSpeed(chaseSpeed);
      mover.MoveTo(player.transform.position);
    }

    private void AttackBehaviour(float playerDistance)
    {
      attacker.Attack(player, collisionLayer);
    }

    private void IdeleBehavior()
    {
      mover.MoveTo(guardPosition);
    }

    //called by unity
    private void OnDrawGizmosSelected()
    {
      Gizmos.color = Color.blue;
      Gizmos.DrawWireSphere(transform.position, visionRange);
    }
  }
}
