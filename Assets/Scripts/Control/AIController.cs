using UnityEngine;
using RPG.Combat;

namespace RPG.Control
{
  public class AIController : MonoBehaviour
  {
    [SerializeField] float chaseDistance;
    Fighter fighter;
    Health health;
    GameObject player;

    private void Awake()
    {
      fighter = GetComponent<Fighter>();
      health = GetComponent<Health>();
      player = GameObject.FindWithTag("Player");
    }

    private void Update()
    {
      if (health.IsDead) return;
      AttackPlayerInDistance();
    }

    private void AttackPlayerInDistance()
    {
      if (player)
      {
        bool shoudChase = Vector3.Distance(player.transform.position, transform.position) <= chaseDistance;
        Health playerHealth = player.GetComponent<Health>();
        if (shoudChase && !playerHealth.IsDead) fighter.SetCombatTarget(player);
        else fighter.Cancel();
      }
    }
  }
}
