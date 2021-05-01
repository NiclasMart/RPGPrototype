using UnityEngine;
using UnityEngine.AI;
using RPG.Core;
using RPG.Stats;

namespace RPG.Resources
{
  public class Health : MonoBehaviour
  {
    BaseStats stats;
    Animator animator;
    float currentHealth;
    bool isDead = false;
    public bool IsDead => isDead;

    private void Start()
    {
      animator = GetComponent<Animator>();
      stats = GetComponent<BaseStats>();
      currentHealth = stats.GetHealth();
    }

    public void ApplyDamage(float damage)
    {
      currentHealth = Mathf.Max(0, currentHealth - damage);
      if (currentHealth == 0 && !isDead)
      {
        HandleDeath();
      }
      print("current Health: " + currentHealth);
    }

    private void HandleDeath()
    {
      if (animator != null) animator.SetTrigger("die");
      GetComponent<ActionScheduler>().CancelCurrentAction();
      DisableNavAgent();
      isDead = true;
    }

    private void DisableNavAgent()
    {
      if (TryGetComponent(out NavMeshAgent agent)) agent.enabled = false;
    }
  }
}
