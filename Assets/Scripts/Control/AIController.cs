using UnityEngine;
using RPG.Combat;
using RPG.Core;
using RPG.Movement;
using System.Collections;

namespace RPG.Control
{
  public class AIController : MonoBehaviour
  {
    [SerializeField] float chaseDistance;
    [SerializeField] float suspicionTime;

    Fighter fighter;
    Health health;
    GameObject player;

    Vector3 guardPosition;
    float lastSeenPlayerTime = -Mathf.Infinity;

    private void Awake()
    {
      fighter = GetComponent<Fighter>();
      health = GetComponent<Health>();
      player = GameObject.FindWithTag("Player");

      guardPosition = transform.position;
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
      GetComponent<Mover>().MoveTo(guardPosition);
    }

    private void SuspicionBehaviour()
    {
      GetComponent<ActionScheduler>().CancelCurrentAction();
    }

    private void AttackBehaviour()
    {
      fighter.SetCombatTarget(player);
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
    private void OnDrawGizmos()
    {
      Gizmos.color = Color.blue;
      Gizmos.DrawWireSphere(transform.position, chaseDistance);
    }
  }
}
